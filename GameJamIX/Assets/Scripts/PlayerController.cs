using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject mainCamera;

    public Animator animator;

    public float cameraMaxUpAngle = -80f;
    public float cameraMaxDownAngle = 70f;

    public float speed = 100f;
    public float acceleration = 100f;
    public float jumpForce = 20000f;

    public float sensitivityX = 50f;
    public float sensitivityY = 50f;

    private Rigidbody body;

    private Vector3 gunPos;

    private bool jumped = false;
    private bool shot = false;

    private float pitch = 0f;

    private float sprint = 1f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();

        Animations();
    }

    void OnCollisionEnter(Collision col)
    {
        if(jumped)
        {
            jumped = false;
        }
    }

    private void Animations()
    {
    }

    private void Inputs()
    {
        //Look inputs
        if(Input.GetAxisRaw("Mouse Y") != 0)
        {
            transform.Rotate(0,Input.GetAxisRaw("Mouse X") * sensitivityX *  Time.deltaTime,0);
            pitch += (-Input.GetAxisRaw("Mouse Y") * sensitivityY * Time.deltaTime);
            pitch = Mathf.Clamp( pitch, -cameraMaxUpAngle, cameraMaxDownAngle);
            mainCamera.transform.localEulerAngles = new Vector3( pitch, 0, 0);
        }

        if(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            Vector3 temp = mainCamera.transform.forward;
            Vector3 temp1 = mainCamera.transform.right;
            temp.y = 0;
            temp1.y = 0;

            body.velocity += (((Input.GetAxisRaw("Vertical") * temp) + (Input.GetAxisRaw("Horizontal") * temp1)).normalized * acceleration * sprint * Time.deltaTime);
            body.velocity.Set(Mathf.Clamp(body.velocity.x, 0, 5 * Time.deltaTime), body.velocity.y, Mathf.Clamp(body.velocity.z, 0, 5 * Time.deltaTime));
        }

        if(Input.GetAxisRaw("Jump") != 0 && !jumped)
        {
            jumped = true;
            body.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Reload");
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            animator.SetBool("Sprinting", true);
            sprint = 2.5f;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("Sprinting", false);
            sprint = 1f;
        }

        if(Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Shoot");
            RaycastHit hit;
            
            if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, float.MaxValue))
            {
                Debug.Log(hit.collider.name);
                if(hit.collider.GetComponent<Enemy>())
                {
                    hit.collider.GetComponent<Enemy>().damage(100f, transform.position);
                }
            };

        }
    }
}
