using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroyerUI : MonoBehaviour
{
    [SerializeField]
    public GameObject destroyTowerUI;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDestroySelect(Tower tower)
    {
        tower.OnDestroy();
    }
}
