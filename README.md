# Python Installer App 🐍🖥️

This project is a **Windows desktop installer application** that simplifies Python environment setup and client code deployment. It automates Python installation, required packages, background task registration, and system configuration — making it ideal for end-users or client systems.

## 💡 Features

- ✅ Installs **Python** automatically
- 📦 Installs dependencies from `requirements.txt`
- 🚀 Deploys your **client Python script**
- 🔁 Registers the script to **run at system startup**
- 🔐 Handles **UAC (User Account Control)** settings
- 🔄 Reboots system if needed

## 📁 Project Structure

InstallerApk/
├── requirements.txt
├── client_script.py # Your main Python client code
├── PythonInstaller.cs # Main C# installer logic
├── Resources/
│ └── python-3.xx.exe # Embedded Python installer
└── InstallerApk.exe # Final installer binary


## 🚀 How It Works

1. Checks if Python is already installed.
2. If not, installs it silently from the embedded `.exe`.
3. Installs dependencies listed in `requirements.txt`.
4. Copies the client script to a target location.
5. Adds registry key or Task Scheduler entry to run at startup.
6. Adjusts permissions and reboots the system (if required).

## 🔧 Setup for Development

### Prerequisites:
- [Visual Studio](https://visualstudio.microsoft.com/) with **.NET Desktop Development**
- [.NET Framework](https://dotnet.microsoft.com/) (for WinForms/WPF apps)
- Python executable for embedding

### Building the App:
1. Open the solution in Visual Studio
2. Configure build settings to **Release**
3. Build the solution
4. `InstallerApk.exe` will be generated inside the `bin/Release` folder

## 📦 Deployment

To distribute, provide the `InstallerApk.exe` to end-users or upload it under the [Releases](https://github.com/LokeshAdivishnu/python_installer/releases) tab.

## 📌 Notes

- Do not push `.exe` files directly in Git unless tracked via **Git LFS**
- For larger distribution, consider packaging with **Inno Setup** or **NSIS**

## 📄 License

MIT License — feel free to use and adapt.

---

Made with ❤️ by [Lokesh Adivishnu](https://github.com/LokeshAdivishnu)
