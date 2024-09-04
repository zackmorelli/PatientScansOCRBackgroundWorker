using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Spire.Pdf;
using Tesseract;

namespace PatientScansOCRBackgroundWorker
{
    public class Tesseract_OCR : ITesseract_OCR
    {
        public bool FileWatchStarted { get; set; }

        public void StartFileSystemWatch()
        {
            FileSystemWatcher fileWatch = new FileSystemWatcher();
            fileWatch.Path = @"\\wvariafssp01ss\VA_DATA$\Documents";
            fileWatch.Filter = "*.*";
            fileWatch.NotifyFilter = NotifyFilters.LastWrite;
            fileWatch.IncludeSubdirectories = true;
            fileWatch.Changed += FileWatch_Changed;
            fileWatch.EnableRaisingEvents = true;
            FileWatchStarted = true;
        }

        private void Execute(string filePath)
        {
            System.Drawing.Image[] images;
            using (PdfDocument pdf = new PdfDocument(filePath))
            {
                images = pdf.Pages[0].ExtractImages();
            }

            if (images.Length > 0)
            {
                //we know each page only has one image, because it is a scan saved as a pdf
                 System.Drawing.Image im = images[0];
                int k = filePath.LastIndexOf("\\");
                string fname = filePath.Substring(k + 1);
                fname.Replace("pdf", "tif");
                im.Save(@"\\wvariafssp01ss\VA_DATA$\ProgramData\Vision\Tesseract_OCR_Files\Images\Image" + fname);

                Process process = new Process();
                process.StartInfo.FileName = @"C:\prog\Tesseract_Execute\Tesseract_Execute\bin\Debug\Tesseract_Execute.exe";
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.Arguments = @"\\wvariafssp01ss\VA_DATA$\ProgramData\Vision\Tesseract_OCR_Files\Images\Image" + fname;
                process.Start();
            }
        }

        private void FileWatch_Changed(object sender, FileSystemEventArgs e)
        {
            if(e.FullPath.EndsWith("pdf"))
            {
                Execute(e.FullPath);
            }
        }
    }
}
