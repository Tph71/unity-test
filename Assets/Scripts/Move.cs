using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;

    private string test = "success  one";

    public float MoveSpeed = 5.0f;
    private Vector2 moveDir;

    public bool isGrounded = false;

    public GameObject groundedObject;
    public float JumpForce = 500.0f;
    private SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().localPosition = new Vector3(-4.26f, -3.4f, 0);
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("inGround", isGrounded);
        Right();
        Left();
        Up();

    }

    void Right()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir.x = MoveSpeed;
            m_Animator.SetFloat("MoveSpeed", 1);
            m_SpriteRenderer.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            moveDir = Vector2.zero;
            m_Animator.SetFloat("MoveSpeed", 0);
            m_SpriteRenderer.flipX = true;
        }

        moveDir.y = m_Rigidbody2D.velocity.y;
        m_Rigidbody2D.velocity = moveDir;
    }
    void Left()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir.x = -MoveSpeed;
            m_Animator.SetFloat("MoveSpeed", 1);
            m_SpriteRenderer.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir = Vector2.zero;
            m_Animator.SetFloat("MoveSpeed", 0);
            m_SpriteRenderer.flipX = false;
        }

        moveDir.y = m_Rigidbody2D.velocity.y;
        m_Rigidbody2D.velocity = moveDir;
    }

    void Up()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            m_Rigidbody2D.AddForce(Vector2.up * JumpForce);
            m_Animator.SetTrigger("Jump");
        }

        moveDir.y = m_Rigidbody2D.velocity.y;
        m_Rigidbody2D.velocity = moveDir;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D element in other.contacts)
            {
                if (element.normal.y < 0.25f) Debug.Log("Zero");
                if (element.normal.y > 0.25f)
                {
                    groundedObject = other.gameObject;
                    Debug.Log("One");
                    isGrounded = true;
                    break;
                }

            }
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject == groundedObject)
        {
            groundedObject = null;
            isGrounded = false;
        }
    }
}
