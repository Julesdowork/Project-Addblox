using UnityEngine;

public class Block : MonoBehaviour
{
    private enum State { FALLING, RESTING };

    [SerializeField]
    private float speed = 50f;
    private State currentState = State.FALLING;
    private float offset = 1f;

    private float minX = -4f, maxX = 4f;
    [SerializeField]
    private LayerMask blockLayer;
    private Vector3 rayOffset = new Vector3(0.5f, 0, 0);

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
        if (collision.gameObject.CompareTag("BottomBounds") ||
            (collision.gameObject.CompareTag("Block") && currentState == State.FALLING))
        {
            currentState = State.RESTING;
            BlockSpawner.instance.SpawnNewBlock();
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

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            newX -= offset;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            newX += offset;

        float clampedX = Mathf.Clamp(newX, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, 0);
    }

    private void Drop()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
}
