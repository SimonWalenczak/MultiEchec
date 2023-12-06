using UnityEngine;

public class Move : MonoBehaviour
{
    public MovableHandler movableHandler;
    public int index;
    public bool IsSelect;
    
    private void OnMouseDown()
    {
        Debug.Log("clique !");
        IsSelect = !IsSelect;

        if (IsSelect)
        {
            movableHandler.selectedPiece = this;
        }
    }
}
