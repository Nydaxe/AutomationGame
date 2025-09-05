using UnityEngine;
using System.Collections;

public class Tube : MonoBehaviour
{
    public LineRenderer tubeLineRenderer;
    public LineRenderer previewTubeLineRenderer;
    public float speed = 5f;
    public Collider2D startCollider;
    public Collider2D endCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        float distToStart = (other.gameObject.transform.position - tubeLineRenderer.GetPosition(0)).sqrMagnitude;
        float distToEnd = (other.gameObject.transform.position - tubeLineRenderer.GetPosition(tubeLineRenderer.positionCount - 1)).sqrMagnitude;
        int startIndex;
        int endIndex;
        int direction;

        if (distToStart < distToEnd)
        {
            startIndex = 0;
            endIndex = tubeLineRenderer.positionCount - 1;
            direction = 1;
        }
        else
        {
            startIndex = tubeLineRenderer.positionCount - 1;
            endIndex = 0;
            direction = -1;
        }

        Tubeable tubeable = other.GetComponent<Tubeable>();
        if (tubeable == null || tubeable.inTube)
            return;

        tubeable.inTube = true;
        StartCoroutine(MoveObjectAlongTube(tubeable, startIndex, endIndex, direction));
    }

    private IEnumerator MoveObjectAlongTube(Tubeable tubeable, int startIndex, int endIndex, int direction)
    {
        for (int i = 1; i < tubeLineRenderer.positionCount; i++)
        {
            Vector2 startPosition = tubeLineRenderer.GetPosition(startIndex + (i - 1) * direction);
            Vector2 endPosition = tubeLineRenderer.GetPosition(startIndex + i * direction);
            float distance = Vector2.Distance(startPosition, endPosition);
            float duration = distance / speed;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / duration);
                tubeable.gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, t);
                yield return null;
            }
            tubeable.gameObject.transform.position = endPosition;
        }
        tubeable.inTube = false;
    }

    public void UpdateColliders()
    {
        if (tubeLineRenderer.positionCount < 2)
            return;

        Vector2 start = tubeLineRenderer.GetPosition(0);
        Vector2 end = tubeLineRenderer.GetPosition(tubeLineRenderer.positionCount - 1);

        startCollider.transform.position = start;
        endCollider.transform.position = end;

        startCollider.enabled = true;
        endCollider.enabled = true;
    }
}
