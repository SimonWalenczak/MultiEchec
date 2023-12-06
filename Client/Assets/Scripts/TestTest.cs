using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTest : MonoBehaviour
{
    [SerializeField] private GameObject image;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            image.SetActive(true);
        }
    }
}
