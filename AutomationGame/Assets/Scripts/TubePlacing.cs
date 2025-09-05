using System;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TubePlacing : MonoBehaviour
{
    public bool placingtube = false;

    public IEnumerator EnableTubePlacing()
    {
        Tube tube = TubeManager.Instance.tubePool.GetObject().GetComponent<Tube>();
        LineRenderer tubeLine = tube.tubeLineRenderer;
        LineRenderer previewTubeLine = tube.previewTubeLineRenderer;

        placingtube = true;

        tubeLine.positionCount = 1;

        bool initialPlacement = false;

        Vector2 lastPipePosition = Vector2.zero;

        Vector2 corner = Vector2.zero;

        bool prioritizeX = true;

        while (placingtube)
        {
            yield return null;

            Vector2 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (Mouse.current.leftButton.wasPressedThisFrame && !initialPlacement)
            {
                tubeLine.gameObject.transform.position = mouseScreenPosition;

                tubeLine.SetPosition(0, mouseScreenPosition);
                lastPipePosition = mouseScreenPosition;

                previewTubeLine.positionCount = 3;
                previewTubeLine.SetPosition(0, mouseScreenPosition);
                previewTubeLine.SetPosition(1, mouseScreenPosition);
                previewTubeLine.SetPosition(2, mouseScreenPosition);

                initialPlacement = true;
                Debug.Log("Initial Placement");
                continue;
            }

            if (!initialPlacement)
                continue;

            // Use the last placed point
            lastPipePosition = tubeLine.GetPosition(tubeLine.positionCount - 1);

            float xDistance = Mathf.Abs(mouseScreenPosition.x - lastPipePosition.x);
            float yDistance = Mathf.Abs(mouseScreenPosition.y - lastPipePosition.y);

            prioritizeX = xDistance >= yDistance;

            if (tubeLine.positionCount > 1)
            {
                // Change direction if overlapping in the same axis
                int lastPipeDirectionX = MathF.Sign(lastPipePosition.x - tubeLine.GetPosition(tubeLine.positionCount - 2).x);
                int lastPipeDirectionY = MathF.Sign(lastPipePosition.y - tubeLine.GetPosition(tubeLine.positionCount - 2).y);

                if (lastPipeDirectionX != 0)
                    prioritizeX = lastPipeDirectionX == MathF.Sign(mouseScreenPosition.x - lastPipePosition.x);
                if (lastPipeDirectionY != 0)
                    prioritizeX = !(lastPipeDirectionY == MathF.Sign(mouseScreenPosition.y - lastPipePosition.y));
            }

            // Calculate corner position
            corner = prioritizeX
            ? new Vector2(mouseScreenPosition.x, lastPipePosition.y)
            : new Vector2(lastPipePosition.x, mouseScreenPosition.y);

            // For preview, always use the last placed point
            previewTubeLine.SetPosition(0, lastPipePosition);
            previewTubeLine.SetPosition(1, corner);
            previewTubeLine.SetPosition(2, mouseScreenPosition);

            // On click, add the corner and mouse as new points
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                tubeLine.positionCount += 2;
                tubeLine.SetPosition(tubeLine.positionCount - 2, corner);
                tubeLine.SetPosition(tubeLine.positionCount - 1, mouseScreenPosition);
            }
        }

        tubeLine.gameObject.GetComponent<Tube>().UpdateColliders();

        previewTubeLine.positionCount = 0;
    }

    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && placingtube)
        {
            placingtube = false;
        }
    }
}