//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevBootstrapper.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Language
    {
        public Language()
        {
            this.Articles = new HashSet<Article>();
        }
    
        public byte LanguageID { get; set; }
        public byte AspNETLanguageID { get; set; }
    
        public virtual ICollection<Article> Articles { get; set; }
    }
}
