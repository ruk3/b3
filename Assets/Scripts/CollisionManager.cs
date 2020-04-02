using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollisionManager : MonoBehaviour
{

        NavMeshAgent agent;
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Selected Immoble")
            {

                //If the agent is selected, and it reaches the target destination, switch its tag to Selected Immoble
                //If a collision occurs with a gameobject with the tag Selected Immoble, switch agent tag to Selected Immoble
                Debug.Log("Collision of Selected Immoble objects");
            agent.tag = "Selected Immoble";
            }
        }
   
}
