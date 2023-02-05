using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthPlayer : MonoBehaviour
{
    public int health = 3;
    int original;
    GameObject explosion;
    List<GameObject> hearts = new List<GameObject>();
    GameObject gameOver;
    GameObject main;
    bool start = true;
    GameObject main2;
    GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Canvas");
        main2 = GameObject.Find("Main Camera");
        gameOver = GameObject.Find("GameOverPanel");
        gameOver.SetActive(false);
        health = 3;
        heart = GameObject.Find("Heart1");
        explosion = GameObject.Find("Explosion");
       
        original = health;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(start == true)
        {
            start = false;
            int healthLevel = main2.GetComponent<LoadData2>().healthLevel;
            health = (healthLevel - 1) + 3;
            original = health;
            //4.1
            float x = 643.8f;
            float y = 302.8f;
            for(int temp = health; temp > 0; temp--)
            {
                x += 4.1f;
                GameObject newHeart = Instantiate(heart);
                if (x > 680)
                {
                    if (y == 302.8f)
                    {
                        y = 298.8f;
                        x = 647.9f;
                        newHeart.transform.position = new Vector3(x, y, 90f);
                    }
                    else
                    {
                        y = 294.8f;
                        x = 647.9f;
                        newHeart.transform.position = new Vector3(x, y, 90f);
                    }
                }
                else
                {
                    newHeart.transform.position = new Vector3(x, y, 90f);
                }
                newHeart.transform.parent = GameObject.Find("Panel").transform;
                newHeart.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                hearts.Add(newHeart);
                
                
            }
            hearts.Reverse();
        }
        if (health <= 0)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newExplosion.GetComponent<Explosion>().destroy = 1;
            gameOver.SetActive(true);
            GameObject textScore = GameObject.Find("TextScore");
            textScore.GetComponent<Text>().text = "Your Score was: " + main.GetComponent<MainGameCode>().score.ToString();
            main.GetComponent<MainGameCode>().gameOver = true;
            Destroy(this.gameObject);
        }
        if( health < original)
        {
            foreach (GameObject heart in hearts)
            {

                if(heart.activeSelf == true)
                {
                    heart.SetActive(false);
                    original -= 1;
                    break;
                }
            }
        }

    }
}
