using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PatientScansOCRBackgroundWorker;



using IHost host = Host.CreateDefaultBuilder(args).UseWindowsService(options =>
{
    options.ServiceName = "ZTM Patient Scan OCR Service";
}).ConfigureServices(services =>
{
    services.AddHostedService<WindowsBackgroundService>().AddScoped<ITesseract_OCR, Tesseract_OCR>();
}).Build();


await host.RunAsync();

