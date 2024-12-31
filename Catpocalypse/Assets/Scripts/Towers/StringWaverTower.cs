using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWaverTower : Tower
{

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        ApplyScrapUpgrades();
        StartCoroutine(DistractCat());
    }

    protected override void ApplyScrapUpgrades()
    {
        if (PlayerDataManager.Instance.CurrentData.stringUpgrades > 0)
        {
            fireRate *= PlayerDataManager.Instance.Upgrades.StringWaverFrequencyUpgrade;
            if (PlayerDataManager.Instance.CurrentData.stringUpgrades > 1)
            {
                range.radius *= PlayerDataManager.Instance.Upgrades.StringWaverRangeUpgrade;
                if (PlayerDataManager.Instance.CurrentData.stringUpgrades > 2)
                {
                    distractValue *= PlayerDataManager.Instance.Upgrades.StringWaverDistractValueUpgrade;
                    if (PlayerDataManager.Instance.CurrentData.stringUpgrades > 3)
                    {
                        if (PlayerDataManager.Instance.CurrentData.stringUpgrades > 4)
                        {

                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            targets.Add(other.gameObject);
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            targets.Remove(other.gameObject);
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
    }
    IEnumerator DistractCat()
    {
        List<GameObject> cats = targets;

        ////if (cats.Current != null)
        if (cats.Count > 0) 
        {
            _towerSound.Play();
            //GameObject cat = cats.Current;
            //Debug.LogWarning(cat);
            //do
            //{
            //    if (targets.Contains(cat) && cat != null)
            //    {
            //        cat.GetComponent<CatBase>().DistractCat(distractValue, this.gameObject.GetComponent<Tower>());


            //    }
            //} while (cats.MoveNext());
            //foreach (GameObject cat1 in cats)
            for(int i = 0; i< cats.Count; i++) 
            {
                if (cats[i] != null && i < cats.Count)
                {
                    cats[i].GetComponent<CatBase>().DistractCat(distractValue, this.gameObject.GetComponent<Tower>());


                }
            }
        }
        yield return new WaitForSeconds(towerStats.FireRate);
        StartCoroutine(DistractCat());
    }
}
