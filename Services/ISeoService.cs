using Codinlab.SSEO.Models;
using Orchard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codinlab.SSEO.Services {
    public interface ISeoService : IDependency {
        string GenerateDefaultDescription(SeoPart part);
        string GenerateDefaultKeywords(SeoPart part);
        string GenerateCanonicalUrl(SeoPart part);
        string GetDescription(SeoPart part);
        string GetKeywords(SeoPart part);
        string GetRobots(SeoPart part);
        string GetCanonicalUrl(SeoPart part);
    }
}
