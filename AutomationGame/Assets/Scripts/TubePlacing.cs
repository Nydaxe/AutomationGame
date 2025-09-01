using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TubePlacing : MonoBehaviour
{
    bool clicked = false;
    bool placingtube = false;

    public IEnumerator EnableTubePlacing()
    {
        placingtube = true;

        GameObject tube = TubeManager.Instance.GetComponent<ObjectPool>().GetObject();
        LineRenderer tubeLine = tube.GetComponent<LineRenderer>();

        tubeLine.positionCount = 1;

        while (placingtube)
        {
            int positionIndex = tubeLine.positionCount - 1;

            //Previews the tube placement
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseScreenPosition.z = 0f;
            tubeLine.SetPosition(positionIndex, mouseScreenPosition);

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                tubeLine.SetPosition(positionIndex, mouseScreenPosition);
                tubeLine.positionCount++;
            }

            yield return null;
        }
    }
}
