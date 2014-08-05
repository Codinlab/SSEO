using Codinlab.SSEO.Models;
using Codinlab.SSEO.Services;
using Orchard;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using Orchard.Mvc.Filters;
using Orchard.UI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Codinlab.SSEO.Filters {
    [OrchardFeature("Codinlab.SSEO")]
    public class SeoFilter : FilterProvider, IResultFilter {
        private readonly IResourceManager _resourceManager;
        private readonly IOrchardServices _orchardServices;
        private readonly ISeoService _seoService;
        private readonly ICurrentContentAccessor _currentContentAccessor;

        public SeoFilter(
            IResourceManager resourceManager,
            IOrchardServices orchardServices,
            ISeoService seoService,
            ICurrentContentAccessor currentContentAccessor
            ) {
			_resourceManager = resourceManager;
			_orchardServices = orchardServices;
            _seoService = seoService;
            _currentContentAccessor = currentContentAccessor;
		}

        public void OnResultExecuted(ResultExecutedContext filterContext) {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext) {
            if (Orchard.UI.Admin.AdminFilter.IsApplied(filterContext.RequestContext)
                || !(filterContext.Result is ViewResult))
                return;

            ContentItem currentContent = _currentContentAccessor.GetCurrentContentItem();
            if (currentContent != null) {
                SeoPart seoPart = currentContent.As<SeoPart>();
                if (seoPart != null) {
                    // Canonical URL
                    string canonicalUrl = _seoService.GetCanonicalUrl(seoPart);
                    if (!String.IsNullOrWhiteSpace(canonicalUrl)) {
                        _resourceManager.RegisterLink(new LinkEntry() { Rel = "canonical", Href = canonicalUrl });
                    }
                    // Description meta
                    string description = _seoService.GetDescription(seoPart);
                    if (!String.IsNullOrWhiteSpace(description)) {
                        _resourceManager.SetMeta(new MetaEntry() { Name = "description", Content = description });
                    }
                    // Keywords meta
                    string keywords = _seoService.GetKeywords(seoPart);
                    if (!String.IsNullOrWhiteSpace(keywords)) {
                        _resourceManager.SetMeta(new MetaEntry() { Name = "keywords", Content = keywords });
                    }
                    // Description meta
                    string robots = _seoService.GetRobots(seoPart);
                    if (!String.IsNullOrWhiteSpace(robots)) {
                        _resourceManager.SetMeta(new MetaEntry() { Name = "robots", Content = robots });
                    }
                }
            }

        }
    }
}