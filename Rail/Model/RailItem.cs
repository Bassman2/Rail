﻿using Rail.Controls;
using Rail.Misc;
using Rail.Mvvm;
using Rail.Trigonometry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Rail.Model
{
    [DebuggerDisplay("RailItem Index={DebugIndex} Id={Id} X={Position.X} Y={Position.Y} A={Angle}")]
    public class RailItem : RailBase
    {
        

        public RailItem()
        { }

        public RailItem(TrackBase track, Point pos, Guid layer) 
        {
            this.DebugIndex = globalDebugIndex++;
            //this.Id = Guid.NewGuid();
            this.TrackId = track.Id;
            this.Track = track;
            this.Position = pos;
            this.Angle = 0.0;
            this.Layer = layer;
            this.DockPoints = track.DockPoints.Select(dp => new RailDockPoint(this, dp)).ToList();
        }

        public RailItem(RailGroupItem railGroupItem)
        {
            railGroupItem.CopyTo(this);
            this.Track = railGroupItem.Track;
            this.TrackId = railGroupItem.TrackId;
        }
        
        //[XmlAttribute("Id")]
        //public Guid Id { get; set; }

        [XmlAttribute("TrackId")]
        public string TrackId { get; set; }

        [XmlIgnore]
        public TrackBase Track { get; set; }

        [XmlIgnore]
        public override List<TrackMaterial> Materials
        {
            get { return this.Track.Materials; }
        }

        public override void DrawRailItem(DrawingContext drawingContext, RailViewMode viewMode, RailLayer layer)
        {
            TransformGroup transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform (this.Angle));
            transformGroup.Children.Add(new TranslateTransform(this.Position.X, this.Position.Y));
            drawingContext.PushTransform(transformGroup);

            //Debug.WriteLine($"DrawRailItem {this.IsSelected}");
            this.Track.Render(drawingContext, viewMode, layer.TrackBrush);
            if (this.IsSelected)
            {
                this.Track.RenderSelection(drawingContext, viewMode);
            }
            
            drawingContext.Pop();

            DrawDebug(drawingContext);
            DrawDebugDogpoints(drawingContext);
        }

        public override bool IsInside(Point point, RailViewMode viewMode)
        {
            Geometry geometry = GetGeometry(viewMode, this.RailTransform);
            bool f = geometry.FillContains(point);
            return f;
        }

        public override bool IsInside(Rect rec, RailViewMode viewMode)
        {
            Geometry geometry = GetGeometry(viewMode, this.RailTransform);
            bool f = rec.Contains(geometry.Bounds);
            return f;
        }

        protected Geometry GetGeometry(RailViewMode viewMode, Transform transform)
        {
            Geometry geometry = viewMode switch { RailViewMode.Tracks => this.Track.GeometryTracks.Clone(), RailViewMode.Rail => this.Track.GeometryRail.Clone(), _ => null };
            geometry.Transform = this.RailTransform;
            return geometry;
        }

        #region debug

        [Conditional("DEBUGINFO")]
        public void DrawDebug(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Blue, 1), new Rect(this.Position, new Size(32, 20)));
            drawingContext.DrawText(new FormattedText(this.DebugIndex.ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 9, Brushes.Blue, 1.25), this.Position);
            drawingContext.DrawText(new FormattedText(this.Angle.Value.ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 9, Brushes.Blue, 1.25), this.Position + new Vector(0, 9));
        }

        [Conditional("DEBUGINFO")]
        public void DrawDebugDogpoints(DrawingContext drawingContext)
        {
            double width = 48;
            foreach (var dp in this.DockPoints)
            {
                Point pos = dp.Position - new Vector((dp.Angle < 45 || dp.Angle > 225 ? width : 0), (dp.Angle < 135 || dp.Angle > 315 ? 20 : 0));
                drawingContext.DrawRectangle(Brushes.White, new Pen(Brushes.Blue, 1), new Rect(pos, new Size(width, 20)));
                string str1 = $"{dp.DebugDockPointIndex}-{dp.DockedWith?.DebugRailIndex},{dp.DockedWith?.DebugDockPointIndex}";
                string str2 = $"{dp.Angle}-{dp.DockedWith?.Angle}";
                drawingContext.DrawText(new FormattedText(str1, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 9, Brushes.Blue, 1.25), pos);
                drawingContext.DrawText(new FormattedText(str2, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, new Typeface("Verdana"), 9, Brushes.Blue, 1.25), pos + new Vector(0, 9));
            }
        }

        #endregion
    }
}
