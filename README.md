AzureStorageSync
==========

[![Join the chat at https://gitter.im/CatenaLogic/AzureStorageSync](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/CatenaLogic/AzureStorageSync?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

![License](https://img.shields.io/github/license/catenalogic/azurestoragesync.svg)
![NuGet downloads](https://img.shields.io/nuget/dt/azurestoragesync.svg)
![Version](https://img.shields.io/nuget/v/azurestoragesync.svg)
![Pre-release version](https://img.shields.io/nuget/vpre/azurestoragesync.svg)
![Chocolatey count](https://img.shields.io/chocolatey/dt/azurestoragesync.svg)
![Chocolatey version](https://img.shields.io/chocolatey/v/azurestoragesync.svg)

![AzureStorageSync](design/logo/logo_64.png)

AzureStorageSync let's users synchronize a local directory with an Azure storage container via the command line.

This library is created because I prefer not to mess around with powershell. This tool is a command line wrapper around the Azure SDK.

<a href="https://pledgie.com/campaigns/27034"><img alt="Click here to lend your support to: AzureStorageSync and make a donation at pledgie.com !" src="https://pledgie.com/campaigns/27034.png?skin_name=chrome" border="0" /></a>

-- 


# How to use

To use this tool, use the following command line:

	AzureStorageSync.exe [localDirectory] [remoteDirectory] -c [connectionString]

For example:

	AzureStorageSync.exe C:\TestDirectory /testcontainer/testdirectory -c DefaultEndpointsProtocol=https;AccountName=youraccountname;AccountKey=youraccountkey

The Azure container will automatically be determined from the remote directory. The string */testcontainer/testdirectory/subdirectory* will result in the following information:

* Container name: *testcontainer*
* Root directory *testdirectory/subdirectory*
