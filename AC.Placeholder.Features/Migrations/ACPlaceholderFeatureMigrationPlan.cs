using System;
using Umbraco.Cms.Core.Packaging;

namespace AC.Placeholder.Features.Migrations
{
    public class ACPlaceholderFeatureMigrationPlan : PackageMigrationPlan
    {
        private static string PackageName = "AC.Placeholder.Features";

        public ACPlaceholderFeatureMigrationPlan() : base(PackageName)
        {

        }

        protected override void DefinePlan()
        {
            To<ImportPackageXmlMigration>(new Guid("9D0D9F48-AE88-424C-9FE2-91E9C3C52239"));
        }
    }
}
