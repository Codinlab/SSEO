using Codinlab.SSEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Settings {
    public class SeoPartSettings {
        public bool AddCanonicalLink { get; set; }
        public string DefaultDescriptionPattern { get; set; }
        public string DefaultKeywordsPattern { get; set; }
        public SeoRobotsMeta DefaultRobotsMeta { get; set; }
    }
}