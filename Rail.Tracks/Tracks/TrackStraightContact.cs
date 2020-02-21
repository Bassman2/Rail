﻿using Rail.Tracks.Properties;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Rail.Tracks
{
    public class TrackStraightContact : TrackStraight
    {
        [XmlIgnore, JsonIgnore]
        public override string Name
        {
            get
            {
                return $"{Resources.TrackStraightContact}";
            }
        }
    }
}