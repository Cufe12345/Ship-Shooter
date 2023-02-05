using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buy : MonoBehaviour
{
    string type;
    int cost = 10000;
    int multiplier = 10000;
    GameObject main;
    int level = 1;
    GameObject dataL;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        main = GameObject.Find("Main Camera");
        if (transform.name.Contains("UpgradeButton1"))
        {
            type = "health";
            multiplier = 100;
            
        }
        else if (transform.name.Contains("UpgradeButton2"))
        {
            type = "bullet";
            multiplier = 2000;
        }
        else if (transform.name.Contains("UpgradeButton3"))
        {
            type = "ship";
            multiplier = 5000;
        }
        else if (transform.name.Contains("UpgradeButton4"))
        {
            type = "gun";
            multiplier = 50;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (type.Contains("health"))
        {
            level = main.GetComponent<LoadData>().healthLevel;
        }
        else if (type.Contains("bullet"))
        {
            level = main.GetComponent<LoadData>().bulletLevel;
        }
        else if (type.Contains("ship"))
        {
            level = main.GetComponent<LoadData>().shipType;
        }
        else if (type.Contains("gun"))
        {
            level = main.GetComponent<LoadData>().gunLevel;
        }
        cost = level * level * multiplier;
        gameObject.GetComponentInChildren<Text>().text = "Upgrade cost = " + cost;
                

    }
    public void pressed(bool press = false)
    {
        
        if( press == true)
        {
            int moneyAmount = main.GetComponent<LoadData>().money;
            if(moneyAmount < cost)
            {
                main.GetComponent<LoadData>().ErrorMessage();
            }
            else
            {
                
                if (type.Contains("health"))
                {
                    if ((main.GetComponent<LoadData>().healthLevel + 1) > 22)
                    {
                        main.GetComponent<LoadData>().MaxMessage();
                    }
                    else
                    {
                        main.GetComponent<LoadData>().healthLevel += 1;
                        main.GetComponent<LoadData>().money -= cost;
                    }
                }
                else if (type.Contains("bullet"))
                {
                    if ((main.GetComponent<LoadData>().bulletLevel + 1) > 3)
                    {
                        main.GetComponent<LoadData>().MaxMessage();
                    }
                    else
                    {
                        main.GetComponent<LoadData>().bulletLevel += 1;
                        main.GetComponent<LoadData>().money -= cost;
                    }
                }
                else if (type.Contains("ship"))
                {
                    if ((main.GetComponent<LoadData>().shipType + 1) > 2)
                    {
                        main.GetComponent<LoadData>().MaxMessage();
                    }
                    else
                    {
                        main.GetComponent<LoadData>().shipType += 1;
                        main.GetComponent<LoadData>().money -= cost;
                    }
                }
                else if (type.Contains("gun"))
                {
                    if ((main.GetComponent<LoadData>().gunLevel + 1) > 5)
                    {
                        main.GetComponent<LoadData>().MaxMessage();
                    }
                    else
                    {
                        main.GetComponent<LoadData>().gunLevel += 1;
                        main.GetComponent<LoadData>().money -= cost;
                    }
                    
                }
                main.GetComponent<LoadData>().WriteData();
                main.GetComponent<LoadData>().LoadGameData();
            }

        }
    }
}
