using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int destroy = 0;
    float timer = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(destroy == 1)
        {
            if(timer <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        
    }
}
