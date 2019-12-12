﻿using Rail.Misc;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Rail.Model
{
    public class TrackCurved : TrackBase
    {
        [XmlAttribute("Radius")]
        public double Radius { get; set; }

        [XmlAttribute("Angle")]
        public double Angle { get; set; }

        protected override void Create()
        {
            this.Geometry = CreateCurvedTrackGeometry(this.Angle, this.Radius);

            // Tracks
            DrawingGroup drawingTracks = new DrawingGroup();
            drawingTracks.Children.Add(new GeometryDrawing(trackBrush, linePen, this.Geometry));
            drawingTracks.Children.Add(this.textDrawing);
            this.drawingTracks = drawingTracks;

            // Rail
            DrawingGroup drawingRail = new DrawingGroup();
            if (this.Ballast)
            {
                drawingRail.Children.Add(CurvedBallast(this.Angle, this.Radius));
            }
            drawingRail.Children.Add(CurvedRail(this.Angle, this.Radius));
            this.drawingRail = drawingRail;

            // Terrain
            this.drawingTerrain = drawingRail;

            Point circleCenter = new Point(0, this.Radius);
            this.DockPoints = new List<TrackDockPoint>
            {
                new TrackDockPoint(circleCenter - PointExtentions.Circle(-this.Angle / 2, this.Radius),  this.Angle / 2 -  45),
                new TrackDockPoint(circleCenter - PointExtentions.Circle( this.Angle / 2, this.Radius), -this.Angle / 2 + 135)
            };
        }
    }
}
