using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonPresses : MonoBehaviour
{
    GameObject mainMenu;
    GameObject shop;
    GameObject info;
    GameObject online;
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        shop = GameObject.Find("ShopPanel");
        mainMenu = GameObject.Find("MainPanel");
        info = GameObject.Find("InfoPanel");
        online = GameObject.Find("OnlinePanel");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start == true)
        {
            info.SetActive(false);
            shop.SetActive(false);
            online.SetActive(false);
            start = false;
        }
        
    }
    public void Shop(bool pressed = false)
    {
        if(pressed == true)
        {
            shop.SetActive(true);
            mainMenu.SetActive(false);
            online.SetActive(false);
        }
    }
    public void Main(bool pressed = false)
    {
        if (pressed == true)
        {
            
            shop.SetActive(false);
            mainMenu.SetActive(true);
            online.SetActive(false);
        }
    }
    public void Play(bool pressed = false)
    {
        if (pressed == true)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public void MainMenu(bool pressed = false)
    {
        if (pressed == true)
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void Info(bool pressed = false)
    {
        if (pressed == true)
        {
            info.SetActive(true);
        }
    }
    public void CloseInfo(bool pressed = false)
    {
        if (pressed == true)
        {
            info.SetActive(false);
        }
    }
    public void VsOnline(bool pressed = false)
    {
        if(pressed == true)
        {
            SceneManager.LoadScene("ConnectScene");
        }
    }
    public void Online(bool pressed = false)
    {
        if (pressed == true)
        {
            online.SetActive(true);
            mainMenu.SetActive(false);
            shop.SetActive(false);
        }
    }

}
