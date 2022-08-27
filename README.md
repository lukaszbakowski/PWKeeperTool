# PWKeeperTool
## is a simple tool to keep passwords safe in storage on your own hardware/USB
### Why PWKeeperTool:
Decided to do this project as internet is growing every single day and we need more 
and more accounts to handle all over and remember many long and difficult passwords, 
and Im having feelings that I cant trust any other software... so here it is: PWKeeperTool

### Description:
PWKeeperTool is giving you full controll on your files which storing passwords locally 
and are hard coded. You can storage all passwords with just one. It is highly recomened
to use with encrypted USB drive.

### Usage:
 - when we forget any password
 - for our familly if something happens to us: testament, etc.

### Manual install on USB:

1. Run "Notepad"
2. Type in:
```
[Autorun]
Open=PWKeeper.Client.exe
Action=Start PWKeeper.Client
Label=PWKeeperTool
Icon=PWKeeper.Client.exe
```
3. Save the file as autorun.inf in the root of the USB flash drive
4. Copy source of PWKeeperTool to USB

### How to create a new account
while first signIn if there is no existing login name it will get automatically created. 
Account is working locally which you can delete
in ./data folder.