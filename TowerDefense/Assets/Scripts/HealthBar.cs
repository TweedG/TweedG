using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth = 100;
    private float originalScale;

    // Start is called before the first frame update
    void Start()
    {
        originalScale = gameObject.transform.localScale.x;  //state original health bar size
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tmpScale = gameObject.transform.localScale; //calculates the sacle of the health bar
        tmpScale.x = currentHealth / maxHealth * originalScale;
        gameObject.transform.localScale = tmpScale; 
    }
}
