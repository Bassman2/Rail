﻿using Rail.Misc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Rail.Model
{
    public class RailDockPoint
    {
        private Point position;
        private double angle;

        public RailDockPoint(TrackDockPoint trackDockPoint)
        {
            this.position = trackDockPoint.Position;
            this.Angle = trackDockPoint.Angle;
        }

        public Point Position { get { return this.position; } }

        public double X {  get { return this.position.X; } }

        public double Y { get { return this.position.Y; } }

        public double Angle 
        { 
            get { return this.angle; }
            //private set { this.angle = Math.Round((value + 360) % 360, 1) % 360; }
            private set { this.angle = (Math.Round(value, 1) % 360.0 + 360.0) % 360.0; }
        }



        public RailDockPoint DockedWith { get; set; }

        public bool IsDocked {  get { return this.DockedWith != null; } }


        public void Move(Vector vec)
        {
            this.position += vec;
        }

        public RailDockPoint Move(Point pos)
        {
            this.position += (Vector)pos;
            return this;
        }

        public void Rotate(double angle)
        {
            this.Angle += angle;
        }

        public void Rotate(double angle, Point center)
        {
            Rotate(angle);
            this.position = this.position.Rotate(angle, center);
        }

        public double Distance(Point p)
        {
            return this.position.Distance(p);
            //return Math.Sqrt(Math.Pow(p.X - position.X, 2) + Math.Pow(p.Y - position.Y, 2));
        }

        public double Distance(RailDockPoint p)
        {
            return this.position.Distance(p.Position);
            //return Math.Sqrt(Math.Pow(p.X - position.X, 2) + Math.Pow(p.Y - position.Y, 2));
        }
    }
}
