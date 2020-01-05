using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public Sprite[] blockSprites = new Sprite[6];

    [SerializeField]
    private GameObject gameOverPanel;

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

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }
}
