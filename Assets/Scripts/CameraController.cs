using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 15f;
    public float moveBorder = 10f;
    public Vector2 moveLimit;
    public float scrollSpeed = 100f;
    public float rotationX =0.0f;
    public float rotationY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Screen.lockCursor = true;//locks screen curser into place when started
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;//initialize a poss value to calculate movements on
        rotationX += Input.GetAxis("Mouse X") * 300 * Time.deltaTime;//Gets mouse movement along x
        rotationY += Input.GetAxis("Mouse Y") * 300 * Time.deltaTime;//Gets mouse movement along y
        rotationY = Mathf.Clamp(rotationY, -90, 90);//Sets maximum rotations
        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);//transforms according to x rotations
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);//transforms according to y rotations
        pos = transform.position;

        //The following moves right, left, backward, forward, down and up respectively
        if (Input.GetKey("d"))
        {
            pos += transform.right*moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey("a"))
        {
            pos += (-1) * transform.right* moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey("s"))
        {
            pos += transform.forward* (-1)*moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey("w"))
        {
            pos += transform.forward * moveSpeed * Time.deltaTime;

        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            pos.y += (-1) * moveSpeed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.Space))
        {
            pos.y += moveSpeed * Time.deltaTime;

        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");//up down depending on scrolling
        pos.y += scroll*scrollSpeed * Time.deltaTime;

      
        pos.x = Mathf.Clamp(pos.x, -moveLimit.x, moveLimit.x);//creates border on the x plane
        pos.z = Mathf.Clamp(pos.z, -moveLimit.y, moveLimit.y);//creates border on the z plane
        transform.position = pos;//transforms position according to pos value

    }
}
        
