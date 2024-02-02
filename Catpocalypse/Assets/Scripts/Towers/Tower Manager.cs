using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static GameObject instance;
    void Awake()
    {
        
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            
        }
    }

}
