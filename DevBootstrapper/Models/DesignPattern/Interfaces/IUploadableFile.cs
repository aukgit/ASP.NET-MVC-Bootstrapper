using System;

namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    public interface IUploadableFile : IUploadableImageCategory {
        Guid FileUploadId { get; set; }
        //IImageCategory Category { get; }
        short Sequence { get; set; }
        string Title { get; set; }
        string Subtitle { get; set; }
        string Extension { get; set; }

        string GetLocation();
        string GetFileName();
        string GetFileNameWithExtension();
    }
}