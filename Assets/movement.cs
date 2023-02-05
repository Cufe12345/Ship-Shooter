using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    GameObject bullet;
    GameObject bullet2;
    GameObject bullet3;
    float speed = 0.5f;
    float weaponCoolDown = 0;
    GameObject main;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera");
        bullet = GameObject.Find("FriendlyBullet1");
        bullet2 = GameObject.Find("FriendlyBullet2");
        bullet3 = GameObject.Find("FriendlyBullet3");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            float x = transform.position.x - 1 * speed;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("d"))
        {
            float x = transform.position.x + 1 * speed;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        if (Input.GetKey("w"))
        {
            float y = transform.position.y + 1 * speed;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (Input.GetKey("s"))
        {
            float y = transform.position.y - 1 * speed;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        if (weaponCoolDown <= 0)
        {
            if (Input.GetKeyDown("space"))
            {
                try
                {
                    main.GetComponent<NetworkingCode>().playerFired = true;
                }
                catch (NullReferenceException)
                {

                }
                GameObject newBullet = null;
                GameObject newBullet2 = null;
                int bulletLevel = 1;
                int gunLevel = 1;
                int shipType = 1;
                bulletLevel = main.GetComponent<LoadData2>().bulletLevel;
                gunLevel = main.GetComponent<LoadData2>().gunLevel;
                shipType = main.GetComponent<LoadData2>().shipType;

                if (shipType == 1)
                {
                    int damage = 1;
                    if (bulletLevel == 1)
                    {
                        newBullet = Instantiate(bullet);
                        damage = 1;
                    }
                    else if (bulletLevel == 2)
                    {
                        newBullet = Instantiate(bullet2);
                        damage = 5;
                    }
                    else if (bulletLevel == 3)
                    {
                        newBullet = Instantiate(bullet3);
                        damage = 10;
                    }
                    else
                    {
                        newBullet = Instantiate(bullet);
                        damage = 1;
                    }

                    newBullet.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().damage = damage;
                    if (gunLevel == 1)
                    {
                        weaponCoolDown = 0.6f;
                    }
                    else if (gunLevel == 2)
                    {
                        weaponCoolDown = 0.5f;
                    }
                    else if (gunLevel == 3)
                    {
                        weaponCoolDown = 0.4f;
                    }
                    else if (gunLevel == 4)
                    {
                        weaponCoolDown = 0.3f;
                    }
                    else if (gunLevel == 5)
                    {
                        weaponCoolDown = 0.2f;
                    }
                    else if (gunLevel == 6)
                    {
                        weaponCoolDown = 0.1f;
                    }
                }
                else
                {
                    int damage = 1;
                    if (bulletLevel == 1)
                    {
                        newBullet = Instantiate(bullet);
                        newBullet2 = Instantiate(bullet);
                        damage = 1;

                    }
                    else if (bulletLevel == 2)
                    {
                        newBullet = Instantiate(bullet2);
                        newBullet2 = Instantiate(bullet2);
                        damage = 5;

                    }
                    else if (bulletLevel == 3)
                    {
                        newBullet = Instantiate(bullet3);
                        newBullet2 = Instantiate(bullet3);
                        damage = 10;
                    }
                    else
                    {
                        newBullet = Instantiate(bullet);
                        newBullet2 = Instantiate(bullet);
                        damage = 1;
                    }
                   

                    
                    newBullet.transform.position = new Vector3(transform.position.x - 2, transform.position.y + 1.5f, transform.position.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().enemy = false;
                    newBullet.GetComponent<bulletFired>().damage = damage;


                    newBullet2.transform.position = new Vector3(transform.position.x + 2, transform.position.y + 1.5f, transform.position.z);
                    newBullet2.GetComponent<bulletFired>().fired = true;
                    newBullet2.GetComponent<bulletFired>().enemy = false;
                    newBullet2.GetComponent<bulletFired>().damage = damage;
                    if (gunLevel == 1)
                    {
                        weaponCoolDown = 0.6f;
                    }
                    else if (gunLevel == 2)
                    {
                        weaponCoolDown = 0.5f;
                    }
                    else if (gunLevel == 3)
                    {
                        weaponCoolDown = 0.4f;
                    }
                    else if (gunLevel == 4)
                    {
                        weaponCoolDown = 0.3f;
                    }
                    else if (gunLevel == 5)
                    {
                        weaponCoolDown = 0.2f;
                    }
                    else if (gunLevel == 6)
                    {
                        weaponCoolDown = 0.1f;
                    }
                    
                    
                    
                }

            }
        }
        else
        {
            weaponCoolDown -= Time.deltaTime;
        }
        if(transform.position.x < 644)
        {
            transform.position = new Vector3(644, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 696)
        {
            transform.position = new Vector3(696, transform.position.y, transform.position.z);
        }
        if (transform.position.y < 277)
        {
            transform.position = new Vector3(transform.position.x,277, transform.position.z);
        }
        if (transform.position.y > 292)
        {
            transform.position = new Vector3(transform.position.x, 292, transform.position.z);
        }


    }
}
