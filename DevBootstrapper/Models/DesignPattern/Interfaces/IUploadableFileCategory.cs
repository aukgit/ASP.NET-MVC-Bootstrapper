namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    public interface IUploadableFileCategory {
        string CategoryName { get; set; }

        /// <summary>
        ///     where the files should be placed after conversion.
        /// </summary>
        string FolderName { get; set; }

        /// <summary>
        ///     Where the files should be uploaded
        /// </summary>
        string TempFolderName { get; set; }

        bool IsImage { get; set; }
        bool IsTempFoldeNecessary { get; set; }

        /// <summary>
        ///     If true then only upload files which are possible from the extensions table
        /// </summary>
        bool OnlyAcceptsKnownFileExtensions { get; set; }

        long MaxSizeInMegaBytes { get; set; }
        string GetRelativeFolderPath();
    }
}