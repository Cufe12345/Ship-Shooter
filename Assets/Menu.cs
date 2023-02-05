using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    GameObject canvas1;
    GameObject canvas2;
    // Start is called before the first frame update
    void Start()
    {
        canvas1 = GameObject.Find("Canvas1");
        canvas2 = GameObject.Find("Canvas2");
        canvas2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HowToPlay(bool on = false)
    {
        if(on == true)
        {
            on = false;
            canvas1.SetActive(false);
            canvas2.SetActive(true);
        }
    }
    public void Main(bool on = false)
    {
        if (on == true)
        {
            on = false;
            canvas2.SetActive(false);
            canvas1.SetActive(true);
        }
    }
    public void Start(bool on = false)
    {
        if (on == true)
        {
            on = false;
            SceneManager.LoadScene("PlayerScene");
        }
    }
}
