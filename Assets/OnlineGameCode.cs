using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineGameCode : MonoBehaviour
{
    bool start = true;
    float timer = 0;
    float timer2 = 0.1f;
    GameObject main;
    GameObject ship1;
    GameObject ship2;
    GameObject bullet1;
    GameObject bullet2;
    GameObject bullet3;
    GameObject enemyShip1;
    GameObject enemyShip2;
    Vector3 enemyPosition;
    NetworkingCode networkCodeScript;
    public bool gameOver = false;
    GameObject ping;
    int pingTime = 0;
    string ipPing;
    float timer3 = 5;
    // Start is called before the first frame update
    void Start()
    {
        ping = GameObject.Find("Ping");
        main = GameObject.Find("Main Camera");
        ship1 = GameObject.Find("ship1");
        ship2 = GameObject.Find("ship2");
        bullet1 = GameObject.Find("EnemyBullet1");
        bullet2 = GameObject.Find("EnemyBullet2");
        bullet3 = GameObject.Find("EnemyBullet3");
        enemyShip1 = GameObject.Find("EnemyOnline1");
        enemyShip2 = GameObject.Find("EnemyOnline2");
        networkCodeScript = main.GetComponent<NetworkingCode>();
        enemyShip1.SetActive(false);
        enemyShip2.SetActive(false);
        ship1.SetActive(false);
        ship2.SetActive(false);
        start = true;
        
    }
    public string ActiveShip()
    {
        if(ship1.activeSelf == true)
        {
            return ship1.transform.name;
        }
        else if(ship2.activeSelf == true)
        {
            return ship2.transform.name;
        }
        else
        {
            return null;
        }
    }
    public string ActiveShipEnemy()
    {
        try
        {
            if (enemyShip1.activeSelf == true)
            {
                return enemyShip1.transform.name;
            }
            else if (enemyShip2.activeSelf == true)
            {
                return enemyShip2.transform.name;
            }
            else
            {
                return null;
            }
        }
        catch (MissingReferenceException)
        {
            return null;
        }
    }
    public void Pinging()
    {
        int timeout = 120;

        while (true)
        {
            try
            {
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                PingReply reply = ping.Send(ipPing, timeout, buffer, options);

                if (reply.Status == IPStatus.Success)
                {
                    pingTime = int.Parse(reply.RoundtripTime.ToString());
                }
            }catch(Exception e)
            {
                Debug.LogError(e);
            }
            Thread.Sleep(5);

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer3 <= 0)
        {
            timer3 = 2;
            pingTime = main.GetComponent<NetworkingCode>().pingTime;
            ping.GetComponent<Text>().text = "Ping: " + pingTime;
        }
        else
        {
            timer3 -= Time.deltaTime;
        }
        if (start == true)
        {
            if (timer2 <= 0)
            {
                start = false;
                int shipLevel = main.GetComponent<LoadData2>().shipType;
                int shipLevelOnline = main.GetComponent<LoadData2>().shipTypeOnline;
                ipPing = main.GetComponent<LoadData2>().position;
                //Thread pingThread = new Thread(Pinging);
                //pingThread.IsBackground = true;
                //pingThread.Start();
                if (shipLevel == 1)
                {
                    ship1.SetActive(true);
                }
                else if (shipLevel == 2)
                {
                    ship2.SetActive(true);
                }
                else
                {
                    ship1.SetActive(true);
                }
                if (shipLevelOnline == 1)
                {
                    enemyShip1.SetActive(true);
                }
                else if (shipLevelOnline == 2)
                {
                    enemyShip2.SetActive(true);
                }
                else
                {
                    Debug.LogError("Couldnt Get Online Ship");
                }

            }
            else
            {
                timer2 -= Time.deltaTime;
            }
        }
        if (gameOver == true)
        {

            if (Input.GetKey(KeyCode.Return))
            {
                // main.GetComponent<LoadData2>().money += score;
                main.GetComponent<LoadData2>().WriteData();
                SceneManager.LoadScene("PlayerScene");
            }
        }
        else
        {
            Vector3 pos = networkCodeScript.enemyPosition;
            //302.1 is top of screen and 277 is bottom of screen         
            float tempY = pos.y - 277;
            tempY = 302.1f - tempY;
            pos.y = tempY;
            if (enemyShip1.activeSelf == true)
            {

                enemyShip1.transform.position = new Vector3(pos.x, pos.y, pos.z);

            }
            else if (enemyShip2.activeSelf == true)
            {
                enemyShip2.transform.position = new Vector3(pos.x, pos.y, pos.z);
            }
            bool enemyFired = networkCodeScript.enemyFiring;

            if (enemyFired == true)
            {
                int bulletLevel = main.GetComponent<LoadData2>().bulletLevelOnline;
                int shipLevel = main.GetComponent<LoadData2>().shipTypeOnline;
                int damage = 1;
                if (shipLevel == 2)
                {
                    GameObject newBullet = null;
                    GameObject newBullet2 = null;

                    if (bulletLevel == 1)
                    {
                        newBullet = Instantiate(bullet1);
                        newBullet2 = Instantiate(bullet1);
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
                        newBullet = Instantiate(bullet1);
                        newBullet2 = Instantiate(bullet1);
                        damage = 1;
                    }



                    newBullet.transform.position = new Vector3(pos.x - 2, pos.y - 1.5f, pos.z);
                    newBullet.GetComponent<bulletFired>().fired = true;
                    newBullet.GetComponent<bulletFired>().enemy = true;
                    newBullet.GetComponent<bulletFired>().damage = damage;


                    newBullet2.transform.position = new Vector3(pos.x + 2, pos.y - 1.5f, pos.z);
                    newBullet2.GetComponent<bulletFired>().fired = true;
                    newBullet2.GetComponent<bulletFired>().enemy = true;
                    newBullet2.GetComponent<bulletFired>().damage = damage;
                }
                else
                {
                    GameObject bullet = null;
                    if (bulletLevel == 1)
                    {
                        bullet = Instantiate(bullet1);
                    }
                    else if (bulletLevel == 2)
                    {
                        bullet = Instantiate(bullet2);
                        damage = 5;
                    }
                    else if (bulletLevel == 3)
                    {
                        bullet = Instantiate(bullet3);
                        damage = 10;
                    }
                    bullet.transform.position = new Vector3(pos.x, pos.y - 2, pos.z);
                    bullet.GetComponent<bulletFired>().enemy = true;
                    bullet.GetComponent<bulletFired>().fired = true;
                    bullet.GetComponent<bulletFired>().damage = damage;
                }

                main.GetComponent<NetworkingCode>().enemyFiring = false;
                enemyFired = false;


            }
            bool enemyHit1 = networkCodeScript.hitEnemy;
            if (enemyHit1 == true)
            {
                enemyHit1 = false;
                int damage = 1;
                int level = main.GetComponent<LoadData2>().bulletLevel;
                if (level == 1)
                {
                    damage = 1;
                }
                else if (level == 2)
                {
                    damage = 5;
                }
                else if (level == 3)
                {
                    damage = 10;
                }

                GameObject.Find(ActiveShipEnemy()).GetComponent<HealthOnlineEnemy>().health -= damage;
                main.GetComponent<NetworkingCode>().hitEnemy = false;
            }

        }
    }
}
