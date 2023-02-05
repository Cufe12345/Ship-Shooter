using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthEnemy : MonoBehaviour
{
    public int health = 100;
    GameObject explosion;
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        if (transform.name.Contains("Enemy1"))
        {
            health = 1;
        }
        if (transform.name.Contains("Enemy3"))
        {
            health = 2;
        }
        if (transform.name.Contains("Enemy2"))
        {
            health = 10;
        }
        if (transform.name.Contains("Enemy4"))
        {
            health = 20;
        }
        if (transform.name.Contains("Enemy5"))
        {
            health = 100;
        }
        canvas = GameObject.Find("Canvas");
        explosion = GameObject.Find("Explosion");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newExplosion.GetComponent<Explosion>().destroy = 1;
            canvas.GetComponent<MainGameCode>().enemyCount -= 1;
            int tempScore = 0;
            if (transform.name.Contains("Enemy1"))
            {
                tempScore = 1;
            }
            if (transform.name.Contains("Enemy3"))
            {
                tempScore = 2;
            }
            if (transform.name.Contains("Enemy2"))
            {
                tempScore = 10;
            }
            if (transform.name.Contains("Enemy4"))
            {
                tempScore = 20;
            }
            if (transform.name.Contains("Enemy5"))
            {
                tempScore = 100;
            }
            canvas.GetComponent<MainGameCode>().score += tempScore;
            Destroy(this.gameObject);

        }
        
    }
    
}
