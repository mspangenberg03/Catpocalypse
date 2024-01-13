using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPointerTowerPrefab;
    [SerializeField]
    private GameObject scratchPostTowerPrefab;

    [SerializeField]
    private GameObject cucumberTowerPrefab;

    [SerializeField]
    private GameObject stringWaverTowerPrefab;

    private TowerBase towerBase;


    // Start is called before the first frame update
    void Start()
    {
        towerBase = this.gameObject.GetComponentInParent<TowerBase>();
    }

    // Update is called once per frame
    public void BuildTower(int towerType)
    {
        transform.parent.GetComponent<TowerBase>().Deselect();

        switch(towerType)
        {
            case 0:
                StartCoroutine(SpawnLaserPointerTower());
                break;
            case 1:
                StartCoroutine(SpawnScratchPostTower());
                break;
            case 2:
                StartCoroutine(SpawnCucumberPostTower());
                break;
            case 3:
                StartCoroutine(SpawnStringWaverPostTower());
                break;
                //default:
                //    StartCoroutine(SpawnLaserPointerTower());
                //    break;
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
    IEnumerator SpawnScratchPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(scratchPostTowerPrefab, this.transform);
            towerBase.tower.transform.position = this.transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnCucumberPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(cucumberTowerPrefab, this.transform);
            towerBase.tower.transform.position = this.transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnStringWaverPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(stringWaverTowerPrefab, this.transform);
            towerBase.tower.transform.position = this.transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
}
