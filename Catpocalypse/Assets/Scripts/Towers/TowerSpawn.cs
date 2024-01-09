using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPointerTowerPrefab;

    private TowerBase towerBase;


    // Start is called before the first frame update
    void Start()
    {
        towerBase = this.gameObject.GetComponentInParent<TowerBase>();
    }

    // Update is called once per frame
    public void BuildTower(int towerType)
    {
        switch(towerType)
        {
            default:
                StartCoroutine(SpawnLaserPointerTower());
                break;
        }
    }

    IEnumerator SpawnLaserPointerTower()
    {
        if(towerBase.tower == null)
        {
            towerBase.tower = Instantiate(laserPointerTowerPrefab, this.transform);
            towerBase.tower.transform.position = this.transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
}
