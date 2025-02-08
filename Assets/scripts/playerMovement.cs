using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 500;
    public float jumpForce = 7f;
    public Rigidbody rb;
    public float v;
    public float h;
    private bool isGrounded = true;
    private bool isNextToBed = false;
    public Transform player;
    private Animator animator;
    public Transform camera;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(h, 0, v);
        
        if (movement != Vector3.zero)
        {
            animator.SetBool("moving", true);
            if (v < 0)
            {
                animator.SetBool("movingBackward", true);
            }
            else
            {
                animator.SetBool("movingBackward", false);
            }
            if (h < 0)
            {
                animator.SetBool("movingLeft", true);
                animator.SetBool("movingRight", false);
            }
            else if (h > 0)
            {
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingRight", true);
            }
            else
            {
                animator.SetBool("movingLeft", false);
                animator.SetBool("movingRight", false);
            }
        }
        else
        {
            animator.SetBool("moving", false);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingRight", false);
            animator.SetBool("movingBackward", false);
        }
        

        float cameraYRotation = camera.eulerAngles.y;
        player.localRotation = Quaternion.Euler(0f, cameraYRotation, 0f);

        movement = camera.TransformDirection(movement);
        movement.y = 0; // Prevent movement on the Y-axis
        movement.Normalize(); // Normalize to maintain consistent speed
        movement *= speed * Time.deltaTime;

        rb.linearVelocity = new Vector3(movement.x, rb.linearVelocity.y, movement.z);
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        isNextToBed = false;
        if(collision.gameObject.tag == "bed")
        {
            isNextToBed = true;
        }


        Debug.Log(isNextToBed);
    }
}
