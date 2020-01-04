using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public Sprite[] blockSprites = new Sprite[6];

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
