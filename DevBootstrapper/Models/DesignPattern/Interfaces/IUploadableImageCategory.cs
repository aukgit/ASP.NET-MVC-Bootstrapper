using System.Collections.Generic;

namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    public interface IUploadableImageCategory : IUploadableFileCategory {
        double Width { get; set; }
        double Height { get; set; }
        
        bool IsThumbsInSameFolder { get; set; }

        ICollection<IUploadableImageCategory> ThumbsCollections { get; set; }
    }
}