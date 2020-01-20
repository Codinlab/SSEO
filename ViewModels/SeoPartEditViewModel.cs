using Codinlab.SSEO.Models;
using Codinlab.SSEO.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.ViewModels {
    public class SeoPartEditViewModel {
        public SeoPartEditViewModel() { }

        public SeoPartEditViewModel(SeoPart part) {
            if (!String.IsNullOrWhiteSpace(part.Description)) {
                this.Description = part.Description;
                this.OverrideDescription = true;
            }
            this.OverrideKeywords = part.OverrideKeywords;
            this.Keywords = part.Keywords;
            this.OverrideRobots = part.OverrideRobots;
            this.Robots = part.Robots;
            this.CanonicalUrl = part.CanonicalUrl;
        }

        #region SeoPart members
        public virtual bool OverrideDescription { get; set; }
        public virtual string Description { get; set; }
        public virtual string DefaultDescription { get; set; }
        public virtual bool OverrideKeywords { get; set; }
        public virtual string Keywords { get; set; }
        public virtual string DefaultKeywords { get; set; }
        public virtual bool OverrideRobots { get; set; }
        public virtual SeoRobotsMeta Robots { get; set; }
        public virtual SeoRobotsMeta DefaultRobots { get; set; }
        public virtual string CanonicalUrl { get; set; }
        public virtual string DefaultCanonicalUrl { get; set; }

        #endregion

    }
}