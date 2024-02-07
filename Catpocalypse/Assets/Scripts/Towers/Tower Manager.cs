using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static GameObject instance;
    void Awake()
    {
        //instance = gameObject;
        if (instance != null)
        {
            Destroy(instance.gameObject);
            //instance = gameObject;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = gameObject;
        }
        //
    }

}
