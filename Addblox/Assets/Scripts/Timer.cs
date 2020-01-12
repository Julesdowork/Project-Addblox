using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private int totalTime;
    private int seconds;
    private int minutes;
    public int Minutes { get; }
    private string time;
    private TextMeshProUGUI timeText;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CountUpTime", 0.1f, 1f);
    }

    void CountUpTime()
    {
        totalTime = (int)Time.timeSinceLevelLoad;
        minutes = totalTime / 60;
        seconds = totalTime % 60;
        time = minutes.ToString("D2") + ":" + seconds.ToString("D2");
        timeText.text = time;
    }
}
