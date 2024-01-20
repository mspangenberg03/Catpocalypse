using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TowerInfoCollection : MonoBehaviour
{
    [SerializeField]
    private GameObject laserPointerTowerPrefab;
    
    [SerializeField]
    private GameObject scratchingPostTowerPrefab;

    [SerializeField]
    private GameObject cucumberTowerPrefab;

    [SerializeField]
    private GameObject stringWaverTowerPrefab;

    [SerializeField]
    private GameObject yarnBallTowerPrefab;



    private List<TowerInfo> _TowerInfoList = new List<TowerInfo>();
    


    private void Awake()
    {
        // Load all of the TowerInfo scriptable objects in the project, even if they
        // aren't inside the TowerPopupsInfo folder.
        _TowerInfoList = Resources.LoadAll<TowerInfo>("").ToList();

        SyncTowerInfosWithPrefabs();
    }

    /// <summary>
    /// This function copies necessary values from the tower prefabs, so the UI stays up-to-date even
    /// when the tower prefabs are modified.
    /// </summary>
    private void SyncTowerInfosWithPrefabs()
    {
        TowerInfo laserTowerInfo = GetTowerInfo(TowerInfo.TowerTypes.LaserPointer);
        Tower laserPointerTower = laserPointerTowerPrefab.GetComponent<Tower>();
        laserTowerInfo.Cost = laserPointerTower.BuildCost;

        TowerInfo scratchingPostTowerInfo = GetTowerInfo(TowerInfo.TowerTypes.ScratchingPost);
        Tower scratchingPostTower = scratchingPostTowerPrefab.GetComponent<Tower>();
        scratchingPostTowerInfo.Cost = scratchingPostTower.BuildCost;

        TowerInfo cucumberTowerInfo = GetTowerInfo(TowerInfo.TowerTypes.CucumberThrower);
        Tower cucumberTower = cucumberTowerPrefab.GetComponent<Tower>();
        cucumberTowerInfo.Cost = cucumberTower.BuildCost;

        TowerInfo stringWaverTowerInfo = GetTowerInfo(TowerInfo.TowerTypes.StringWaver);
        Tower stringWaverTower = stringWaverTowerPrefab.GetComponent<Tower>();
        stringWaverTowerInfo.Cost = stringWaverTower.BuildCost;

        TowerInfo yarnBallTowerInfo = GetTowerInfo(TowerInfo.TowerTypes.YarnBall);
        Tower yarnBallTower = yarnBallTowerPrefab.GetComponent<Tower>();
        yarnBallTowerInfo.Cost = yarnBallTower.BuildCost;
    }

    public TowerInfo GetTowerInfo(TowerInfo.TowerTypes towerType)
    {
        for (int i = 0; i < _TowerInfoList.Count; i++)
        {
            if (_TowerInfoList[i].TowerType == towerType)
                return _TowerInfoList[i];
        }

        return null;
    }
}
