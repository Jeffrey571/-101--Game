using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkspeed = 20f;
    public float gravity = -10f;
    public float jumpforce = 1000f;
    [Range(1,100)]
    public float camerasensitivity = 100f;
    public bool grounded = false;
    public bool isflying = false;
    public Vector3 velocity = Vector3.zero;
    public Vector3 playerinput;
    public CharacterController controller;
    public float flyspeed = 10f;

    public Camera playercamera;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        grounded = controller.isGrounded;

        //Gathering player input
        playerinput = new Vector3(Input.GetAxis("Horizontal"),0f, Input.GetAxis("Vertical"));
        
        controller.Move(transform.TransformDirection(playerinput) * Time.deltaTime * walkspeed);

        //Toggle Flying
        if (Input.GetKeyDown(KeyCode.F))
        {
            isflying = !isflying;
        }

        if (isflying)
        {
            //Flying Movement
            if (Input.GetKey(KeyCode.Z))
            {
                velocity.y = flyspeed;
            }
            else if (Input.GetKey(KeyCode.C))
            {
                velocity.y = -flyspeed;
            }

            else
            {
                velocity.y = 0;
            }
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {


            //Jumping and Gravity
            if (grounded && velocity.y < 0)
            {
                velocity.y = 0;
            }

            if (Input.GetButtonDown("Jump") && grounded)
            {
                velocity.y += jumpforce;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        // Mouse Rotation (Up and/or Down) with camera
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * Time.deltaTime * camerasensitivity);
        float xroatation = Input.GetAxis("Mouse Y") * Time.deltaTime * camerasensitivity * -1f;
        playercamera.transform.Rotate(Vector3.right * xroatation);
    }

}
