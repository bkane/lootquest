Where to put stuff
========================

Content
---------
steampipe/content_win/ (or steampipe/content_osx/) is where the game build goes, including redist installers. This becomes the root folder when installed on Steam, so it should look like this:

steampipe/content_win/
	- ktane_Data/
	- demoSettings.xml
	- ktane.exe 
	- logConfig.xml
	- steam_api.dll
	
or

steampipe/content_osx/Keep Talking and Nobody Explodes.app

Steam will place the contents of "content_win" into "steamapps/common/Keep Talking and Nobody Explodes/", so you do NOT need to create a "Keep Talking and Nobody Explodes" subfolder (and if you do, Steam won't be able to find and launch ktane.exe).

Logs
----
steampipe/output_win/ is where intermediate and log files will be generated. Can be deleted, but will slow next build.

Neither content nor output folder should be under source control.

steampipe/scripts/ contains the app and depot scripts for the game.


How to run a build
========================
To create a build, run "run_build_win.bat" or builder\steamcmd.exe +login account password +run_app_build ..\scripts\<app_script>.vdf +quit

<app_script>.vdf should point to the appropriate content and output folders as mentioned above.


Soundtrack
========================
Similarly, use content_soundtrack for the soundtrack build.
Note that the contents of "content_soundtrack" will automatically be placed into "steamapps/common/Keep Talking and Nobody Explodes/" when the user downloads it,
so you **DO** need to create a "soundtrack" subfolder. Also, place the FLAC files under "soundtrack_flac" so that the Steam music player doesn't automatically pick it up (and thus would be adding two copies of the soundtrack to the library).

Thus it should look like this:

steampipe/content_soundtrack/
	- soundtrack/
		- AlbumArtwork.png
		- track01.mp3
		- track02.mp3
		...
	- soundtrack_flac/
		- track01.flac
		- track02.flac
		...