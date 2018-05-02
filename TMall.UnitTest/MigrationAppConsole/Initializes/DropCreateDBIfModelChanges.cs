using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using TMall.DomainModels.Management;
using TMall.DomainModule.Enums;

namespace MigrationAppConsole.Initializes
{
    public class DropCreateDBIfModelChanges : DropCreateDatabaseIfModelChanges<MigrationDbContext>
    {
        protected override void Seed(MigrationDbContext context)
        {
            base.Seed(context);
        }
    }
}
