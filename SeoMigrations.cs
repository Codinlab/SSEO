using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Environment.Extensions;

namespace Codinlab.SSEO {
    [OrchardFeature("Codinlab.SSEO")]
    public class SeoMigrations : DataMigrationImpl {
        public int Create() {
            ContentDefinitionManager.AlterPartDefinition("SeoPart", builder => builder
                .Attachable()
                .WithDescription("Provides SEO management for your content item.")
            );

            return 1;
        }
    }
}