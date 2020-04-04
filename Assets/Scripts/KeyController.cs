using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    Animator anim;
    int jumpHash = Animator.StringToHash("Jump");
    int leftHash = Animator.StringToHash("IdleTurnLeft");
    int rightHash = Animator.StringToHash("IdleTurnRight");

    float runSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyControl();
    }

    private void KeyControl()
    {
        anim.SetBool("move", true);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runSpeed = 2;
        }
        else
        {
            runSpeed = 1;
        }

        float moveZ = Input.GetAxis("Vertical");
        //anim.SetFloat("Vertical", moveZ*runSpeed);
        anim.SetFloat("Vertical", moveZ * runSpeed, 0.2f, Time.deltaTime);

        float moveX = Input.GetAxis("Horizontal");
        anim.SetFloat("Horizontal", moveX * runSpeed, 0.2f, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(jumpHash);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger(leftHash);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger(rightHash);
        }
    }
}
