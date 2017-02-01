using Codinlab.SSEO.Models;
using Codinlab.SSEO.Services;
using Orchard.ContentManagement.Handlers;

namespace Codinlab.SSEO.Handlers
{
    public class SeoPartHandler : ContentHandler
    {
        public SeoPartHandler(ISeoService seoService)
        {
            OnIndexing<SeoPart>((context, seoPart) => context.DocumentIndex
                .Add("seo-description", seoService.GetDescription(seoPart)).Analyze()
                .Add("seo-keywords", seoService.GetKeywords(seoPart)).Analyze()
                .Store()
            );
        }
    }
}