﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Rail.Model
{
    public class TrackFlex : TrackBase
    {
        [XmlAttribute("MaxLength")]
        public double MaxLength { get; set; }

        protected override Geometry CreateGeometry(double spacing)
        {
            return CurvedGeometry(20, 360, CurvedOrientation.Center, spacing, new Point());
        }

        protected override Drawing CreateRailDrawing(bool isSelected)
        {
            return null;
        }

        protected override List<TrackDockPoint> CreateDockPoints()
        {
            return null;
        }
    }
}