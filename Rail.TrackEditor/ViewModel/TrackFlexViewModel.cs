﻿using Rail.Tracks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rail.TrackEditor.ViewModel
{
    public class TrackFlexViewModel : TrackViewModel
    {
        private readonly TrackFlex track;

        public TrackFlexViewModel()
        {
            this.track = new TrackFlex();
        }

        public TrackFlexViewModel(TrackFlex track)
        {
            this.track = track;
        }

        public string Name { get { return this.track.Name; } }

        public string Article
        {
            get { return this.track.Article; }
            set { this.track.Article = value; NotifyPropertyChanged(nameof(Article)); }
        }
    
    }
}