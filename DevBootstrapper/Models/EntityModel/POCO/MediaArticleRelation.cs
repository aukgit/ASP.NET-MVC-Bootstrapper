//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevBootstrapper.Models.EntityModel.POCO
{
    public partial class MediaArticleRelation
    {
        public long MediaArticleRelationID { get; set; }
        public long MediaID { get; set; }
        public long ArticleID { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual Medium Medium { get; set; }
    }
}