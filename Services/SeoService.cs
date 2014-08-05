using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Tokens;
using Codinlab.SSEO.Models;
using Orchard.ContentManagement;
using Codinlab.SSEO.Settings;
using System.Web.Mvc;
using Orchard.Mvc.Extensions;

namespace Codinlab.SSEO.Services {
    public class SeoService : ISeoService {
        private readonly ITokenizer _tokenizer;
        private readonly IContentManager _contentManager;
        private readonly UrlHelper _urlHelper;

        public SeoService(
            ITokenizer tokenizer,
            IContentManager contentManager,
            UrlHelper urlHelper
            ) {
            _contentManager = contentManager;
            _urlHelper = urlHelper;
            _tokenizer = tokenizer;
        }

        public string GenerateDefaultDescription(SeoPart part) {
            SeoPartSettings settings = part.Settings.GetModel<SeoPartSettings>();
            if (!String.IsNullOrWhiteSpace(settings.DefaultDescriptionPattern)) {
                return _tokenizer.Replace(
                    settings.DefaultDescriptionPattern,
                    BuildTokenContext(part.ContentItem),
                    new ReplaceOptions { Encoding = ReplaceOptions.NoEncode });
            }
            else {
                return String.Empty;
            }
        }

        public string GenerateDefaultKeywords(SeoPart part) {
            SeoPartSettings settings = part.Settings.GetModel<SeoPartSettings>();
            if (!String.IsNullOrWhiteSpace(settings.DefaultKeywordsPattern)) {
            return _tokenizer.Replace(
                settings.DefaultKeywordsPattern,
                BuildTokenContext(part.ContentItem),
                new ReplaceOptions { Encoding = ReplaceOptions.NoEncode });
            }
            else {
                return String.Empty;
            }
        }

        public string GenerateCanonicalUrl(SeoPart part) {
            return _urlHelper.MakeAbsolute(_urlHelper.RouteUrl(_contentManager.GetItemMetadata(part.ContentItem).DisplayRouteValues));
        }

        public string GetDescription(SeoPart part) {
            if (String.IsNullOrWhiteSpace(part.Description)) {
                return GenerateDefaultDescription(part);
            }
            else {
                return part.Description;
            }
        }

        public string GetKeywords(SeoPart part) {
            if (part.OverrideKeywords) {
                return CleanKeywords(part.Keywords);
            }
            else {
                string defaultKeywords = CleanKeywords(GenerateDefaultKeywords(part));
                string additionalKeywords = CleanKeywords(part.Keywords);
                if (String.IsNullOrWhiteSpace(defaultKeywords)) {
                    return additionalKeywords;
                }
                else if (String.IsNullOrWhiteSpace(additionalKeywords)) {
                    return defaultKeywords;
                }
                else {
                    return defaultKeywords + "," + additionalKeywords;
                }
            }
        }

        public string GetRobots(SeoPart part) {
            SeoRobotsMeta robots;
            if (part.OverrideRobots) {
                robots = part.Robots;
            }
            else {
                SeoPartSettings settings = part.Settings.GetModel<SeoPartSettings>();
                robots = settings.DefaultRobotsMeta ;
            }
            return robots != SeoRobotsMeta.All ? robots.ToString().ToLower() : String.Empty;
        }

        public string GetCanonicalUrl(SeoPart part) {
            SeoPartSettings settings = part.Settings.GetModel<SeoPartSettings>();
            return settings.AddCanonicalLink ? GenerateCanonicalUrl(part) : String.Empty;
        }

        #region Private functions
        private IDictionary<string, object> BuildTokenContext(IContent item) {
            return new Dictionary<string, object> { { "Content", item } };
        }

        private string CleanKeywords(string Keywords) {
            return String.IsNullOrWhiteSpace(Keywords) ? String.Empty : Keywords.Trim(new char[] { ' ', ',' });
        }
        
        #endregion

    }
}