using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject sheepPrefab;
    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;

    private List<GameObject> sheepList = new List<GameObject>();
 
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        /*int maxSpawnPoints = sheepSpawnPositions.Count;   The above line does all this
        int randomNum = Random.Range(0, maxSpawnPoints);
        Transform randomSpawnPoint = sheepSpawnPositions[randomNum];
        Vector3 randomPosition = randomSpawnPoint.position;*/
        GameObject sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    //public void SetSpawner(SheepSpawner spawner)
    //{
    //    sheepSpawner = spawner;
    // }

    private IEnumerator SpawnRoutine()  //using a co routine as to not freeze the whole game
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns); //pause and delay firing of sheep
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList)
        {
            Destroy(sheep);
        }

        sheepList.Clear();
    }
}

