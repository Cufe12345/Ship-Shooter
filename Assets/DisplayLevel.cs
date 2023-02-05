using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLevel : MonoBehaviour
{
    GameObject main;
    int data = 0;
    bool first = true;
    float timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        main = GameObject.Find("Main Camera");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(first == true)
        {
            first = false;
            main.GetComponent<LoadData>().LoadGameData();

        }

        if (transform.name.Contains("Health"))
        {
            data = main.GetComponent<LoadData>().healthLevel;
            transform.GetComponent<Text>().text = "Health Level = " + data;
        }
        else if (transform.name.Contains("Bullet"))
        {
            data = main.GetComponent<LoadData>().bulletLevel;
            transform.GetComponent<Text>().text = "Bullet Level = " + data;
        }
        else if (transform.name.Contains("Gun"))
        {
            data = main.GetComponent<LoadData>().gunLevel;
            transform.GetComponent<Text>().text = "Gun Level = " + data;
        }
        else if (transform.name.Contains("Ship"))
        {
            data = main.GetComponent<LoadData>().shipType;
            transform.GetComponent<Text>().text = "Ship Type = " + data;
        }
        else if (transform.name.Contains("Money"))
        {
            data = main.GetComponent<LoadData>().money;
            transform.GetComponent<Text>().text = "Money = " + data;
        }

    }
}
