using System;
using System.Web;

namespace DevBootstrapper.Modules.Uploads {
    public class UploadProcessor {
        public bool UploadFile(HttpPostedFileBase submittedFile, string fileName = null, string fileExtension = null,
            PictureType pictureType = PictureType.Default, int number = -1, bool isNumbering = false,
            bool isPrivate = false, string rootPath = "~/Uploads/Images/") {
            try {
                if (fileName == null) {
                    fileName = submittedFile.FileName;
                }
                var absFileLocation = FileLocation.GetLocation(fileName, fileExtension, isPrivate, rootPath);
                submittedFile.SaveAs(absFileLocation);
                return true;
            } catch (Exception) {
                throw new Exception("File can't be uploaded.");
            }
        }

        public bool UploadImageFile(HttpPostedFileBase submittedFile, string fileName, string fileExtension = "jpg",
            PictureType pictureType = PictureType.Default, int number = -1, bool isNumbering = false,
            bool isPrivate = false, string rootPath = "~/Uploads/Images/") {
            try {
                if (fileName == null) {
                    fileName = submittedFile.FileName;
                }
                var absFileLocation = FileLocation.GetImageLocation(fileName, fileExtension, pictureType, number,
                    isNumbering, isPrivate, rootPath);
                submittedFile.SaveAs(absFileLocation);
                return true;
            } catch (Exception) {
                throw new Exception("File can't be uploaded.");
            }
        }
    }
}