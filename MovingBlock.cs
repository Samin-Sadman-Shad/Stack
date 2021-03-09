using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class MovingBlock : MonoBehaviour
{
    [SerializeField] GameObject veryFirstBlock;
    [SerializeField] public static MovingBlock currentBlock { get; private set; }
    [SerializeField] public static MovingBlock lastBlock { get; private set; }
    public BlockSpawner.MoveDirection MoveDirection { get; set; }

    public float movingSpeed;
    public float hangOver;
    public static int score;

    public static TextMeshProUGUI scoreCard;
    public TextMeshProUGUI startText;

    private void OnEnable()
    {
        /* i) set the first block
         * ii) set every moving block to contain this script
         * iii) change the color of every block
         * iv) set the scale(z) of every block
         * position is set at BlockSpawner
        */
        //Debug.Log(gameObject.name);
        
        if(lastBlock == null)
        /* 1) if last block is empty, when the game is beginning,set the 
         * First block to be the last block
         */
        {
            //lastBlock = this;
            lastBlock = veryFirstBlock.GetComponent<MovingBlock>();
            //Debug.Log("last block is " + lastBlock);
            //lastBlock = GameObject.Find("First Block").GetComponent<MovingBlock>();
        }
        /* 2) set the moving block to follow this script  */
        currentBlock = this;
        //Debug.Log("current block is " + currentBlock);
        //whenever a new cube instantiated/enabled, current cube will be set to that

        /* 3) change the color of every block */
        GetComponent<Renderer>().material.color = GetRandomColor();

        /* 4) set the scale of every moving block */
        transform.localScale = new Vector3(lastBlock.transform.localScale.x,
                transform.localScale.y, lastBlock.transform.localScale.z);

    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    internal void Stop()
    {
        movingSpeed = 0;
        hangOver = GetHangOver();

        float max = MoveDirection == BlockSpawner.MoveDirection.Z ? lastBlock.transform.localScale.z : lastBlock.transform.localScale.x;
        if (Mathf.Abs(hangOver) >= max)
        //if the hangOver is greater than the size of the last cube, the gaqme is over
        {
            lastBlock = null;
            currentBlock = null;
            //StartCoroutine(ScoreScreen());
            score = 0;
            SceneManager.LoadScene(0);
             
            //startText.gameObject.SetActive(true);
            //score = 0;
            /*load a new scene
             * clear the lastBlock and currentBlock as they are static and they
             * won't get clear by themselves
              */
        }
        else
        {
            score += 1;
        }
        //Debug.Log("last block's z "+lastBlock.transform.position.z);
        //Debug.Log("block's z "+transform.position.z);
        //Debug.Log("hangOver's value "+hangOver);
        float direction = hangOver > 0 ? 1f : -1f;

        if (MoveDirection == BlockSpawner.MoveDirection.Z)
        {
            CutBlockOnZ(hangOver, direction);
        }
        else
        {
            CutBlockOnX(hangOver, direction);
        }

        lastBlock = currentBlock;
    }

    private float GetHangOver()
    {
        if(MoveDirection == BlockSpawner.MoveDirection.Z)
        {
            return transform.position.z - lastBlock.transform.position.z;
        }
        else
        {
            return transform.position.x - lastBlock.transform.position.x;
        }
        
    }

    private void CutBlockOnX(float hangOver, float direction)
    {
        float newXSize = Mathf.Abs(lastBlock.transform.localScale.x - Mathf.Abs(hangOver));
        float fallingObjectSize = transform.localScale.x - newXSize;
        //Debug.Log("size of new " + newXSize);

        float newXPos = lastBlock.transform.position.x + (hangOver / 2);
        transform.localScale = new Vector3(newXSize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXSize * direction) / 2;
        float fallingBlockZPosition = cubeEdge + (fallingObjectSize * direction) / 2;

        SpawnBlockToDrop(fallingBlockZPosition, fallingObjectSize);
    }

    private void CutBlockOnZ(float hangOver, float direction)
    {
        float newZSize = Mathf.Abs(lastBlock.transform.localScale.z - Mathf.Abs(hangOver));
        float fallingObjectSize = transform.localScale.z - newZSize;
        //Debug.Log("size of new " + newZSize);

        float newZPos = lastBlock.transform.position.z + (hangOver / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);

        float cubeEdge = transform.position.z + (newZSize * direction) / 2;
        float fallingBlockZPosition = cubeEdge + (fallingObjectSize * direction) / 2;

        SpawnBlockToDrop(fallingBlockZPosition, fallingObjectSize);
    }

    void SpawnBlockToDrop(float fallingBlockPos, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if(MoveDirection == BlockSpawner.MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockPos);

        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockPos, transform.position.y, transform.position.z);
        }

        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        cube.AddComponent<Rigidbody>();
        Destroy(cube, 1.5f);
    }

    IEnumerator ScoreScreen()
    {
        scoreCard.gameObject.SetActive(true);
        scoreCard.SetText("Your score is " + score);
        /*
        if (Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        */
        //else
        
            yield return new WaitForSeconds(5);
        
    }
    // Update is called once per frame
    void Update()
    {
        if(MoveDirection == BlockSpawner.MoveDirection.Z)
        {
            transform.position += Vector3.forward * Time.deltaTime * movingSpeed;
        }
        else
        {
            transform.position += Vector3.right * Time.deltaTime * movingSpeed;
        }
        
    }
}
