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
    
    public partial class SendArticle
    {
        public long SendArticleID { get; set; }
        public long ArticleID { get; set; }
        public bool IsProcessed { get; set; }
        public double GainedXP { get; set; }
        public long WrittenByUserID { get; set; }
        public Nullable<long> ProcessedByUserId { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}