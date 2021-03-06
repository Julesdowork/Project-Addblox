﻿using System.Collections;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public enum State { FALLING, RESTING, ON_HOLD, IN_QUEUE };
    public State currentState = State.IN_QUEUE;

    private float speed;
    private float offset = 1f;
    private float minX = -2f, maxX = 2f, maxY = 4.5f;

    [SerializeField]
    private LayerMask blockLayer;
    private Vector3 rayOffset = new Vector3(0.5f, 0, 0);

    [SerializeField]
    private GameObject differentFX;
    [SerializeField]
    private GameObject sameFX;
    private Vector3 effectOffset = new Vector3(0.5f, 0.5f, 0);

    private BlockData data;

    void Awake()
    {
        data = GetComponent<BlockData>();
        speed = GameplayController.instance.globalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == State.FALLING)
        {
            Fall();
            Move();
            Drop();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentState == State.FALLING)
        {
            currentState = State.RESTING;

            if (collision.gameObject.CompareTag("BottomBounds"))
            {
                GameplayController.instance.ResetMultiplier();
                SoundManager.instance.PlayImpactSound();
            }
            else if (collision.gameObject.CompareTag("Block"))
                HandleBlockCollision(collision.gameObject);

            if (transform.position.y < maxY)
                BlockSpawner.instance.SpawnNewBlock();
            else
                GameplayController.instance.GameOver();
        }
    }

    private void Fall()
    {
        //Debug.DrawRay(transform.localPosition + rayOffset, Vector2.down * 8f, Color.red);
        transform.Translate(-transform.up * speed * Time.deltaTime);
    }

    private void Move()
    {
        float newX = transform.position.x;
        RaycastHit2D hit;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f, blockLayer);
            if (hit) { return; }
            newX -= offset;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f, blockLayer);
            if (hit) { return; }
            newX += offset;
        }

        float clampedX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    private void Drop()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            float yPos = FindNextAvailableYPosition();
            transform.position = new Vector3(transform.position.x, yPos, 0);
        }
    }

    private float FindNextAvailableYPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + rayOffset, Vector2.down, 8f, blockLayer);

        if (hit.collider != null)
        {
            return hit.collider.gameObject.transform.position.y + 0.5f;
        }

        return 0;
    }

    private void HandleBlockCollision(GameObject otherBlock)
    {
        BlockData otherBlockData = otherBlock.GetComponent<BlockData>();
        int sum = data.BlockNumber + otherBlockData.BlockNumber;
        bool colorsMatch = BlockColorsMatch(data, otherBlockData);


        if (sum == 10 && colorsMatch)
        {
            ActivateEffect(sameFX, gameObject, otherBlock);
            Destroy(otherBlock);
            Destroy(gameObject);
            GameplayController.instance.AddToScore(70);
            GameplayController.instance.AddToMultiplier();
            SoundManager.instance.PlayMergeSound(true);
        }
        else if (sum == 10)
        {
            ActivateEffect(differentFX, gameObject, otherBlock);
            Destroy(otherBlock);
            Destroy(gameObject);
            GameplayController.instance.AddToScore(10);
            GameplayController.instance.AddToMultiplier();
            SoundManager.instance.PlayMergeSound(false);
        }
        else if (colorsMatch)
        {
            int tempVal = sum % 10;
            otherBlockData.BlockNumber = tempVal;
            otherBlockData.UpdateNumber();
            Destroy(gameObject);
            SoundManager.instance.PlayAddBlocksSound();
        }
        else
        {
            GameplayController.instance.ResetMultiplier();
            SoundManager.instance.PlayImpactSound();
        }
    }

    private bool BlockColorsMatch(BlockData block1, BlockData block2)
    {
        return block1.BlockColor == block2.BlockColor;
    }

    private void ActivateEffect(GameObject effect, GameObject thisBlock,
        GameObject otherBlock)
    {
        Instantiate(effect, thisBlock.transform.position + effectOffset,
            effect.transform.rotation);
        Instantiate(effect, otherBlock.transform.position + effectOffset,
            effect.transform.rotation);
    }
}
