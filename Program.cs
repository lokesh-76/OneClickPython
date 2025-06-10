using System;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

class Program
{
    static async Task Main(string[] args)
    {
        string pythonUrl = "https://www.python.org/ftp/python/3.11.9/python-3.11.9.exe";
        string pythonInstaller = "python-3.11.9.exe";
        string pythonEXE = "C:\\Program Files (x86)\\Python311-32\\python.exe";
        string requirementsFile = @"C:\Users\abby\Desktop\ClientApk\InstallerApk\Requirements.txt";

        try
        {
            //python
            //dowloading using link
            //Console.WriteLine("Downloading Python...");
            //using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(10) })
            //{
            //    var data = await client.GetByteArrayAsync(pythonUrl);
            //    await File.WriteAllBytesAsync(pythonInstaller, data);
            //}
            Console.WriteLine("Starting download...");

            using (var httpClient = new HttpClient { Timeout = TimeSpan.FromMinutes(10) })
            using (var response = await httpClient.GetAsync(pythonUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                var canReportProgress = totalBytes != -1;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(pythonInstaller, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                {
                    var buffer = new byte[8192];
                    long totalRead = 0;
                    int bytesRead;
                    var lastProgress = -1;

                    while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                    {
                        await fileStream.WriteAsync(buffer, 0, bytesRead);
                        totalRead += bytesRead;

                        if (canReportProgress)
                        {
                            var progress = (int)((totalRead * 100) / totalBytes);
                            if (progress != lastProgress)
                            {
                                Console.Write($"\rDownloading... {progress}%");
                                lastProgress = progress;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("");
             Console.WriteLine("Installing Python...");
                    //starting installation
                    var startInfo = new ProcessStartInfo
                    {
                        FileName = pythonInstaller,
                        Arguments = "/quiet InstallAllUsers=1 PrependPath=1",
                        UseShellExecute = true,
                        Verb = "runas"
                    };
                    var process = Process.Start(startInfo);
                    process.WaitForExit();

                    Console.WriteLine("Waiting for Python to be available...");
                    //checking python version, so we get to know python installed
                    //2min for ensuring it will install even when network is slow
                    int waited = 0;
                    string versionOutput = "";
                    while (waited < 120)
                    {
                        try
                        {
                            var versionInfo = new ProcessStartInfo
                            {
                                FileName = pythonEXE,
                                Arguments = "--version",
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                UseShellExecute = false,
                                CreateNoWindow = true
                            };
                            var checkProcess = Process.Start(versionInfo);
                            versionOutput = checkProcess.StandardOutput.ReadToEnd();
                            string error = checkProcess.StandardError.ReadToEnd();
                            checkProcess.WaitForExit();

                            if (!string.IsNullOrWhiteSpace(versionOutput) || !string.IsNullOrWhiteSpace(error))
                            {
                                Console.WriteLine("Python installed: " + (string.IsNullOrWhiteSpace(versionOutput) ? error : versionOutput));
                                break;
                            }
                        }
                        catch { }

                        Thread.Sleep(5000);
                        waited += 5;

                    }

                    if (waited >= 120)
                    {
                        Console.WriteLine("Python not detected after 120 seconds.");
                        return;
                    }

                    //copying files


                    //pip
                    Console.WriteLine("Installing pip...");
                    RunCommand(pythonEXE, "-m ensurepip");

                    Console.WriteLine("Upgrading pip...");
                    RunCommand(pythonEXE, "-m pip install --upgrade pip");
                    Console.WriteLine("Pip upgraded successfully.");
            

                    Console.WriteLine("Process Completed !");
                } 
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
    //run
    static void RunCommand(string fileName, string arguments)
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine(output);
            if (!string.IsNullOrWhiteSpace(error))
                Console.Error.WriteLine(error);

            if (process.ExitCode != 0)
            {
                Console.WriteLine("Command failed with exit code " + process.ExitCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error running command: " + ex.Message);
        }
    }
}
