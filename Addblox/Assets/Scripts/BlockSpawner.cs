using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner instance;

    public GameObject fallingBlock;
    public GameObject heldBlock;

    private GameObject[] blockQueue = new GameObject[3];
    private bool canHoldNewBlock = true;

    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Transform blockHolder;
    [SerializeField]
    private Transform heldBlockArea;
    [SerializeField]
    private Transform[] nextBlockPreviews = new Transform[3];

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        HoldBlock();
    }

    private void InitializeGame()
    {
        for (int i = 0; i < blockQueue.Length; i++)
        {
            blockQueue[i] = Instantiate(blockPrefab, nextBlockPreviews[i]);
            blockQueue[i].transform.position = nextBlockPreviews[i].position;
        }

        SpawnBlock(Instantiate(blockPrefab));
    }

    public void SpawnNewBlock()
    {
        SpawnBlock(blockQueue[0]);
        ShiftQueue();
        AddBlockToQueue();

        canHoldNewBlock = true;
    }

    private void SpawnBlock(GameObject block)
    {
        block.transform.position = transform.position;
        block.transform.parent = blockHolder;

        block.GetComponent<BlockMovement>().currentState = BlockMovement.State.FALLING;
        fallingBlock = block;
    }

    private void ShiftQueue()
    {
        for (int i = 1; i < blockQueue.Length; i++)
        {
            blockQueue[i - 1] = blockQueue[i];
            blockQueue[i - 1].transform.parent = nextBlockPreviews[i - 1];
            blockQueue[i - 1].transform.position = nextBlockPreviews[i - 1].position;
        }
    }

    private void AddBlockToQueue()
    {
        int i = blockQueue.Length - 1;
        blockQueue[i] = Instantiate(blockPrefab, nextBlockPreviews[i]);
        blockQueue[i].transform.position = nextBlockPreviews[i].position;
    }

    private void HoldBlock()
    {
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            && canHoldNewBlock)
        {
            if (!heldBlock)
            {
                heldBlock = fallingBlock;
                heldBlock.GetComponent<BlockMovement>().currentState = BlockMovement.State.ON_HOLD;
                heldBlock.transform.position = heldBlockArea.position;
                SpawnNewBlock();
            }
            else
            {
                GameObject temp = heldBlock;
                heldBlock = fallingBlock;
                fallingBlock = temp;

                heldBlock.GetComponent<BlockMovement>().currentState = BlockMovement.State.ON_HOLD;
                fallingBlock.GetComponent<BlockMovement>().currentState = BlockMovement.State.FALLING;
                Vector3 oldPos = heldBlock.transform.position;
                heldBlock.transform.position = heldBlockArea.position;
                fallingBlock.transform.position = oldPos;
            }

            canHoldNewBlock = false;
        }
    }
}
