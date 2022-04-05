using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    private float lastShotTime;
    private MonsterData monsterData;

    public List<GameObject> enemiesInRange;     //list of enemies in range of the monster

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();

        lastShotTime = Time.time;
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }

        if (target != null)
        {
            if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI, new Vector3(0, 0, 1));
        }
    }

    void OnTriggerEnter2D(Collider2D other) //Add enemies to list
    {
        if (other.gameObject.tag.Equals("Enemy"))
            enemiesInRange.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)  //Remove enemies from list
    {
        if (other.gameObject.tag.Equals("Enemy"))
            enemiesInRange.Remove(other.gameObject);
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;  //Get the start target positions of the bullet
        Vector3 startPosition = gameObject.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;    //set the positions to the bulletPrefab
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);   //Instantiate a new bullet using prefab and monster level
        newBullet.transform.position = startPosition;
        BulletBehaviour bulletComp = newBullet.GetComponent<BulletBehaviour>();
        bulletComp.target = target.gameObject;                  //Asign start positions and target positions
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        Animator animator = monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }
}
