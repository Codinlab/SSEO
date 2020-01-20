using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Models {
    public class SeoPart : ContentPart {
        public string Description {
            get { return this.Retrieve(x => x.Description, versioned: true); }
            set { this.Store(x => x.Description, value, versioned: true); }
        }

        public bool OverrideKeywords {
            get { return this.Retrieve(x => x.OverrideKeywords, versioned: true); }
            set { this.Store(x => x.OverrideKeywords, value, versioned: true); }
        }

        public string Keywords {
            get { return this.Retrieve(x => x.Keywords, versioned: true); }
            set { this.Store(x => x.Keywords, value, versioned: true); }
        }

        public bool OverrideRobots {
            get { return this.Retrieve(x => x.OverrideRobots, versioned: true); }
            set { this.Store(x => x.OverrideRobots, value, versioned: true); }
        }

        public SeoRobotsMeta Robots {
            get { return (SeoRobotsMeta)this.Retrieve<int>("Robots", versioned: true); }
            set { this.Store<int>("Robots", (int)value, versioned: true); }
        }

        public string CanonicalUrl {
            get { return this.Retrieve(x => x.CanonicalUrl, versioned: true); }
            set { this.Store(x => x.CanonicalUrl, value, versioned: true); }
        }
    }
}