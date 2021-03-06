﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Rail.Tracks
{
    public abstract class TrackBase
    {
        protected TrackType trackType;

        public TrackBase()
        {
            this.Id = Guid.NewGuid();
        }

        [XmlAttribute("Id")]
        public Guid Id { get; set; }

        [XmlIgnore, JsonIgnore]
        public abstract TrackTypes TrackType { get; }

        [XmlIgnore, JsonIgnore]
        public abstract double RampLength { get; }

        [XmlIgnore, JsonIgnore]
        public string Name { get; protected set; }

        [XmlIgnore, JsonIgnore]
        public string Description { get; protected set; }

        [XmlIgnore, JsonIgnore]
        public double RailWidth { get; protected set; }

        [XmlIgnore, JsonIgnore]
        public double TrackWidth { get; protected set; }

        [XmlIgnore, JsonIgnore]
        public abstract List<TrackMaterial> Materials { get; }
        
        [XmlIgnore, JsonIgnore]
        public Geometry TrackGeometry { get; protected set; }

        [XmlIgnore, JsonIgnore]
        public List<TrackDockPoint> DockPoints { get; protected set; }

        public abstract TrackBase Clone();
        public abstract void Update(TrackType trackType);

        public abstract void RenderTrack(DrawingContext drawingContext, Brush trackBrush);

        public abstract void RenderRail(DrawingContext drawingContext);

        public abstract void RenderSelection(DrawingContext drawingContext);
    }
}
