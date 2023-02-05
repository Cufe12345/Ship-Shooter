using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LoadData2 : MonoBehaviour
{
    bool loaded = false;
    string path1;
    string path2;
    string path3;
    string path4;
    string path5;
    string path6;
    string path7;
    string path8;
    string path9;
    string path10;
    public int gunLevel = 0;
    public int bulletLevel = 0;
    public int healthLevel = 0;
    public int shipTypeOnline = 0;
    public int gunLevelOnline = 0;
    public int bulletLevelOnline = 0;
    public int healthLevelOnline = 0;
    public int shipType = 0;
    public int money = 0;
    public string position = null;
    bool needWrite = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        
        path1 = "Assets/GameData/HealthLevel.txt";
        path2 = "Assets/GameData/BulletLevel.txt";
        path3 = "Assets/GameData/GunLevel.txt";
        path4 = "Assets/GameData/ShipType.txt";
        path5 = "Assets/GameData/MoneyValue.txt";
        path6 = "Assets/GameData/HealthLevelOnline.txt";
        path7 = "Assets/GameData/GunLevelOnline.txt";
        path8 = "Assets/GameData/ShipTypeOnline.txt";
        path9 = "Assets/GameData/BulletLevelOnline.txt";
        path10 = "Assets/GameData/Position.txt";

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
        
        if (loaded == false)
        {
            loaded = true;
            LoadGameData();

        }

    }
    public void LoadPosition()
    {
        string path = path10;
        StreamReader reader1 = new StreamReader(path);
        string data2 = reader1.ReadLine();

        if (data2 == null)
        {
            needWrite = true;
        }
        else if (data2 == "0")
        {
            needWrite = true;
        }
        else if (data2 == "")
        {
            needWrite = true;
        }
        position = data2;
        reader1.Close();
    }
    public void LoadGameData()
    {
        for (int i = 1; i < 10; i++)
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
            else if (i == 6)
            {
                path = path6;
            }
            else if (i == 7)
            {
                path = path7;
            }
            else if (i == 8)
            {
                path = path8;
            }
            else if (i == 9)
            {
                path = path9;
            }
           
            //StreamReader and writer code from https://support.unity.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
            StreamReader reader1 = new StreamReader(path);
            string data2 = reader1.ReadLine();

            if (data2 == null)
            {
                needWrite = true;
            }
            else if (data2 == "0")
            {
                needWrite = true;
            }
            else if (data2 == "")
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
                else if (i == 6)
                {
                    healthLevelOnline = Convert.ToInt32(data2);
                }
                else if (i == 7)
                {
                    gunLevelOnline = Convert.ToInt32(data2);
                }
                else if (i == 8)
                {
                    shipTypeOnline = Convert.ToInt32(data2);
                }
                else if (i == 9)
                {
                    bulletLevelOnline = Convert.ToInt32(data2);
                }
                
            }
            reader1.Close();
            if (needWrite == true)
            {
                needWrite = false;
                StreamWriter writer = new StreamWriter(path, false);
                writer.WriteLine("1");
                writer.Close();

                
                
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
    
}
