using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerTower : Tower
{
    [SerializeField]
    private GameObject laser;
    [SerializeField]
    private int numOfLasers;
    [SerializeField]
    private GameObject laserPointerTower;

    public void Awake()
    {
        laserPointerTower = this.gameObject;
        laserPointerTower.transform.position = new Vector3(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget != null )
        {
            if(currentTarget.CompareTag("cat") ){
                //currentTarget.GetComponent<NormalCat>.DistractCat(50);
            }
            //laser.SetActive(true);
            //laser.transform.LookAt(currentTarget.transform.position);
            //laser.transform.localScale = new Vector3 (laser.transform.position.x, laser.transform.position.y, Vector3.Distance(currentTarget.transform.position, this.transform.position));
        } else
        {
            //laser.SetActive(false);
        }
    }
}
