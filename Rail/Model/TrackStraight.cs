﻿using Rail.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Rail.Model
{

    public class TrackStraight : TrackBase
    {

        [XmlAttribute("Length")]
        public double Length { get; set; }

        protected override void Create()
        {
            // Tracks
            this.GeometryTracks = CreateStraitTrackGeometry(this.Length, this.RailSpacing);

            DrawingGroup drawingTracks = new DrawingGroup();
            drawingTracks.Children.Add(new GeometryDrawing(trackBrush, this.linePen, this.GeometryTracks));
            drawingTracks.Children.Add(this.textDrawing);
            this.drawingTracks = drawingTracks;

            DrawingGroup drawingTracksSelected = new DrawingGroup();
            drawingTracksSelected.Children.Add(new GeometryDrawing(trackBrush, this.selectedLinePen, this.GeometryTracks));
            drawingTracksSelected.Children.Add(this.textDrawing);
            this.drawingTracksSelected = drawingTracks;

            // Rail
            this.GeometryRail = CreateStraitTrackGeometry(this.Length, this.sleepersSpacing);

            DrawingGroup drawingRail = new DrawingGroup();
            if (this.Ballast)
            {
                drawingRail.Children.Add(StraitBallast(this.Length, StraitOrientation.Center, 0, null));
            }
            drawingRail.Children.Add(StraitRail(false, this.Length));
            this.drawingRail = drawingRail;

            DrawingGroup drawingRailSelected = new DrawingGroup();
            if (this.Ballast)
            {
                drawingRailSelected.Children.Add(StraitBallast(this.Length, StraitOrientation.Center, 0, null));
            }
            drawingRailSelected.Children.Add(StraitRail(true, this.Length));
            this.drawingRailSelected = drawingRailSelected;

            // Terrain
            this.drawingTerrain = drawingRail;

            this.DockPoints = new List<TrackDockPoint> 
            { 
                new TrackDockPoint(0, new Point(-this.Length / 2.0, 0.0), 135, this.dockType), 
                new TrackDockPoint(1, new Point(+this.Length / 2.0, 0.0), 315, this.dockType) 
            };
        }
    }
}
