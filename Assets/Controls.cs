using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public KeyCode moveForward;
    public KeyCode moveLeft;
    public KeyCode moveRight;
    public KeyCode moveBack;
    public KeyCode fire;

    public CharacterController characterController;

    public Transform bullet;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKey(moveForward)) {
        //     transform.position += transform.worldToLocalMatrix.MultiplyVector(Vector3.forward) * Time.deltaTime;
        // }
        // if(Input.GetKey(moveLeft)) {
        //     transform.position += transform.worldToLocalMatrix.MultiplyVector(Vector3.left) * Time.deltaTime;

        //     Quaternion wantedRotation = Quaternion.LookRotation(Vector3.left);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, wantedRotation, Time.time * 0.01f);
        // }
        // if(Input.GetKey(moveRight)) {
        //     transform.position += Vector3.right * Time.deltaTime;
        // }
        // if(Input.GetKey(moveBack)) {
        //     transform.position += Vector3.back * Time.deltaTime;
        // }

        float verticalSpeed = 0;
        if(!characterController.isGrounded) {
            verticalSpeed = -9.87f * Time.deltaTime;
        }
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);

        Vector3 move = Vector3.zero;
        if(Input.GetKey(moveForward)) {
            move += transform.forward;
        }
        if(Input.GetKey(moveBack)) {
            move -= transform.forward;
        }
        characterController.Move(3.0f * Time.deltaTime * move + Time.deltaTime * gravityMove);

        if(Input.GetKey(moveLeft)) {
            transform.Rotate(0.0f, -1.0f, 0.0f, Space.Self);
        }
        if(Input.GetKey(moveRight)) {
            transform.Rotate(0.0f, 1.0f, 0.0f, Space.Self);
        }

        if(
            Input.GetKey(moveForward) ||
            Input.GetKey(moveBack)
        ) {
            anim.SetBool("isRunning", true);
        } else {
            anim.SetBool("isRunning", false);
        }

        if(Input.GetKeyDown(fire)) {
            Vector3 angles = transform.rotation.eulerAngles;
            Transform newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(90, angles.y, 0));
            newBullet.GetComponent<Rigidbody>().velocity = 5 * transform.forward;
        }
    }
}
