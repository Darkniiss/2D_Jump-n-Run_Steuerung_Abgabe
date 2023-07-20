using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float jumpForce = 3;
    [SerializeField] private bool canDoubleJump;
    [SerializeField] private bool canWallJump;

    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;
    private Vector2 moveVec;
    private bool isGrounded;
    private bool isAtWall;
    private bool usedDoubleJump;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

        playerRb.velocity = new Vector2(moveVec.x * Time.deltaTime * moveSpeed, playerRb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveVec = context.ReadValue<Vector2>();

        if (moveVec.x > 0)
        {

            playerSprite.flipX = true;
        }
        else if (moveVec.x < 0)
        {
            playerSprite.flipX = false;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
        else if (!isGrounded && canDoubleJump && !usedDoubleJump && context.performed)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
            usedDoubleJump = true;
        }
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if(isAtWall && canWallJump && context.performed)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            usedDoubleJump = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isAtWall = true;
            usedDoubleJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isAtWall = false;
        }
    }

}
