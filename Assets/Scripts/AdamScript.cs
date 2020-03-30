using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AdamScript : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    //public GameObject target;

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    int jumpHash = Animator.StringToHash("Jump");
    int leftHash = Animator.StringToHash("IdleTurnLeft");
    int rightHash = Animator.StringToHash("IdleTurnRight");

    float runSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        float smooth = Mathf.Min(1.0f, Time.deltaTime / 01.5f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        if (Time.deltaTime > 1e-5f)
        {
            velocity = smoothDeltaPosition / Time.deltaTime;
        }



        //KeyControl();
        /*
        var destDir = target.transform.position - transform.position;
        var currDir = transform.forward;
        var angDev = Vector3.SignedAngle(destDir, currDir, Vector3.up) / 180;

        var hor = -1 * angDev;
        var ver = Mathf.Abs(hor) > 1.0f ? 0 : Mathf.Min(destDir.magnitude, 0.5f);

        anim.SetFloat("Horizontal", hor);
        anim.SetFloat("Vertical", ver);

        Debug.DrawLine(transform.position, transform.position + Vector3.right * hor, Color.red);
        Debug.DrawLine(transform.position, transform.position + Vector3.forward * ver, Color.cyan);
        */
    }

    private void KeyControl()
    {
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
