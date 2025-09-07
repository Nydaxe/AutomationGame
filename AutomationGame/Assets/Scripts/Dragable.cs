using UnityEngine;
using UnityEngine.InputSystem;

public class Dragable : MonoBehaviour
{
    public bool isDragging = false;
    [SerializeField] float dragSpeed = 0.1f;

    void OnMouseDrag()
    {
        Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (StateManager.Instance.state != StateManager.State.Idle)
            return;

        if (Vector2.Distance(transform.position, mouseScreenPosition) < .001f)
            return;

        transform.position = Vector2.Lerp(transform.position, mouseScreenPosition, dragSpeed);

        if (Vector2.Distance(transform.position, mouseScreenPosition) < 0.1f)
        {
            transform.position = mouseScreenPosition;
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
    }
    
    void OnMouseUp()
    {
        isDragging = false;
    }
}