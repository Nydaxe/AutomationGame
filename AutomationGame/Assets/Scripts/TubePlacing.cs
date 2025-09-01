using System.Collections;
using UnityEngine;

public class TubePlacing : MonoBehaviour
{
    bool clicked = false;
    bool placingtube = false;

    public IEnumerator EnableTubePlacing()
    {
        placingtube = true;

        GameObject tube = TubeManager.Instance.GetComponent<ObjectPool>().GetObject();

        while (placingtube)
        {
            //Set the position of the tube to the mouse position
            Vector3 mouseScreenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseScreenPosition.z = 0f;
            Debug.Log(mouseScreenPosition);
            Debug.Log(tube.transform.position);
            tube.transform.position = mouseScreenPosition;

            if (clicked)
            {
                
            }

            yield return null;
        }
    }

    void OnMouseDown()
    {
        clicked = !clicked;
    }
}
