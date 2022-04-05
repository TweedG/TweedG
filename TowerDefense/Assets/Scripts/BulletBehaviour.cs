using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 10;    //how quickly the bullets fly
    public int damage;
    public GameObject target;   //Target refers to the enemy
    public Vector3 startPosition;
    public Vector3 targetPosition;
    private Vector3 normalizeDirection;     //Used to standardise vectors, if we didn't do this, than a bullet shot at a closer enemy would move faster than one further away.
    private GameManagerBehaviour gameManager;   //increase the player's Gold when an enemy is destroyed.

    // Start is called before the first frame update
    void Start()
    {
        normalizeDirection = (targetPosition - startPosition).normalized; //normalize the difference between targetPosition and startPosition
        GameObject gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManagerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += normalizeDirection * speed * Time.deltaTime;  //updates the bullets position along the normalised vector
    }

    void OnTriggerEnter2D(Collider2D other) //Checks for a collision with an enemy
    {
        target = other.gameObject;
        if (target.tag.Equals("Enemy"))
        {
            Transform healthBarTransform = target.transform.Find("HealthBar");
            HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
            healthBar.currentHealth -= damage;  //decrease enemy health bar by damage

            if (healthBar.currentHealth <= 0)   //If the health is less than or equal to 0, destroy the enemy
            {
                Destroy(target);
                AudioSource audioSource = target.GetComponent<AudioSource>();   
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);  //play sound effect
                gameManager.Gold += 50; //increase players gold by 50
            }
            Destroy(gameObject);
        }
    }
}
