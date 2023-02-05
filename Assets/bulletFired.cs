using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletFired : MonoBehaviour
{
    public bool fired = false;
    public int damage = 1;
    float timer = 10;
    public bool enemy = false;
    float speed = 0.6f;
    float speedE = 0.4f;
    GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (fired == true)
        {
            timer -= Time.deltaTime;
            if (enemy == false)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + (1*speed), transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - (1*speedE), transform.position.z);
            }
        }
        if( timer <= 0)
        {
            Destroy(this.gameObject);
        }
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        GameObject target = collision.gameObject;
        if (enemy == false)
        {
            if (!target.name.Contains("EnemyBullet"))
            {
                if (target.name.Contains("EnemyOnline"))
                {
                    Debug.Log("RUNNING"+" "+damage);
                    //target.GetComponent<HealthOnlineEnemy>().health -= damage;
                }
                else if (target.name.Contains("Enemy"))
                {
                    target.GetComponent<healthEnemy>().health -= damage;
                    Destroy(this.gameObject);

                }
            }
        }
        else
        {
            if (target.name.Contains("ship"))
            {
                try
                {
                    target.GetComponent<healthPlayer>().health -= damage;
                }
                catch (NullReferenceException)
                {
                    target.GetComponent<HealthPlayerOnline>().health -= damage;
                    main.GetComponent<NetworkingCode>().playerHit = true;
                }
                
                
                Destroy(this.gameObject);

            }
        }
    }
}
