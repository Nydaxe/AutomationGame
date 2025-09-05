using UnityEngine;
using System.Collections;

public class Dragable : MonoBehaviour
{
    void OnMouseDrag()
    {
        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (StateManager.state != StateManager.State.Idle)
        {
            if ((Vector2)transform.position == mouseWorldPosition)
                return;

            transform.position = Vector2.Lerp(transform.position, mouseWorldPosition, 0.2f);
        }
    }

}