# TshockLogs_Explosives
This is a plugin for Tshock, helps to track explosives(Bomb, Dynamite, etc... ) usage.

### How to use:

1. Download [latest released files](https://github.com/slzn/TshockLogs_Explosives/releases) and unzip it.
2. Put all .dll files to Tshock/ServerPlugins folder.
3. (Re)start tshock server.
4. You will see "[Server API] Info Plugin TshockLogs_Explosives vX.X.X.X (by slzn0124@gmail.com) initiated" message on console if plugin loaded.

### Config:

A config "TshockLogs_Explosives_Config.json" will be created at tshock folder(same place with other tshock config files).<br>
You can edit it and "reload" server to apply new setings while server is running.

TshockLogs_Explosives_Config.json
```
{
  "ShowOnConsole": false,   //Sets to true to show logs on console.
  "Explosives": [           //Explosives to be track. simply remove it from array to ignore.
    "Bomb",
    "BouncyBomb",
    "StickyBomb",
    "Dynamite",
    "BouncyDynamite",
    "StickyDynamite",
    "BombFish",
    "Explosives"
  ]
}
```

### Results:
Explosives usage will be logged at Tshock log file.<br>
Examples:<br>
2017-08-02 03:03:27 - TextLog: INFO: UserB throws Dynamite at (66483.73,2534)<br>
2017-08-02 05:51:14 - TextLog: INFO: UserA throws Bomb at (67192.53,6445.662)<br>
