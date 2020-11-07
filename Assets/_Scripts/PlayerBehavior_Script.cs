using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior_Script : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float HorizontalForce;
    public float VerticalForce;
    public bool isGrounded;
    public Transform SpawnPoint;

    private Rigidbody2D _rigidBody2d;
    private SpriteRenderer _SpriteRenderer;
    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    void Move()
    {
        if (isGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {//move right
                _rigidBody2d.AddForce(Vector2.right * HorizontalForce * Time.deltaTime);
                _SpriteRenderer.flipX = false;
                _Animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {//move left
                _rigidBody2d.AddForce(Vector2.left * HorizontalForce * Time.deltaTime);
                _SpriteRenderer.flipX = true;
                _Animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Vertical > joystickVerticalSensitivity)
            {//jumps
                _rigidBody2d.AddForce(Vector2.up * VerticalForce * Time.deltaTime);
                _Animator.SetInteger("AnimState", 2);
            }
            else
            {//idle
                _Animator.SetInteger("AnimState", 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//respawn
        if(collision.gameObject.CompareTag("DeathPlane"))
        transform.position = SpawnPoint.position;

    }
}
