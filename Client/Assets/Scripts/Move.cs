using PlayerIOClient;
using UnityEngine;

public class Move : IFunction
{
    public void Execute(Message message)
    {
        int oldPosX = message.GetInt(1);
        int oldPosY = message.GetInt(2);
        int newPosX = message.GetInt(3);
        int newPosY = message.GetInt(4);
        //GridManager.Instance.MovePawn(new Vector2Int(newPosX, newPosY), new Vector2Int(oldPosX, oldPosY));
    }
}
