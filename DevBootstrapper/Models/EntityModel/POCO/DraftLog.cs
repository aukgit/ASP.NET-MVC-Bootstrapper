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
    public partial class DraftLog
    {
        public long DraftLogID { get; set; }
        public long ArticleID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Page1 { get; set; }
        public string Page2 { get; set; }
        public string Page3 { get; set; }
        public string Page4 { get; set; }
    
        public virtual Article Article { get; set; }
    }
}