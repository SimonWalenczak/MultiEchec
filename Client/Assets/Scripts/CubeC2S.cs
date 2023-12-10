using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;
using UnityEngine;

public class CubeC2S : IFunction
{
    public void Execute(Message message)
    {
        int newPos = message.GetInt(1);

        GameManager.Instance.MoveCube(newPos);
        
        Debug.Log("Salut cest CubeC2S");
    }
}
