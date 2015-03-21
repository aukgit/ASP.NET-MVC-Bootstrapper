using DevBootstrapper.Models.DesignPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevBootstrapper.Models.POCO.IdentityCustomization {
    public class UploadableFile : IUploadableFile {
        public Guid FileUploadId { get; set; }

        public short Sequence { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Extension { get; set; }

        public string GetLocation() {
            throw new NotImplementedException();
        }

        public string GetFileName() {
            throw new NotImplementedException();
        }

        public string GetFileNameWithExtension() {
            throw new NotImplementedException();
        }

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