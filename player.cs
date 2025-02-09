using System.Net.Mime;
using UnityEngine;

public class PlayerMoncef : MonoBehaviour
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
    public bool isGathering = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        string currentAnimation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (currentAnimation != "Gathering Objects")
        {
            Move();
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                animator.SetTrigger("GatherTrigger");
            }
        }



    }

    void Move(){

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(h, 0, v);
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();



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
