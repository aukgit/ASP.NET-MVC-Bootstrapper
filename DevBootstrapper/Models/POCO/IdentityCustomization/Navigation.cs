﻿#region using block

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class Navigation {
        public int NavigationId { get; set; }

        /// <summary>
        ///     Find navigation by.
        /// </summary>
        [Column(TypeName = "VARCHAR")]
        [StringLength(30)]
        public string Name { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Display(Name = "Element Classes", Description = "Html classes related to this unordered-list.")]
        public string ElementClasses { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Display(Name = "Element ID",
            Description =
                "Html ID related to this unordered-list. Prefer not to use because classes are the mordern practice.")]
        public string ElementId { get; set; }

        [ForeignKey("NavigationID")]
        public virtual ICollection<NavigationItem> NavigationItems { get; set; }

        [ForeignKey("ParentNavigationID")]
        public virtual ICollection<NavigationItem> NestedOneSubNavigationItems { get; set; }
    }
}