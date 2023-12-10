using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameManager.Instance.pioconnection.Send("CUBE", 1);
            Debug.Log("clique !");
        }
    }
}
