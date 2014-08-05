using Codinlab.SSEO.Models;
using Codinlab.SSEO.Permissions;
using Codinlab.SSEO.Services;
using Codinlab.SSEO.Settings;
using Codinlab.SSEO.ViewModels;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.Core.Contents;
using Orchard.Localization;
using Orchard.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Drivers {
    public class SeoPartDriver : ContentPartDriver<SeoPart> {
        private readonly ISeoService _seoService;
        private readonly IAuthorizer _authorizer;
        private const string TemplateName = "Parts/Seo";

        public SeoPartDriver(
            ISeoService seoService,
            IAuthorizer authorizer) {
            _seoService = seoService;
            _authorizer = authorizer;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix { get { return "Seo"; } }

        // GET
        protected override DriverResult Editor(SeoPart part, dynamic shapeHelper) {
            return Editor(part, null, shapeHelper);
        }

        // POST
        protected override DriverResult Editor(SeoPart part, IUpdateModel updater, dynamic shapeHelper) {
            if (!_authorizer.Authorize(DynamicPermissions.CreateDynamicPermission(SeoPermissions.EditSeoPart, part.ContentItem.TypeDefinition))) {
                return null;
            }

            SeoPartEditViewModel model = new SeoPartEditViewModel(part);

            SeoPartSettings settings = part.Settings.GetModel<SeoPartSettings>();
            model.DefaultRobots = settings.DefaultRobotsMeta;
            model.DefaultDescription = _seoService.GenerateDefaultDescription(part);
            model.DefaultKeywords = _seoService.GenerateDefaultKeywords(part);

            if (updater != null && updater.TryUpdateModel(model, Prefix, null, null)) {
                part.Description = model.OverrideDescription ? model.Description : String.Empty;
                part.OverrideKeywords = model.OverrideKeywords;
                part.Keywords = model.Keywords;
                part.OverrideRobots = model.OverrideRobots;
                part.Robots = model.OverrideRobots ? model.Robots : model.DefaultRobots;
            }

            return ContentShape("Parts_SeoPart_Edit", () => {
                return shapeHelper.EditorTemplate(
                    TemplateName: TemplateName,
                    Model: model,
                    Prefix: Prefix);
            });
        }
    }
}