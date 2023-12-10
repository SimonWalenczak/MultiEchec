using System.Collections;
using System.Collections.Generic;
using PlayerIOClient;
using UnityEngine;

public interface IFunction
{
    void Execute(Message message);
}