using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TubePlacing : MonoBehaviour
{
    public bool placingtube = false;
    int tubePlacements;

    public IEnumerator EnableTubePlacing()
    {        
        placingtube = true;

        GameObject tube = TubeManager.Instance.GetComponent<ObjectPool>().GetObject();
        LineRenderer tubeLine = tube.GetComponent<LineRenderer>();

        tubeLine.positionCount = 1;

        tubePlacements = 0;

        bool initialPlacement = false;


        while (placingtube)
        {
            //Gets the mouse position in world space
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPosition.z = 0f;

            if (Mouse.current.leftButton.wasPressedThisFrame && !initialPlacement)
            {
                tubeLine.SetPosition(0, mouseScreenPosition);
                initialPlacement = true;
                Debug.Log("Initial Placement");
                continue;
            }

            if (!initialPlacement)
            {
                continue;
            }

            Debug.Log("yo");

            yield return null;
        }
    }
}
