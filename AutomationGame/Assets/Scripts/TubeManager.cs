using UnityEngine;

public class TubeManager : MonoBehaviour
{
    public static TubeManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////


    public ObjectPool tubePool;
    public TubePlacing tubePlacing;

    public void StartPlacingTube()
    {
        StartCoroutine(tubePlacing.EnableTubePlacing());
    }
}
