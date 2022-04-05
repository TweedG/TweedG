using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject testEnemyPrefab;

    public Wave[] waves;
    public int timeBetweenWaves = 5;

    private GameManagerBehaviour gameManager;

    private float lastSpawnTime;
    private int enemiesSpawned = 0;

    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnInterval = 2;
        public int maxEnemies = 20;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastSpawnTime = Time.time;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentWave = gameManager.Wave;     //check if current wave is the last wave or not
        if (currentWave < waves.Length)     
        {
            float timeInterval = Time.time - lastSpawnTime; //calculate time passed between spawns
            float spawnInterval = waves[currentWave].spawnInterval;

            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || (enemiesSpawned!= 0 && timeInterval > spawnInterval)) &&
            (enemiesSpawned < waves[currentWave].maxEnemies))   // if we haven't spawned all the enemies, and the right time has passed between spawns, spawn an enemy!
            {
                lastSpawnTime = Time.time;
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                enemiesSpawned++;
            }
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)   //checks if the wave is over/all enemies have spawned
            {
                gameManager.Wave++;
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                enemiesSpawned = 0;
                lastSpawnTime = Time.time;
            }
        }
        else           //current wave is NOT less than max enemies
        {
            gameManager.gameOver = true;
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
        }
    }
}
