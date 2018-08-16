using System;

enum MessageIdentifiers { OnePlayerConnected, TwoPlayersConnected, StartingGame,
    WaitingForOpponent, GameUpdate, RetryGameUpdate, GameOver, PauseRequest, PauseGame }

[Serializable]
public class Sclass1
{
    private string message;
    private string player;

    public Sclass1()
    {
        message = "I was contructed";
    }

    public string GetMessage()
    {
        return message;
    }

    public void SetMessage(string s)
    {
        message = s;
    }

    public string GetPlayer()
    {
        return player;
    }

    public void SetPlayer(string s)
    {
        player = s;
    }
}