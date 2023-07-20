using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10;

    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;
    private Vector2 moveVec;

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

}
