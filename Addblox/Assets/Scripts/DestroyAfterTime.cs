using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField]
    private float time = 2f;

    void Awake()
    {
        Destroy(gameObject, time);
    }
}
