using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 5.0f;

    Rigidbody2D rigid = null;

    bool bJumpable = true;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x > 0) transform.localScale = new Vector2(1.0f, 1.0f);
        else if (x < 0) transform.localScale = new Vector2(-1.0f, 1.0f);

        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);

        if(Input.GetKeyDown(KeyCode.Space) && bJumpable)
        {
            bJumpable = false;
            rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.CompareTag("GROUND"))
        {
            bJumpable = true;
        }
    }
}
