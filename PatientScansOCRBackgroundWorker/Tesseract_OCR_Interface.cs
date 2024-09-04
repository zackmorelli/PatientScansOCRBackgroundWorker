using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientScansOCRBackgroundWorker
{
    public interface ITesseract_OCR
    {
        bool FileWatchStarted { get; set; }

        void StartFileSystemWatch()
        {

        }

        void Execute(string filePath)
        {

        }

        void FileWatch_Changed(object sender, FileSystemEventArgs e)
        {

        }


    }
}
