# Python Installer App

This project is a **Windows desktop installer application** that simplifies Python environment setup and client code deployment.  
It automates Python installation, dependency setup, background task registration, and system configuration — making it suitable for end-users or client systems.

## Features

- Installs Python automatically
- Installs dependencies from `requirements.txt`
- Deploys the client Python script
- Registers the script to run at system startup
- Handles User Account Control (UAC) settings
- Reboots the system if required

## Project Structure

```
InstallerApk/
├── requirements.txt
├── client_script.py       # Main Python client code
├── PythonInstaller.cs     # Main C# installer logic
├── Resources/
│   └── python-3.xx.exe    # Embedded Python installer
└── InstallerApk.exe       # Final installer binary
```

## How It Works

1. Checks if Python is already installed
2. Installs Python silently from the embedded installer if not found
3. Installs dependencies listed in `requirements.txt`
4. Copies the client script to the target location
5. Adds a registry key or Task Scheduler entry to run the script at startup
6. Adjusts permissions and reboots the system if necessary

## Setup for Development

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) with **.NET Desktop Development** workload
- [.NET Framework](https://dotnet.microsoft.com/) (for WinForms/WPF applications)
- Python executable for embedding

### Building the Application

1. Open the solution in Visual Studio
2. Set build configuration to **Release**
3. Build the solution
4. The output file `InstallerApk.exe` will be located in the `bin/Release` folder

## Deployment

To distribute the application:

- Provide the `InstallerApk.exe` directly to end-users
- Alternatively, upload it to the [Releases](https://github.com/LokeshAdivishnu/python_installer/releases) section of the repository

## Notes

- Avoid pushing `.exe` files directly to Git unless tracked via **Git LFS**
- For larger distributions, consider packaging the installer with **Inno Setup** or **NSIS**

## License

MIT License — free to use and adapt

---

**Created by Lokesh Adivishnu**
