using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthOnlineEnemy : MonoBehaviour
{
    public int health = 100;
    GameObject explosion;
    GameObject canvas;
    GameObject main;
    float timer = 1;
    bool start = true;
    GameObject winPanel;
    GameObject enemyHeart;
    GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Main Camera");
        main = GameObject.Find("Canvas");
        explosion = GameObject.Find("Explosion");
        winPanel = GameObject.Find("WinPanel");
        winPanel.SetActive(false);
        enemyHeart = GameObject.Find("EnemyHeartNumText");
        heart = GameObject.Find("Heart1");
    }

    // Update is called once per frame
    void Update()
    {
       enemyHeart.GetComponent<Text>().text = "x " + health;
        if (start == true)
        {
            if (timer <= 0)
            {
                start = false;
                health = canvas.GetComponent<LoadData2>().healthLevelOnline;
                float x = 687.9f;
                float y = 298.8f;
                GameObject newHeart = Instantiate(heart);
                newHeart.transform.position = new Vector3(x, y, 90);
                enemyHeart.transform.position = new Vector3(x + 5, y, 90);
                newHeart.transform.parent = GameObject.Find("Panel").transform;
                newHeart.transform.localScale = new Vector3(0.8f, 0.8f, 1);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        if(health <= 0)
        {
            GameObject newExplosion = Instantiate(explosion);
            newExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            newExplosion.GetComponent<Explosion>().destroy = 1;
            Destroy(this.gameObject);
            winPanel.SetActive(true);
            main.GetComponent<OnlineGameCode>().gameOver = true;
        }
    }
}
