# PatientScansOCRBackgroundWorker

This program is for use with ARIA/Eclipse, which is a commerical radiation treatment planning software suite made by Varian Medical Systems which is used in Radiation Oncology. This is one of several programs which I have made while working in the Radiation Oncology department at Lahey Hospital and Medical Center in Burlington, MA. I have licensed it under GPL V3 so it is open-source and publicly available.

There is also a .docx README file in the repo that describes what the program does and how it is organized.

Believe it or not, PatientScansOCRBackgroundWorker is a literal Windows service, as in a service that runs in the background of the Windows OS.

While running under a user account with the proper permissions, it monitors the ARIA documents folder (on the clinic's LAN)
for any changes that occur, for example clinic staff scanning a new patient document, such as a consent form.

When something like that happens, PatientScansOCRBackgroundWorker then starts a new process that runs the Tesseract Execute program (separate repo) as an executable file.

Tesseract Execute uses the Tesseract OCR library to basically convert the scanned patient documents stored in the ARIA dcouments folder to simple text files so that they are available for use by the Document Check Module of the Plancheck program (separate repo).

This file watcher service is essentially a massive workaround because I was not allowed to install the .NET C++ runtime needed to run the Tesseract library (which is a C# wrapper for a C++ program) on the ARIA file server.