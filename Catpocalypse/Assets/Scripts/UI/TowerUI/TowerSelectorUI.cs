using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelectorUI : MonoBehaviour
{
    [SerializeField]
    public GameObject buildTowerUI;


    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBuildSelect(Tower tower)
    {
        
    }
}
