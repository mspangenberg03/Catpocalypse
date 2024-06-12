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

    [SerializeField]
    private GameObject yarnBallTowerPrefab;

    [SerializeField]
    private GameObject nonAllergicPrefab;
    private TowerBase towerBase;


    // Start is called before the first frame update
    void Start()
    {
        towerBase = this.gameObject.GetComponentInParent<TowerBase>();
    }

    public float MoneyToSpend(int tower) 
    {
        switch(tower)
        {
            case 0:
                return laserPointerTowerPrefab.GetComponent<LaserPointerTower>().GetBuildCost();
            case 1:
                return scratchPostTowerPrefab.GetComponent<ScratchingPostTower>().GetBuildCost();
            case 2:
                return cucumberTowerPrefab.GetComponent<CucumberTower>().GetBuildCost();
            case 3:
                return stringWaverTowerPrefab.GetComponent<StringWaverTower>().GetBuildCost();
            case 4:
                return yarnBallTowerPrefab.GetComponent<YarnBallTower>().GetBuildCost();
            case 5:
                return nonAllergicPrefab.GetComponent<NonAllergicTower>().GetBuildCost();
        }
        return 0;
    }
    
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
            case 4:
                StartCoroutine(SpawnYarnBallTower());
                break;
            case 5:
                StartCoroutine(SpawnNATower());
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
            towerBase.tower = Instantiate(laserPointerTowerPrefab, transform, true);

            LaserPointerTower laserPointerTower = laserPointerTowerPrefab.GetComponent<LaserPointerTower>();
            towerBase.refundVal = laserPointerTower.BuildCost * laserPointerTower.GetRefundPercentage();

            towerBase.tower.transform.position = transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnScratchPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(scratchPostTowerPrefab, transform, true);

            ScratchingPostTower scratchingPostTower = scratchPostTowerPrefab.GetComponent<ScratchingPostTower>();
            towerBase.refundVal = scratchingPostTower.towerStats.BuildCost * scratchingPostTower.GetRefundPercentage();

            towerBase.tower.transform.position = transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnCucumberPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(cucumberTowerPrefab, transform, true);

            CucumberTower cucumberTower = cucumberTowerPrefab.GetComponent<CucumberTower>();
            towerBase.refundVal = cucumberTower.towerStats.BuildCost * cucumberTower.GetRefundPercentage();

            towerBase.tower.transform.position = transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnStringWaverPostTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(stringWaverTowerPrefab, transform, true);

            StringWaverTower stringWaverTower = stringWaverTowerPrefab.GetComponent<StringWaverTower>();
            towerBase.refundVal = stringWaverTower.towerStats.BuildCost * stringWaverTower.GetRefundPercentage();

            towerBase.tower.transform.position = transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
    IEnumerator SpawnYarnBallTower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(yarnBallTowerPrefab, transform, true);

            YarnBallTower yarnBallTower = yarnBallTowerPrefab.GetComponent<YarnBallTower>();
            towerBase.refundVal = yarnBallTower.towerStats.BuildCost * yarnBallTower.GetRefundPercentage();

            towerBase.tower.transform.position = transform.position;
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
  
    }
    IEnumerator SpawnNATower()
    {
        if (towerBase.tower == null)
        {
            towerBase.tower = Instantiate(nonAllergicPrefab, transform);

            NonAllergicTower nonAllergic = nonAllergicPrefab.GetComponent<NonAllergicTower>();
            towerBase.refundVal = nonAllergic.towerStats.BuildCost * nonAllergic.GetRefundPercentage();

            towerBase.tower.transform.position = new Vector3(towerBase.tower.transform.position.x, transform.position.y + 1, transform.position.z);
            towerBase.hasTower = true;
        }

        yield return new WaitForSeconds(2f);
    }
}
