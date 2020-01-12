using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float globalSpeed = 1f;
    public int level = 1;

    public Sprite[] blockSprites = new Sprite[6];

    private int score;
    private int multiplier = 1;

    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI multText;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI highScoreText;

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
        scoreText.text = score.ToString();
        highScoreText.text = GameManager.instance.highScore.ToString();

        InvokeRepeating("AdvanceLevel", 60f, 60f);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);

        if (score > GameManager.instance.highScore)
            GameManager.instance.highScore = score;
        if (level > GameManager.instance.highLevel)
            GameManager.instance.highLevel = level;

        GameManager.instance.SaveGameData();
    }

    public void AddToScore(int points)
    {
        score += points * multiplier;
        scoreText.text = score.ToString();
    }

    public void AddToMultiplier()
    {
        multiplier++;
        if (multiplier >= 9)
            multiplier = 9;
        multText.text = "X" + multiplier;
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
        multText.text = "X" + multiplier;
    }

    private void AdvanceLevel()
    {
        level++;
        levelText.text = level.ToString();

        globalSpeed += 0.3f;

        if (level >= 10)
            CancelInvoke("AdvanceLevel");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay 1");
    }
}
