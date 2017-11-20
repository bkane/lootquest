Facepunch.Steamworks.dll  - v0.4

  This is the main dll. It depends on the native library, so when using it the native
  library needs to be somehow available.

  Native dlls are in the Native folder. You will need to copy these to your project root, 
  because Steam doesn't always find the dll.

  You also need to copy the steam_appid.txt to your project root. If you have an appid, put
  it in this file. If you don't, use one of a game you own. I've put Half-Life 2's appid 
  (220) in by default - since everyone owns it.

  You can test that it's all working fine by putting the FacepunchSteamworksTest component
  on something in an scene and running it. If it's successful it should print out your steam 
  name and your steamid.

https://github.com/Facepunch/Facepunch.Steamworks/