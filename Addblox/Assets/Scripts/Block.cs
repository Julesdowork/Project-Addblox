using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private bool isFalling = true;
    private float offset = 1.28f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector3(0, -speed * Time.deltaTime, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
            Move();
        else
            rb.velocity = Vector3.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BottomBounds"))
            isFalling = false;
    }

    void Move()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            rb.position += new Vector2(-offset, 0);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            rb.position += new Vector2(offset, 0);
    }
}
