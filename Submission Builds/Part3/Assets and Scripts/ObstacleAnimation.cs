using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This controls the Animation of the non-controllable dynamic.
public class ObstacleAnimation : MonoBehaviour
{
    public float speed = .2f;
    public float strength = 9f;

    public bool moveZ = false;
    public bool moveX = false;

    private float randomOffset;

    // Start is called before the first frame update
    void Start()
    {
        if (transform.localScale.x == 0.1f)
        {
            moveZ = true;
        } else
        {
            moveX = true;
        }
        // Random offset was meant to be used to randomly initliaze obstacle positions. Unused as obstacles will be manually placed.
        //randomOffset = Random.Range(0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (moveX)
        {
            pos.x = Mathf.Sin(Time.time * speed) * strength;
            transform.position = pos;
        } else if (moveZ)
        {
            pos.z = Mathf.Sin(Time.time * speed) * strength;
            transform.position = pos;
        }
        
    }
}
