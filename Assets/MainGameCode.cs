using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameCode : MonoBehaviour
{
    GameObject enemyShip1;
    GameObject enemyShip2;
    GameObject enemyShip3;
    GameObject enemyShip4;
    GameObject enemyShip5;
    GameObject ship1;
    GameObject ship2;
    float timer = 0;
    float timer2 = 0.1f;
    public int enemyCount = 0;
    public bool gameOver = false;
    public int score = 0;
    bool start = true;
    bool boss = false;
    int waves = 0;
    int multiplier = 1;
    int set = 0;
    

    GameObject main;
    
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera");
        enemyShip1 = GameObject.Find("Enemy1");
        enemyShip2 = GameObject.Find("Enemy2");
        enemyShip3 = GameObject.Find("Enemy3");
        enemyShip4 = GameObject.Find("Enemy4");
        enemyShip5 = GameObject.Find("Enemy5");
        ship1 = GameObject.Find("ship1");
        ship2 = GameObject.Find("ship2");
        ship1.SetActive(false);
        ship2.SetActive(false);
        start = true;
        timer2 = 0.1f;
        waves = 0;
        boss = false;



    }

    // Update is called once per frame
    void Update()
    {
        if(start == true)
        {
            if (timer2 <= 0)
            {
                start = false;
                int shipLevel = main.GetComponent<LoadData2>().shipType;
               
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
                boss = false;
                waves = 0;
            }
            else
            {
                timer2 -= Time.deltaTime;
            }
        }
        
        transform.GetComponentInChildren<Text>().text = "Score = " + score.ToString();
        if (timer <= 0 || enemyCount == 0)
        {

            waves += 1;
            if (waves >= 50)
            {
                boss = true;

                waves -= 50;
            }

            for (int i = 1; i < multiplier; i++)
            {
                if (boss == true)
                {
                    
                        Summon(enemyShip5);
                }
                int num = Random.Range(1, 6);
                timer = 5;
                if (num == 5)
                {
                    int num2 = Random.Range(1, 5);
                    if (num2 == 4)
                    {
                        Summon(enemyShip4);
                    }
                    else
                    {
                        Summon(enemyShip2);
                    }
                }
                else
                {
                    int num2 = Random.Range(1, 5);
                    if (num2 == 4)
                    {
                        Summon(enemyShip3);
                    }
                    else
                    {
                        Summon(enemyShip1);
                    }
                }
            }
            if (boss == true)
            {
                multiplier += 1;
                boss = false;
            }
            

        }
        else
        {
            timer -= Time.deltaTime;
        }
        if(gameOver == true)
        {
            
            if (Input.GetKey(KeyCode.Return))
            {
                main.GetComponent<LoadData2>().money += score;
                main.GetComponent<LoadData2>().WriteData();
                SceneManager.LoadScene("PlayerScene");
            }
        }

        
    }
    public void Summon(GameObject theObject)
    {
        enemyCount += 1;
       GameObject newObject = Instantiate(theObject);
        int temp1 = Random.Range(1, 21);
        int temp2 = Random.Range(1, 3);
        if (temp2 == 1)
        {
            newObject.transform.position = new Vector3(670+temp1, 302, 90);
        }
        else
        {
            newObject.transform.position = new Vector3(670-temp1, 302, 90);
        }
        newObject.GetComponent<enemyMovement>().active = true;
    }
    
}
