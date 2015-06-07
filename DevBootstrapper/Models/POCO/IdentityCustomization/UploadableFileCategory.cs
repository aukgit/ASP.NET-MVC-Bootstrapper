#region using block

using System;
using System.Collections.Generic;
using DevBootstrapper.Models.DesignPattern.Interfaces;

#endregion

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class UploadableFileCategory : IUploadableImageCategory {
        public double Width { get; set; }
        public double Height { get; set; }
        public bool IsThumbsInSameFolder { get; set; }
        public ICollection<IUploadableImageCategory> ThumbsCollections { get; set; }
        public string CategoryName { get; set; }
        public string FolderName { get; set; }
        public string TempFolderName { get; set; }
        public bool IsImage { get; set; }
        public bool IsTempFoldeNecessary { get; set; }
        public bool OnlyAcceptsKnownFileExtensions { get; set; }
        public long MaxSizeInMegaBytes { get; set; }

        public string GetRelativeFolderPath() {
            throw new NotImplementedException();
        }
    }
}