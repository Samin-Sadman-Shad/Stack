using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static MovingBlock firstBlock;
    private BlockSpawner[] spawners;
    private int spawnerIndex;
    private BlockSpawner currentSpawner;
    [SerializeField] int score;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] public TextMeshProUGUI startText;
    // Start is called before the first frame update
    void Awake()
    {
        
        startText.enabled = true;
        
        if (Input.GetMouseButtonDown(0))
        {
            spawners = FindObjectsOfType<BlockSpawner>();
            score = 0;
            spawners[0].SpawnBlock();
        }
        spawners = FindObjectsOfType<BlockSpawner>();
        score = 0;
        spawners[0].SpawnBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (MovingBlock.currentBlock != null)
            {
                MovingBlock.currentBlock.Stop();
                score = MovingBlock.score;
                scoreText.SetText("Score: " + score);
            }
        //}
            //FindObjectOfType<BlockSpawner>().SpawnBlock();
            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SpawnBlock();
        }
    }
}
