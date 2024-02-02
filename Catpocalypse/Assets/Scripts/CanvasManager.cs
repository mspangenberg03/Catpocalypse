using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static GameObject instance;
    // Start is called before the first frame update
    void Awake()
    {
       
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            //instance = gameObject;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
