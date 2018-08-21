using System;


[Serializable]
public class Sclass1
{
    private string message;
    private int player;

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

    public int GetPlayer()
    {
        return player;
    }

    public void SetPlayer(int id)
    {
        player = id;
    }
}