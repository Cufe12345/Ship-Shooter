using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    float speed = 0.3f;
    int num = 1;
    float fireTimer = 0;
    GameObject bullet;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        bullet = GameObject.Find("EnemyBullet");
        Random random = new Random();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (active == true)
        {
            
            if (num == 1)
            {
                transform.position = new Vector3(transform.position.x - (1 * speed), transform.position.y, transform.position.z);
            }
            if (num == 2)
            {
                transform.position = new Vector3(transform.position.x + (1 * speed), transform.position.y, transform.position.z);
            }
            if (transform.position.x < 644)
            {
                transform.position = new Vector3(644, transform.position.y, transform.position.z);
                num = 2;
            }
            if (transform.position.x > 696)
            {
                transform.position = new Vector3(696, transform.position.y, transform.position.z);
                num = 1;
            }
            if (transform.position.y < 277)
            {
                transform.position = new Vector3(transform.position.x, 277, transform.position.z);
            }
            if (transform.position.y < 292)
            {
                transform.position = new Vector3(transform.position.x, 292, transform.position.z);
            }
            
            if (fireTimer <= 0)
            {
                if (transform.name.Contains("Enemy1") || transform.name.Contains("Enemy3"))
                {
                    GameObject newBullet = Instantiate(bullet);
                    newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().enemy = true;
                    newBullet.GetComponent<bulletFired>().damage = 1;
                    fireTimer = 0.6f;
                }
                if (transform.name.Contains("Enemy2") || transform.name.Contains("Enemy4"))
                {
                    GameObject newBullet = Instantiate(bullet);
                    newBullet.transform.position = new Vector3(transform.position.x-2, transform.position.y - 1.5f, transform.position.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().enemy = true;
                    newBullet.GetComponent<bulletFired>().damage = 1;
                    fireTimer = 0.6f;
                    GameObject newBullet2 = Instantiate(bullet);
                    newBullet2.transform.position = new Vector3(transform.position.x + 2, transform.position.y - 1.5f, transform.position.z);
                    newBullet2.GetComponent<bulletFired>().fired = true;
                    newBullet2.GetComponent<bulletFired>().enemy = true;
                    newBullet2.GetComponent<bulletFired>().damage = 1;
                    fireTimer = 0.6f;
                }
                if (transform.name.Contains("Enemy5"))
                {
                    GameObject newBullet = Instantiate(bullet);
                    newBullet.transform.position = new Vector3(transform.position.x - 2, transform.position.y - 1.5f, transform.position.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().enemy = true;
                    newBullet.GetComponent<bulletFired>().damage = 2;
                    fireTimer = 0.6f;
                    GameObject newBullet2 = Instantiate(bullet);
                    newBullet2.transform.position = new Vector3(transform.position.x + 2, transform.position.y - 1.5f, transform.position.z);
                    newBullet2.GetComponent<bulletFired>().fired = true;
                    newBullet2.GetComponent<bulletFired>().enemy = true;
                    newBullet2.GetComponent<bulletFired>().damage = 2;
                    fireTimer = 0.6f;
                    GameObject newBullet3 = Instantiate(bullet);
                    newBullet3.transform.position = new Vector3(transform.position.x - 3, transform.position.y - 1.5f, transform.position.z);
                    newBullet3.GetComponent<bulletFired>().fired = true;
                    newBullet3.GetComponent<bulletFired>().enemy = true;
                    newBullet3.GetComponent<bulletFired>().damage = 2;
                    fireTimer = 0.6f;
                    GameObject newBullet4 = Instantiate(bullet);
                    newBullet4.transform.position = new Vector3(transform.position.x + 3, transform.position.y - 1.5f, transform.position.z);
                    newBullet4.GetComponent<bulletFired>().fired = true;
                    newBullet4.GetComponent<bulletFired>().enemy = true;
                    newBullet4.GetComponent<bulletFired>().damage = 2;
                    fireTimer = 0.6f;

                }
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
