using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static GameObject instance;
    public int basesInScene;
    void Awake()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("TowerBase");
        int baseCount = bases.Count();
        while(baseCount > basesInScene)
        {
            GameObject towerBase = bases[baseCount - 1];
            Destroy(towerBase);
            bases = GameObject.FindGameObjectsWithTag("TowerBase");
            baseCount = bases.Count();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
