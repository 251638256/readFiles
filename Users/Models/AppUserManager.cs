using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Users.Models {
    public class AppUserManager : UserManager<AppUser> {
        public AppUserManager(IUserStore<AppUser> store): base(store) {

        }

        // 依赖注入??
        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context) {
            AppIdentityDbContext db = context.Get<AppIdentityDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<AppUser>(db));

            // 设置密码验证策略
            //manager.PasswordValidator = new PasswordValidator {
            //    RequiredLength = 1,
            //    RequireNonLetterOrDigit = false,
            //    RequireDigit = false,
            //    RequireLowercase = false,
            //    RequireUppercase = false
            //};

            // 自定义的密码验证策略
            manager.PasswordValidator = new CustomPasswordValidator {
                RequiredLength = 1,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };

            // 用户名验证策略
            manager.UserValidator = new UserValidator<AppUser>(manager) {
                AllowOnlyAlphanumericUserNames = true, // 只能有字母和数字
                RequireUniqueEmail = true // 只能唯一的电子邮件
            };

            return manager;
        }
    }
}