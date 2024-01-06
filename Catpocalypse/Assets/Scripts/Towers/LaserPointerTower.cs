using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    [SerializeField]
    public GameObject laser;
    [SerializeField]
    public int numOfLasers;
    [SerializeField]
    public GameObject laserPointerPrefab;

    public LaserPointerTower(TowerBase build)
    {
        baseOfTower = build;
        Instantiate(laserPointerPrefab, build.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null )
        {
            laser.SetActive( true );
            laser.transform.LookAt(currentTarget.transform.position);
            laser.transform.localScale = new Vector3 (laser.transform.position.x, laser.transform.position.y, Vector3.Distance(currentTarget.transform.position, this.transform.position));
        }
    }
}
