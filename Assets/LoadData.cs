using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    bool loaded = false;
    string path1;
    string path2;
    string path3;
    string path4;
    string path5;
    public int gunLevel = 0;
    public int bulletLevel = 0;
    public int healthLevel = 0;
    public int shipType = 0;
    public int money = 0;
    bool needWrite = false;
    GameObject errorText;
    GameObject maxLevelText;
    float errorTimer = 0;
    float maxTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        maxLevelText = GameObject.Find("MaxLevelText");
        maxLevelText.SetActive(false);
        errorText = GameObject.Find("ErrorText");
        errorText.SetActive(false);
        path1 = "Assets/GameData/HealthLevel.txt";
        path2 = "Assets/GameData/BulletLevel.txt";
        path3 = "Assets/GameData/GunLevel.txt";
        path4 = "Assets/GameData/ShipType.txt";
        path5 = "Assets/GameData/MoneyValue.txt";

        /*Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);*/


        //Print the text from the file


        //Read the text from directly from the test.txt file



    }

    // Update is called once per frame
    void Update()
    {
        if (errorTimer <= 0)
        {
            errorText.SetActive(false);
        }
        else
        {
            errorTimer -= Time.deltaTime;
        }
        if (maxTimer <= 0)
        {
            maxLevelText.SetActive(false);
        }
        else
        {
            maxTimer -= Time.deltaTime;
        }
        if (loaded == false)
        {
            loaded = true;
            LoadGameData();

        }
        
    }
    public void LoadGameData()
    {
        for (int i = 1; i < 6; i++)
        {
            string path = "null";
            if (i == 1)
            {
                path = path1;
            }
            else if (i == 2)
            {
                path = path2;
            }
            else if (i == 3)
            {
                path = path3;
            }
            else if (i == 4)
            {
                path = path4;
            }
            else if (i == 5)
            {
                path = path5;
            }
            //StreamReader and writer code from https://support.unity.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
            StreamReader reader1 = new StreamReader(path);
            string data2 = reader1.ReadLine();
           
            if (data2 == null)
            {
                needWrite = true;
            }
            else if(data2 == "0")
            {
                needWrite = true;
            }
            else if(data2 == "")
            {
                needWrite = true;
            }
            else
            {
                
                if (i == 1)
                {
                    healthLevel = Convert.ToInt32(data2);
                    
                }
                else if (i == 2)
                {
                    bulletLevel = Convert.ToInt32(data2);
                }
                else if (i == 3)
                {
                    gunLevel = Convert.ToInt32(data2);
                }
                else if (i == 4)
                {
                    shipType = Convert.ToInt32(data2);
                }
                else if (i == 5)
                {
                    money = Convert.ToInt32(data2);
                }
            }
            reader1.Close();
            if(needWrite == true)
            {
                needWrite = false;
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();

                //Re-import the file to update the reference in the editor

                i -= 1;
            }
        }

    }
    public void WriteData()
    {
        string path = "";
        
        for (int i = 1; i < 6; i++)
        {
            int data = 0;
            if (i == 1)
            {
                path = path1;
                data = healthLevel;
            }
            else if (i == 2)
            {
                path = path2;
                data = bulletLevel;
            }
            else if (i == 3)
            {
                path = path3;
                data = gunLevel;
            }
            else if (i == 4)
            {
                path = path4;
                data = shipType;
            }
            else if (i == 5)
            {
                path = path5;
                data = money;
            }
            StreamWriter writer = new StreamWriter(path, false);
            
            writer.WriteLine(data);
            writer.Close();

            //Re-import the file to update the reference in the editor
           
        }

    }
    public void ErrorMessage()
    {
        errorText.SetActive(true);
        errorTimer = 5;
    }
    public void MaxMessage()
    {
        maxLevelText.SetActive(true);
        maxTimer = 5;
    }
}
