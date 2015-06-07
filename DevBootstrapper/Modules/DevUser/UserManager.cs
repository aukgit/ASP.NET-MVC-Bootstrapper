﻿#region using block

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DevBootstrapper.Application;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.Identity;
using DevBootstrapper.Models.POCO.IdentityCustomization;
using DevBootstrapper.Models.ViewModels;
using DevBootstrapper.Modules.Cache;
using DevBootstrapper.Modules.Role;
using DevBootstrapper.Modules.Session;
using DevBootstrapper.Modules.UserError;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

#endregion

namespace DevBootstrapper.Modules.DevUser {
    public class UserManager {
        #region Authentication

        public static bool IsAuthenticated() {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        #endregion

        #region External Validations

        /// <summary>
        ///     External Validations
        ///     Register Code Validation
        /// </summary>
        /// <param name="model"></param>
        public static async Task<bool> ExternalUserValidation(RegisterViewModel model, ApplicationDbContext db,
            ErrorCollector errors = null) {
            var validOtherConditions = true;
            if (errors == null) {
                errors = new ErrorCollector();
            }
            if (!AppVar.Setting.IsRegisterCodeRequiredToRegister) {
                model.RegistraterCode = Guid.NewGuid();
                model.Role = -1;
            } else {
                var regCode =
                    db.RegisterCodes.FirstOrDefault(
                        n =>
                            n.IsUsed == false && n.RoleId == model.Role && n.RegisterCodeId == model.RegistraterCode &&
                            !n.IsExpired);
                if (regCode != null) {
                    if (regCode.ValidityTill <= DateTime.Now) {
                        // not valid
                        regCode.IsExpired = true;
                        errors.AddMedium(MessageConstants.RegistercCodeExpired, MessageConstants.SolutionContactAdmin);
                        await db.SaveChangesAsync();
                        validOtherConditions = false;
                    }
                } else {
                    errors.AddMedium(MessageConstants.RegistercCodeNotValid, MessageConstants.SolutionContactAdmin);
                    validOtherConditions = false;
                }
            }

            //validation for country language
            var languages = CachedQueriedData.GetLanguages(model.CountryId, 0);
            if (languages == null) {
                //select english as default.
                model.CountryLanguageId = CachedQueriedData.GetDefaultLanguage().CountryLanguageId;
            } else if (languages.Count > 1) {
                //it should be selected inside the register panel.
                validOtherConditions = !(model.CountryLanguageId == 0); //if zero then false.
                errors.AddMedium("You forgot you set your language.");
            } else if (languages.Count == 1) {
                model.CountryLanguageId = languages[0].CountryLanguageId;
            }

            //validation for country timzone
            var timezones = CachedQueriedData.GetTimezones(model.CountryId, 0);
            if (timezones != null && timezones.Count > 1) {
                //it should be selected inside the register panel.
                validOtherConditions = !(model.UserTimeZoneId == 0); //if zero then false.
                errors.AddMedium("You forgot you set your time zone.");
            } else if (timezones.Count == 1) {
                model.UserTimeZoneId = timezones[0].UserTimeZoneId;
            } else {
                validOtherConditions = false;
                errors.AddMedium(
                    "You time zone not found. Please contact with admin and notify him/her about the issue to notify developer.");
            }


            if (!validOtherConditions) {
                AppConfig.SetGlobalError(errors);
            }
            return validOtherConditions;
        }

        #endregion

        #region Registration Code

        public void LinkUserWithRegistrationCode(ApplicationUser user, Guid code) {
            if (user != null) {
                var relation = new RegisterCodeUserRelation {
                    UserId = user.Id,
                    RegisterCodeUserRelationId = code
                };
                using (var db = new ApplicationDbContext()) {
                    db.RegisterCodeUserRelations.Add(relation);
                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region Complete Registration

        /// <summary>
        ///     Also make the EmailConfirmed = IsRegistrationComplete = true;
        ///     If first user then add all the roles.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="getRoleFromRegistration">Try get the role from the registration</param>
        /// <param name="role">has more priority than 'getRoleFromRegistration'. Override registration role.</param>
        public static void CompleteRegistration(long userId, bool getRoleFromRegistration, string role = null) {
            RegistrationCustomCode.CompletionBefore(userId, getRoleFromRegistration, role);
            using (var db2 = new ApplicationDbContext()) {
                var user = db2.Users.Find(userId);
                if (user != null) {
                    user.IsRegistrationComplete = true;
                    user.EmailConfirmed = true;
                    db2.SaveChanges();
                    if (!AppConfig.Setting.IsFirstUserFound) {
                        // first user not found yet.
                        // first user is admin
                        var getHigestPriority = db2.Roles.Min(n => n.PriorityLevel);
                        var getHigestPriorityRole = db2.Roles.FirstOrDefault(n => n.PriorityLevel == getHigestPriority);
                        RoleManager.AddUnderlyingRoles(user, getHigestPriorityRole.Name);
                        using (var db3 = new DevIdentityDbContext()) {
                            var setting = db3.CoreSettings.FirstOrDefault();
                            setting.IsFirstUserFound = true;
                            db3.SaveChanges(setting);
                            AppConfig.RefreshSetting();
                        }
                    } else {
                        if (getRoleFromRegistration) {
                            if (role != null) {
                                RoleManager.AddUnderlyingRoles(user, role);
                            } else {
                                var appRole = RoleManager.ReturnRoleIdFromTempInfoAndRemoveTemp(userId);
                                if (appRole != null) {
                                    RoleManager.AddUnderlyingRoles(user, appRole.Result.Name);
                                }
                            }
                        }
                    }
                }
            }
            RegistrationCustomCode.CompletionAfter(userId, getRoleFromRegistration, role);
        }

        #endregion

        #region Save User

        /// <summary>
        ///     Change current user record.
        /// </summary>
        /// <returns></returns>
        public static bool UpdateUser(ApplicationUser user) {
            using (var db = new ApplicationDbContext()) {
                db.Entry(user).State = EntityState.Modified;
                var i = db.SaveChanges();
                if (i == 0) {
                    return false;
                }
                return true;
            }
        }

        #endregion

        #region Remove user from session.

        public static void RemoveSessionUsersSession() {
            HttpContext.Current.Session[SessionNames.LastUser] = null;
            HttpContext.Current.Session[SessionNames.User] = null;
        }

        #endregion

        #region Get Every User

        /// <summary>
        ///     Get all the list of users.
        /// </summary>
        /// <returns>Returns all stored users in the database.</returns>
        public static List<ApplicationUser> GetAllUsers() {
            return Manager.Users.ToList();
        }

        /// <summary>
        ///     Get all the list of users.
        /// </summary>
        /// <returns>Returns all stored users as IQueryable for pagination.</returns>
        public static IQueryable<ApplicationUser> GetAllUsersAsIQueryable() {
            return Manager.Users;
        }

        #endregion

        #region Declaration

        private static ApplicationUserManager _userManager;

        public static ApplicationUserManager Manager {
            get {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set { _userManager = value; }
        }

        #endregion

        #region Asynchronous Operation

        public static async Task<ApplicationUser> GetUserAsync(string userName, string password) {
            return await Manager.FindAsync(userName, password);
        }

        public static async Task<ApplicationUser> GetUserByEmailAsync(string email, string password) {
            var user = GetUserFromSessionByEmail(email);
            if (user == null) {
                //not found in cache
                user = await Manager.FindByEmailAsync(email);
                if (user != null) {
                    user = await Manager.FindAsync(user.UserName, password);
                    SaveUserInSession(user);
                    return user;
                }
                return null;
            }
            user = await Manager.FindAsync(user.UserName, password);
            return user;
        }

        #endregion

        #region Get User

        public static ApplicationUser GetUser(long userid) {
            var user = GetUserFromSession(userid);
            if (user == null) {
                user = Manager.FindById(userid);
                SaveUserInSession(user);
            }
            return user;
        }


        public static ApplicationUser GetUser(string userName, string password) {
            var user = GetUserFromSession(userName);
            if (user == null) {
                user = Manager.Find(userName, password);
                SaveUserInSession(user);
            }
            return user;
        }

        public static ApplicationUser GetUserByEmail(string email, string password) {
            var user = Manager.FindByEmail(email);
            return Manager.Find(user.UserName, password);
        }

        public static string GetCurrentUserName() {
            if (HttpContext.Current.User.Identity.IsAuthenticated) {
                return HttpContext.Current.User.Identity.Name;
            }
            return null;
        }

        public static ApplicationUser GetUser(string username) {
            var user = GetUserFromSession(username);
            if (user == null) {
                user = Manager.FindByName(username);
                SaveUserInSession(user);
            }
            return user;
        }

        public static ApplicationUser GetUserFromSession() {
            var userSession = HttpContext.Current.Session[SessionNames.User];
            if (userSession != null) {
                return (ApplicationUser) userSession;
            }
            userSession = HttpContext.Current.Session[SessionNames.LastUser];
            if (userSession != null) {
                return (ApplicationUser) userSession;
            }
            return null;
        }

        public static ApplicationUser GetUserFromSessionByEmail(string email) {
            var user = GetUserFromSession();
            if (user != null && user.Email != null && email != null && user.Email.ToLower() == email.ToLower()) {
                return user;
            }
            return null;
        }

        public static ApplicationUser GetUserFromSession(string username) {
            var user = GetUserFromSession();
            if (user != null && user.Email != null && username != null && user.Email.ToLower() == username.ToLower()) {
                return user;
            }
            return null;
        }

        public static ApplicationUser GetUserFromSession(long userId) {
            var user = GetUserFromSession();
            if (user != null && user.UserId == userId) {
                return user;
            }
            return null;
        }

        public static ApplicationUser GetUserFromViewModel(RegisterViewModel model) {
            var user = new ApplicationUser {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                CreatedDate = DateTime.Now,
                EmailConfirmed = false,
                PhoneNumber = model.Phone,
                CountryId = model.CountryId,
                CountryLanguageId = model.CountryLanguageId,
                UserTimeZoneId = model.UserTimeZoneId,
                IsRegistrationComplete = false,
                GeneratedGuid = Guid.NewGuid()
            };


            return user;
        }

        /// <summary>
        ///     Return current user in optimized fashion.
        /// </summary>
        /// <returns></returns>
        public static ApplicationUser GetCurrentUser() {
            var username = GetCurrentUserName();
            if (username != null) {
                var user = GetUserFromSession(username);
                if (user == null) {
                    user = GetUser(username);
                    SaveCurrentUser(user);
                    return user;
                }
            }
            return null;
        }

        #endregion

        #region User Exist check with Email or Username

        public static bool IsUserNameExist(string username) {
            return Manager.Users.Any(n => n.UserName == username);
        }

        public static bool IsEmailExist(string email) {
            return Manager.Users.Any(n => n.Email == email);
        }

        #endregion

        #region Save Current user into session.

        /// <summary>
        ///     Save only current user in session
        /// </summary>
        /// <param name="user"></param>
        public static void SaveCurrentUser(ApplicationUser user) {
            HttpContext.Current.Session[SessionNames.User] = user;
        }

        /// <summary>
        ///     Save last queried user in session.
        /// </summary>
        /// <param name="user"></param>
        public static void SaveUserInSession(ApplicationUser user) {
            HttpContext.Current.Session[SessionNames.LastUser] = user;
        }

        #endregion
    }
}