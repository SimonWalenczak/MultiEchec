using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PlayerIOClient;
using UnityEngine;

public class MovableHandler : MonoBehaviour
{
    public List<Move> Pieces;
    public Move selectedPiece;
    
    void Update()
    {
        if (selectedPiece != null && Input.GetMouseButton(0))
        {
            GameManager.Instance.pioconnection.Send("Testest", selectedPiece.index, Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    public void HandleMessage(Message message)
    {
        switch (message.Type)
        {
            case "Testest":
                int pieceIndex = message.GetInt(0);
                float mouseX = message.GetFloat(1);
                float mouseY = message.GetFloat(2);

                Move piece = Pieces.FirstOrDefault(piece => piece.index == pieceIndex);

                if (piece == null)
                {
                    return;
                }

                piece.gameObject.transform.position = new Vector3(mouseX, mouseY, transform.position.z);
                
                break;
        }
    }
}
