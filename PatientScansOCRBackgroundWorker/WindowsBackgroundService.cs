using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PatientScansOCRBackgroundWorker
{
    public sealed class WindowsBackgroundService : BackgroundService
    {
        private readonly ILogger<WindowsBackgroundService> _logger;
        private readonly ITesseract_OCR _tesseract_OCR;

        public WindowsBackgroundService(ITesseract_OCR tesseract_OCR, ILogger<WindowsBackgroundService> logger)
        {
            _tesseract_OCR = tesseract_OCR;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _tesseract_OCR.FileWatchStarted = false;
            while (!stoppingToken.IsCancellationRequested)
            {
                if(_tesseract_OCR.FileWatchStarted == false)
                {
                    _tesseract_OCR.StartFileSystemWatch();
                }
               
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
