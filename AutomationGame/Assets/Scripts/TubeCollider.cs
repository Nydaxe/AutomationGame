using UnityEngine;

public class TubeCollider : MonoBehaviour
{
    public Tube tube;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered tube collider");
        tube.TubeObject(other);
    }
}
