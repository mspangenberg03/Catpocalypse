using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroyerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject destroyTowerUI;

    private TowerBase currentSelectedBase;

    public void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetCurrentSelectedBase(TowerBase current)
    {
        currentSelectedBase = current;
    }

    public void OnDestroySelect(Tower tower)
    {
        tower.OnDestroy();
        currentSelectedBase = null;
        this.gameObject.SetActive(false);
    }
}
