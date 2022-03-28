using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    [Header("GAME LEVELS")]
    [SerializeField] private float roundTime;

    [Header("PLATFORM SPAWN")]
    [SerializeField] private float spawnRate;
    [SerializeField] private int platformYPosMin;
    [SerializeField] private int platformYPosMax;
    [SerializeField] private int newPlatformXPosOffsetMin;
    [SerializeField] private int newPlatformXPosOffsetMax;
    [SerializeField] private int newPlatformYPosOffsetMin;
    [SerializeField] private int newPlatformYPosOffsetMax;
    [SerializeField] private int xPos;

    private int maxNums;
    private float timer, spawnTimer;
    private Vector2 lastPlatformPos;
    private int yPos;
    private bool gameOver, canSpawn;

    public bool GameOver { get { return gameOver; } set { gameOver = value; } }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        IniGame();
    }

    void Update()
    {
        if (maxNums < 9) RoundsControl();

        if (Input.GetKeyDown(KeyCode.R)) Restart(); //TESTING <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<< !!
    }

    private void RoundsControl()
    {
        timer += Time.deltaTime;
        if(timer >= roundTime)
        {
            maxNums++;
            timer = 0;
        }
    }

    private void SpawnPlatform()
    {
        int randX = Random.Range(newPlatformXPosOffsetMin, newPlatformXPosOffsetMax);
        int randY = Random.Range(newPlatformYPosOffsetMin, newPlatformYPosOffsetMax);
        int platformLength = Random.Range(3, 7);

        xPos += platformLength + 4;
        yPos = Mathf.Clamp(yPos + randY, platformYPosMin, platformYPosMax);

        GameObject newPlatform = PlatformPool.instance.RequestPlatform(platformLength);
        newPlatform.transform.position = new Vector2(xPos, yPos);
        newPlatform.SetActive(true);

        if(!GameController.instance.GameOver) Invoke("SpawnPlatform", spawnRate);
    }

    private void IniGame()
    {
        maxNums = 1;
        SpawnPlatform();
        canSpawn = true;
    }

    public int MaxNums()
    {
        return maxNums;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
