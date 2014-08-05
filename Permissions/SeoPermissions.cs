using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents;
using Orchard.Environment.Extensions.Models;
using Orchard.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Codinlab.SSEO.Permissions {
    public class SeoPermissions : IPermissionProvider {
        public static readonly Permission ManageSeo = new Permission { Description = "Manage SEO", Name = "ManageSeo" };
        public static readonly Permission EditSeoPart = new Permission { Description = "Edit {0} SEO", Name = "EditSeo_{0}", ImpliedBy = new[] { SeoPermissions.ManageSeo } };

        private readonly IContentDefinitionManager _contentDefinitionManager;

        public Feature Feature { get; set; }

        public SeoPermissions(IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
        }

        public IEnumerable<Permission> GetPermissions() {
            var seoEnabledTypes = _contentDefinitionManager.ListTypeDefinitions()
                .Where(ctd => ctd.Parts.Where(ctpd => ctpd.PartDefinition.Name == "SeoPart").Count() > 0);

            foreach (var typeDefinition in seoEnabledTypes) {
                yield return DynamicPermissions.CreateDynamicPermission(EditSeoPart, typeDefinition);
            }

            yield return ManageSeo;
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { ManageSeo }
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] { ManageSeo }
                }
            };
        }
    }
}