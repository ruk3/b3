using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class Pointandclick : MonoBehaviour {
	Animator anim;
	UnityEngine.AI.NavMeshAgent agent;
	Vector2 velocity = Vector2.zero;

    float runSpeed = 2f;
    float countdown = 3f;
    int jumpHash = Animator.StringToHash("Jump");

    private bool _traversingLink;
    private OffMeshLinkData _currLink;

    void Start () {
		anim = GetComponent<Animator> ();
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.updatePosition = false;
    }

    void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            if (!_traversingLink)
            {
                _currLink = agent.currentOffMeshLinkData;

                anim.SetTrigger(jumpHash);
                _traversingLink = true;
            }
            else if (!anim.IsInTransition(0))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).tagHash != jumpHash)
                {
                    _traversingLink = false;
                }
            }
        }
        else
        {
            Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

            // Map 'worldDeltaPosition' to local space
            float dx = Vector3.Dot(transform.right, worldDeltaPosition);
            float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
            Vector2 deltaPosition = new Vector2(dx, dy);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                runSpeed = 2;
                agent.speed = 3.5f;
            }
            else
            {
                runSpeed = 1;
                agent.speed = 1.25f;
            }

            // Update velocity if delta time is safe
            if (Time.deltaTime > 1e-5f)
                velocity = deltaPosition / Time.deltaTime;

            if (Vector3.Distance(agent.destination, agent.transform.position) < 1.5f)
            {
                agent.isStopped = true;
                agent.tag = "Stopped";
                agent.velocity = Vector2.zero;
                anim.SetBool("move", false);
                anim.SetFloat("Horizontal", 0 * runSpeed, 0.2f, Time.deltaTime);
                anim.SetFloat("Vertical", 0 * runSpeed, 0.2f, Time.deltaTime);
            }
             else
             {
                bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

                // Update animation parameters
                anim.SetBool("move", shouldMove);
                anim.SetFloat("Horizontal", velocity.x * runSpeed, 0.2f, Time.deltaTime);
                anim.SetFloat("Vertical", velocity.y * runSpeed, 0.2f, Time.deltaTime);
            }

            TagCheck();

            

            LookAt lookAt = GetComponent<LookAt>();
            if (lookAt)
                lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

            //		// Pull agent towards character
            if (worldDeltaPosition.magnitude > agent.radius)
                agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
        }
    }

    private void TagCheck()
    {
        if (agent.tag == "Stopped")
        {
            countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                agent.velocity = Vector2.zero;
                agent.isStopped = true;
                //agent.destination = agent.transform.position;
                anim.SetBool("move", false);
                anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
                
        }
    }

    void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stopped")
        {
            agent.tag = "Stopped";
        }
    }
}
