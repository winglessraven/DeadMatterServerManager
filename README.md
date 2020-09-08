# Dead Matter Server Manager
A GUI server manager for Dead Matter dedicated servers.  Idea was to create a simple and clean interface for managing dedicated servers and the associated configurations withouth manually needing to locate and edit different ini files.  The application is constantly being updated and I welcome any ideas/suggestions for improvements along with bug reports.

You can submit requests and/or issues [here](https://github.com/winglessraven/DeadMatterServerManager/issues/new), or drop me a message on Discord @winglessraven#4334

![GitHub All Releases](https://img.shields.io/github/downloads/winglessraven/deadmatterservermanager/total?style=social)

## Installer File
* Installation file can be found under [releases](https://github.com/winglessraven/DeadMatterServerManager/releases)
* Grab the latest .msi file and run to install the application
* If you are greeted with a 'Windows Smart Screen' pop-up, click 'more info' and 'run anyway'.  Only way for me to get rid of this pop-up is to pay a fortune to get certified
* Once the application is installed follow the 'first run steps' to get up and running

## First run steps

<b>If you plan to install via SteamCMD</b>
* Set your SteamCMD path (if you don't have it, choose a folder to install to)
* Update SteamCMD
* Set your Server Folder path (if you don't have it, choose a folder to install to)
* Enter your Steam ID and Password (these are used for getting the server files via SteamCMD and are not stored anywhere)
* Update Server

<b>If you plan to install via Steam Client (note, currently this seems to cause less issues when running the server)</b>
* Locate 'Dead Matter Dedicated Server' in your Steam library under Tools
* Install and run once from your library.  Once the server has fully loaded, close it down
* Open up the Server Manager and point the Server Folder path to your install location

## Configuration
* If you already have a server configuration click *Get Config* to populate the settings table with your current settings
* Modify the settings as required
* Add admins/whitelist players via the *Admin/Whitelist* tab
* Save Config to write your changes to the server configuration files
* Enter your maximum memory for the server, when the server reaches this value it will restart.  If you do not set a value the server will just run until it crashes and will automatically restart
* If you wish to restart the server on a timer, set the number of minutes you want your server to run for before restarting

## Start Server
* Starting the server will launch a new window with the server output.
* If the server crashes for any reason the application will automatically start it again.
* If you want to stop the server, click *Stop Server*

![Screenshot](https://www.winglessraven.com/DMSM/images/servermanager.png)


[https://www.winglessraven.com/DMSM/](https://www.winglessraven.com/DMSM/)
