using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBootstrapper.Models.DesignPattern.Interfaces {
    interface IUploadableFileCategory {
        string FolderName { get; set; }
        bool IsImage { get; set; }

    }
}
