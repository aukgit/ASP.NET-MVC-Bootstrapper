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
    
    public partial class RelatedArticle
    {
        public long RelatedArticleID { get; set; }
        public long ArticleID { get; set; }
        public long RelArticleID { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Article Article1 { get; set; }
    }
}
