using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthPlayerOnline : MonoBehaviour
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
    GameObject heartText;
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
        heartText = GameObject.Find("HeartNumText");

        original = health;
    }

    // Update is called once per frame
    void Update()
    {
        heartText.GetComponent<Text>().text = "x " + health;
        if (start == true)
        {
            start = false;
            int healthLevel = main2.GetComponent<LoadData2>().healthLevel;
            
            //4.1
            float x = 647.9f;
            float y = 298.8f;
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.position = new Vector3(x, y, 90);
            heartText.transform.position = new Vector3(x + 5, y, 90);
            newHeart.transform.parent = GameObject.Find("Panel").transform;
            newHeart.transform.localScale = new Vector3(0.8f, 0.8f, 1);



        }
        if (health <= 0)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newExplosion.GetComponent<Explosion>().destroy = 1;
            gameOver.SetActive(true);
            GameObject textScore = GameObject.Find("TextScore");
            textScore.GetComponent<Text>().text = "";
            main.GetComponent<OnlineGameCode>().gameOver = true;
            Destroy(this.gameObject);
        }
        if (health < original)
        {
            foreach (GameObject heart in hearts)
            {

                if (heart.activeSelf == true)
                {
                    heart.SetActive(false);
                    original -= 1;
                    break;
                }
            }
        }

    }
}
