using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using DevBootstrapper.Models.Context;
using DevBootstrapper.Models.POCO.IdentityCustomization;

namespace DevBootstrapper.Modules.Menu {
    public class GenerateMenu : IDisposable {
        /// <summary>
        ///     0 - name
        ///     1 - drop li items
        /// </summary>
        private const string DropDownTemplate = @"<li class='dropdown'>
                                            <a href='#' class='dropdown-toggle' data-toggle='dropdown'>{0} <span class='caret'></span></a>
                                            <ul class='dropdown-menu' role='menu'>
                                              {1}
                                            </ul>
                                          </li>";

        private const string DropDownUlTemplete = @"<ul class='dropdown-menu' role='menu'>
                                              {0}
                                            </ul>";

        /// <summary>
        ///     0-href
        ///     1-title attribute
        ///     2-Link display text
        /// </summary>
        private const string HtmlListItem = @"<li title='{1}'><a href='{0}' title='{1}'>{2}</a></li>";

        private readonly StringBuilder _sb = new StringBuilder(150);
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        public List<NavigationItem> Items { get; set; }

        public void Dispose() {
            _db.Dispose();
        }

        public Navigation GetMenuItem(string menuName) {
            return _db.Navigations.Include(w => w.NavigationItems).FirstOrDefault(n => n.Name == menuName);
        }

        public Navigation GetMenuItem(int navigationId) {
            return _db.Navigations.Include(w => w.NavigationItems).FirstOrDefault(n => n.NavigationID == navigationId);
        }

        /// <summary>
        ///     Not recursive yet.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="hasDropDown"></param>
        /// <returns></returns>
        public string GenerateRecursiveMenuItems(List<NavigationItem> list, bool firstTime = true,
            bool hasDropDown = false) {
            if (firstTime) {
                _sb.Clear();
            }
            if (list.Count > 0) {
                var appUrl = AppVar.Url;

                foreach (var item in list) {
                    var url = appUrl + item.RelativeURL;
                    _sb.Append(string.Format(HtmlListItem, url, item.Title, item.Title));
                }
            }
            return _sb.ToString();
        }
    }
}