using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScrip : MonoBehaviour
{
    private float vertclimb;
    private float climbspeed = 8f;
    private bool isladder;
    private bool isclimbing;

    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        vertclimb = Input.GetAxis("Vertical");

        if(isladder && Mathf.Abs(vertclimb) > 0f)
        {
            isclimbing = true;

        }
    }

    private void FixedUpdate()
    {
        if (isclimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertclimb * climbspeed);
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    //on trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isladder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isladder = false;
            isclimbing = false;
        }
    }
}
