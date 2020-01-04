using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private float speed = 50f;
    private float fasterSpeed;
    private bool isFalling = true;
    private float offset = 1f;

    private float minX = -4f, maxX = 4f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        fasterSpeed = speed * 5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(0, -speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
            Move();
        else
            rb.velocity = Vector3.zero;

        FallFaster();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BottomBounds"))
            isFalling = false;
    }

    void Move()
    {
        float newX = rb.position.x;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            newX -= offset;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            newX += offset;

        float clampedX = Mathf.Clamp(newX, minX, maxX);
        rb.position = new Vector2(clampedX, rb.position.y);
    }

    void FallFaster()
    {
        if (Input.GetKey(KeyCode.DownArrow))
            rb.velocity = new Vector2(0, -fasterSpeed * Time.deltaTime);

        if (Input.GetKeyUp(KeyCode.DownArrow))
            rb.velocity = new Vector2(0, -speed * Time.deltaTime);
    }
}
