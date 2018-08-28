Start Server
Start a client, and enter the server IP address
Start a second client, and enter the server IP address
if a console says your turn, type something and hit enter.
Then it should says not your turn, and update the other client that it is their turn

Know bug: if you just hit enter, it breaks, probably because it assumes when you send an empty message, that you disconnected.

If you look into the code, Player1 and Player2 are acutally variables of the Sclass1 serializable object in SharedClasses.
