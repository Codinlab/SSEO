using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Settings {
    public class SeoPartSettingsHooks : ContentDefinitionEditorEventsBase {

        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition) {
            if (definition.PartDefinition.Name != "SeoPart")
                yield break;

            var settings = definition.Settings.GetModel<SeoPartSettings>();

            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel) {
            if (builder.Name != "SeoPart")
                yield break;

            var settings = new SeoPartSettings();

            if (updateModel.TryUpdateModel(settings, "SeoPartSettings", null, null)) {
                builder.WithSetting("SeoPartSettings.AddCanonicalLink", settings.AddCanonicalLink.ToString());
                builder.WithSetting("SeoPartSettings.DefaultDescriptionPattern", settings.DefaultDescriptionPattern);
                builder.WithSetting("SeoPartSettings.DefaultKeywordsPattern", settings.DefaultKeywordsPattern);
                builder.WithSetting("SeoPartSettings.DefaultRobotsMeta", settings.DefaultRobotsMeta.ToString());
            }

            yield return DefinitionTemplate(settings);
        }
    }
}