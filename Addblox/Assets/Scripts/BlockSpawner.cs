using System;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public static BlockSpawner instance;

    public Action SpawnNewBlock = delegate { };

    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Transform blockHolder;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        SpawnNewBlock = SpawnBlock;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBlock();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnBlock()
    {
        GameObject block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
        block.transform.parent = blockHolder;
    }
}
