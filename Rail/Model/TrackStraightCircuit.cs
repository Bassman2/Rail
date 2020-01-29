﻿using Rail.Properties;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Rail.Model
{
    public class TrackStraightCircuit : TrackStraight
    {
        [XmlIgnore, JsonIgnore]
        public override string Name
        {
            get
            {
                return $"{Resources.TrackStraightCircuit}";
            }
        }
    }
}
