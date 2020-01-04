using UnityEngine;
using TMPro;

public enum BlockColor
{
    RED, ORANGE, YELLOW,
    GREEN, BLUE, WHITE
}

public class BlockData : MonoBehaviour
{
    private BlockColor blockColor;
    public BlockColor BlockColor { get; }

    private int blockNumber;
    public int BlockNumber { get; set; }

    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI numberText;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();

        GenerateRandomValues();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateRandomValues()
    {
        int randColor = Random.Range(0, 6);
        blockColor = (BlockColor)randColor;
        spriteRenderer.sprite = GameplayController.instance.blockSprites[(int)blockColor];

        blockNumber = Random.Range(1, 10);
        numberText.text = blockNumber.ToString();
    }
}
