#region using block

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class NavigationItem {
        public int NavigationItemId { get; set; }
        public int NavigationId { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(250)]
        [Display(Name = "Element Classes", Description = "Html classes related to this list-item.")]
        public string ElementClasses { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(200)]
        [Display(Name = "Element ID",
            Description =
                "Html ID related to this list-item. Prefer not to use because classes are the mordern practice.")]
        public string ElementId { get; set; }

        [Required]
        [StringLength(600)]
        public string RelativeUrl { get; set; }

        public int Ordering { get; set; }
        public bool HasDropDown { get; set; }
        public int? ParentNavigationId { get; set; }
    }
}