using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class Pointandclick : MonoBehaviour {
	Animator anim;
	UnityEngine.AI.NavMeshAgent agent;
	Vector2 velocity = Vector2.zero;

    float runSpeed = 2f;
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

            bool shouldMove = velocity.magnitude > 0.5f && agent.remainingDistance > agent.radius;

            // Update animation parameters
            anim.SetBool("move", shouldMove);
            anim.SetFloat("Horizontal", velocity.x * runSpeed, 0.2f, Time.deltaTime);
            anim.SetFloat("Vertical", velocity.y * runSpeed, 0.2f, Time.deltaTime);

            LookAt lookAt = GetComponent<LookAt>();
            if (lookAt)
                lookAt.lookAtTargetPosition = agent.steeringTarget + transform.forward;

            //		// Pull agent towards character
            if (worldDeltaPosition.magnitude > agent.radius)
                agent.nextPosition = transform.position + 0.9f * worldDeltaPosition;
        }
    }

	void OnAnimatorMove () {
		transform.position = agent.nextPosition;
	}
}
