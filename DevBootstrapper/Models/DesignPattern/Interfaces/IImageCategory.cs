using System.Collections.Generic;

namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    public interface IImageCategory {
        string CategoryName { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        
        bool IsThumbsInSameFolder { get; set; }
        string GetRelativeFolderName();

        ICollection<IImageCategory> ThumbsCollections { get; set; }
    }
}