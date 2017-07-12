using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Users.Models {
    public class AppIdentityDbContext : IdentityDbContext<AppUser> {
        public AppIdentityDbContext() : base("HygieneContext") {

        }

        static AppIdentityDbContext() {
            Database.SetInitializer(new IdentityDbInit());
        }

        // OWIN规范
        public static AppIdentityDbContext Create() {
            return new AppIdentityDbContext();
        }
    }

    public class IdentityDbInit : DropCreateDatabaseIfModelChanges<AppIdentityDbContext> {
        protected override void Seed(AppIdentityDbContext context) {
            PerformInitialSetup(context);
            base.Seed(context);
        }
        public void PerformInitialSetup(AppIdentityDbContext context) {
            // initial configuration will go here
            // 初始化配置将放在这儿
        }
    }
}