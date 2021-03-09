using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    
    [SerializeField] private MovingBlock cubePrefab;
    [SerializeField] private MovingBlock FirstPrefab;
    [SerializeField] private MoveDirection moveDirection;

    /* when a block is spawned, it takes moveDirection from spawner
     * spawner tells what is the move direction*/

    public void SpawnBlock()
    {
        //Vector3 instantiatePosition = cubePrefab.transform.position;
        //Debug.Log(instantiatePosition);
        //Debug.Log("scale at y " + transform.localScale.y);
        ////Debug.Log(MovingBlock.lastBlock.name);
        //Debug.Log("height of lastBlock" + MovingBlock.lastBlock.transform.localScale.y);
        var cube = Instantiate(cubePrefab);
        cube.gameObject.SetActive(true);

        
        if(MovingBlock.lastBlock != null && MovingBlock.lastBlock.gameObject != GameObject.Find("First Block"))
        { 
            Debug.Log(MovingBlock.lastBlock);
            Debug.Log(MovingBlock.lastBlock.transform.position.y);
            //Debug.Log("no");
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingBlock.lastBlock.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingBlock.lastBlock.transform.position.z;
            
            cube.transform.position = new Vector3(x,
            MovingBlock.lastBlock.transform.position.y + cubePrefab.transform.localScale.y,
            z);
            //cube.transform.localScale = cubePrefab.transform.localScale;
        }
        else 
        {
            //Debug.Log("yes");
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }

    public enum MoveDirection
    {
        X, Z
    }
}
