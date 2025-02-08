using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    private bool isGrounded = true;
    private Animator animator;
    public float v;
    public float h;
    public Rigidbody rb;
    public float jumpForce = 7f;
    public Transform cameraTransform;

    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }


    }

    void Move(){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(h, 0, v);
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        // if (movementDirection != Vector3.zero)
        // {
        //     animator.SetBool("moving", true);
        //     if (v < 0)
        //     {
        //         animator.SetBool("movingBackward", true);
        //     }
        //     else
        //     {
        //         animator.SetBool("movingBackward", false);
        //     }
        //     if (h < 0)
        //     {
        //         animator.SetBool("movingLeft", true);
        //         animator.SetBool("movingRight", false);
        //     }
        //     else if (h > 0)
        //     {
        //         animator.SetBool("movingLeft", false);
        //         animator.SetBool("movingRight", true);
        //     }
        //     else
        //     {
        //         animator.SetBool("movingLeft", false);
        //         animator.SetBool("movingRight", false);
        //     }
        // }
        // else
        // {
        //     animator.SetBool("moving", false);
        //     animator.SetBool("movingLeft", false);
        //     animator.SetBool("movingRight", false);
        //     animator.SetBool("movingBackward", false);
        // }



        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
        
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("moving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);            
        }else{
            animator.SetBool("moving", false);
        }

    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
}