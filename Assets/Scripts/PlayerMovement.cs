using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 8f;
    private bool isFacingRight = true;

    public bool canMove;
    public bool canWallJump;

    private void Start()
    {
        canMove = true;
        canWallJump = true;
    }
    void Update()
    {
        if (canMove)
        {
            if (!isFacingRight && horizontal > 0f)
            {
                Flip();
            }
            else if (isFacingRight && horizontal < 0f)
            {
                Flip();
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }
            else if (Input.GetKeyUp(KeyCode.Space) && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }

        }


        Debug.Log(IsGrounded());
    }



    private void FixedUpdate()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                horizontal = -1;


                if (IsOnWall(false) && !IsGrounded() && canWallJump && Input.GetKeyDown(KeyCode.Space))
                {
                    JumpWall(false);

                }

                if (canWallJump || !IsOnWall(false))
                {
                    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontal = 1;

                if (IsOnWall(true) && !IsGrounded() && canWallJump && Input.GetKeyDown(KeyCode.Space))
                {
                    JumpWall(true);

                }

                if (canWallJump || !IsOnWall(true))
                {
                    rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
                }
            }
            else
            {
                horizontal = 0;
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }

            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.D))
            {
                horizontal = 0;
            }
        }

    }

    /*
    public void Jump(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            if (context.performed && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            }

            if (context.canceled && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }*/

    public bool IsGrounded()
    {

        Collider[] col = Physics.OverlapSphere(groundCheck.position, 0.1f, groundLayer);

        if (col.Length > 0)
        {
            if (!canWallJump)
            {
                canWallJump = true;
            }
            return true;

        }
        else
        {
            return false;
        }
    }

    public bool IsOnWall(bool droite)
    {
        RaycastHit[] hits;
        if (droite)
        {
            hits = Physics.RaycastAll(gameObject.transform.position, Vector3.right, 1 + 0.1f);
        }
        else
        {
            hits = Physics.RaycastAll(gameObject.transform.position, Vector3.left, 1 + 0.1f);
        }

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == 6)
            {
                return true;
            }

        }

        return false;
    }

    void JumpWall(bool droite)
    {
        if (droite)
        {
            rb.velocity = ((new Vector2(-jumpingPower, jumpingPower)).normalized) * jumpingPower;
        }
        else
        {
            rb.velocity = ((new Vector2(jumpingPower, jumpingPower)).normalized) * jumpingPower;
        }
        canMove = false;
        canWallJump = false;
        StopAllCoroutines();
        StartCoroutine(CoroutineCanDedouble());
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    IEnumerator CoroutineCanDedouble()
    {
        yield return new WaitForSeconds(0.2f);
        canMove = true;
    }

    /*
    public void Move(InputAction.CallbackContext context)
    {
        if (canMove)
        {
            horizontal = context.ReadValue<Vector2>().x;
        }
        else
        {
            horizontal = 0;
        }
    }*/

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }


}