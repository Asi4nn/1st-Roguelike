using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    //private Rigidbody2D playerRb;

    [SerializeField] float speed;

    public Vector2 direction;

    private enum Facing
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
    private Facing facingDirection;

    // Start is called before the first frame update
    void Start()
    {
        facingDirection = Facing.UP;
        animator = GetComponent<Animator>();
        //playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TakeInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        /*playerRb.MovePosition(playerRb.position + direction * speed * Time.fixedDeltaTime);*/
    }

    private void TakeInput()
    {
        direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
            facingDirection = Facing.UP;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
            facingDirection = Facing.LEFT;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
            facingDirection = Facing.DOWN;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
            facingDirection = Facing.RIGHT;
        }
        direction = direction.normalized;

        /*direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");*/
        SetAnimatorMovement();
    }

    private void SetAnimatorMovement()
    {
        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
        // print(animator.GetFloat("xDir"));
    }
}
