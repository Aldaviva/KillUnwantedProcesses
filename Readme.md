KillUnwantedProcesses
===

This is a program which finds specific running processes on your computer and kills them.

## Benefits
- Reduce memory usage
- Clean up after another program exits
- Easier to read process list
- Get rid of unwanted tray icons
- Prevent unwanted services from being reenabled after you install an update

## Features
- Stop or change the start mode of Windows services (automatic, manual, or disabled)
- Uninstall AppX packages
- Avoid killing a process if certain other processes are already running
- Determine whether or not a process is suspended
- When killing one process, reevaluate other processes to see if they should also be killed now too, even if they weren't before.

## Requirements
- [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework)

## Installation
1. Download [`KillUnwantedProcesses.exe`](https://github.com/Aldaviva/KillUnwantedProcesses/releases/latest/download/KillUnwantedProcesses.exe) from the [latest release](https://github.com/Aldaviva/KillUnwantedProcesses/releases/latest/).
1. Use Task Scheduler (`taskschd.msc`) to create a scheduled task that runs this program as often as you want.
    - I run it every 5 minutes.

## Programs
- Acrobat Notification Service
- AcroTray
- Adobe Acrobat Updater
- Adobe Collaboration Synchronizer
- Adobe Crash Processor
- Adobe Creative Cloud Experience
- Adobe Creative Cloud Libraries
- Adobe Desktop Service
- Adobe Extensions Service
- Adobe Genuine Monitor Service
- Adobe Genuine Software Integrity Service
- Adobe Notification Client
- Adobe Sync
- Adobe Update Service
- Autodesk Access Service
- Autodesk Licensing Service
- FlexNet Licensing Service
- Logitech G Hub System Tray
- Nvidia Control Panel
- Office Document Cache
- Search App Edge WebView
- Virtual CloneDrive
- Visual Studio Compiler
- VMware Authorization Service
- VMware DHCP Service
- VMware NAT Service
- VMware Workstation Server

*Some of these are only killed if certain conditions are met, others are killed whenever they're running or installed.*