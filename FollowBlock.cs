using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBlock : MonoBehaviour
{
    float initialOffset;
    // Start is called before the first frame update
    void Start()
    {
        initialOffset = transform.position.y -1.5f ;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, MovingBlock.currentBlock.transform.position.y + initialOffset, transform.position.z);
    }
}
