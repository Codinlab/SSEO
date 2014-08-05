using Orchard;
using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Services {
    public interface ICurrentContentAccessor : IDependency {
        ContentItem GetCurrentContentItem();
    }

}