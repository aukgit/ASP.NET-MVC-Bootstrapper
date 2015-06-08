using System;
using System.IO;

namespace DevBootstrapper.Modules.Uploads {
    public enum PictureType {
        Default,
        Featured,
        SearchThumb,
        Carousel,
        CarouselThumb,
        SmallThumb,
        VerySmallThumb
    }

    public class FileLocation {
        private static readonly string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        ///     gives an absolutue path
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension">rar,jpg,tar etc (without dot)</param>
        /// <returns></returns>
        public static string GetLocation(string fileName, string fileExtension, bool isPrivate = false,
            string rootPath = "~/Uploads/Files/") {
            var path = rootPath.Replace("~", AppPath);
            if (isPrivate) {
                path += "Private/";
            }
            var file = fileName + "." + fileExtension;
            return Path.Combine(path, file).Replace('/', '\\');
        }

        /// <summary>
        ///     gives an absolutue path
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileExtension">rar,jpg,tar etc (without dot)</param>
        /// <param name="pictureType"></param>
        /// <param name="number">add numbers only if numbering enabled.</param>
        /// <param name="isNumbering">enables numbering with same file</param>
        /// <param name="isPrivate"></param>
        /// <param name="rootPath">~/Upload</param>
        /// <returns></returns>
        public static string GetImageLocation(string fileName, string fileExtension = "jpg",
            PictureType pictureType = PictureType.Default, int number = -1, bool isNumbering = false,
            bool isPrivate = false, string rootPath = "~/Uploads/Images/") {
            var path = rootPath.Replace("~", AppPath);
            var typeStr = "";

            #region Get Picture type

            switch (pictureType) {
                case PictureType.Default:
                    break;
                case PictureType.Featured:
                    typeStr = "_featured";
                    break;
                case PictureType.SearchThumb:
                    typeStr = "_searchthumb";
                    break;
                case PictureType.Carousel:
                    typeStr = "_carousel";
                    break;
                case PictureType.CarouselThumb:
                    typeStr = "_carouselthumb";
                    break;
                case PictureType.SmallThumb:
                    typeStr = "_smallthumb";
                    break;
                case PictureType.VerySmallThumb:
                    typeStr = "_verysmallthumb";
                    break;
                default:
                    break;
            }

            #endregion

            if (isNumbering) {
                fileName += typeStr + "_" + number;
            }

            fileName += "." + fileExtension;
            if (isPrivate) {
                path += "Private/";
            }
            return Path.Combine(path, fileName).Replace('/', '\\');
        }
    }
}