using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Models {
    public enum SeoRobotsMeta : int {
        All = 0,
        NoIndex = 1,
        NoFollow = 2,
        None = 3
    }
}