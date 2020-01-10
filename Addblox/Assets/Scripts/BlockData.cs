using UnityEngine;
using TMPro;

public enum BlockColor
{
    RED, ORANGE, YELLOW,
    GREEN, BLUE, WHITE
}

public class BlockData : MonoBehaviour
{
    public BlockColor BlockColor { get; set; }
    public int BlockNumber { get; set; }

    private SpriteRenderer spriteRenderer;
    private TextMeshProUGUI numberText;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        numberText = GetComponentInChildren<TextMeshProUGUI>();

        GenerateRandomValues();
    }

    private void GenerateRandomValues()
    {
        int randColor = Random.Range(0, 6);
        BlockColor = (BlockColor)randColor;
        spriteRenderer.sprite = GameplayController.instance.blockSprites[(int)BlockColor];

        BlockNumber = Random.Range(1, 10);
        UpdateNumber();
    }

    public void UpdateNumber()
    {
        numberText.text = BlockNumber.ToString();
    }
}
