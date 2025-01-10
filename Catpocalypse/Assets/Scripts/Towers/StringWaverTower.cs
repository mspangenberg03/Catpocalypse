using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWaverTower : Tower
{
    [Header("String Fling variables")]
    [SerializeField,Tooltip("How much the String Fling ability distracts cats")]
    private float stringFlingDistractValue = 5;
    [SerializeField,Tooltip("How long the String Fling slowing effect lasts")]
    private int stringFlingSlowingDuration = 1;
    [SerializeField,Tooltip("How long the String Fling ability takes to cooldown")]
    private int stringFlingCooldown = 20;
    public float _speedDebuff = 1.8f;
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
        StartCoroutine(StringFling());
    }
    IEnumerator StringFling()
    {
        if (targets.Count > 0) 
        {
            foreach (GameObject cat in targets)
            {
                cat.GetComponent<CatBase>().DistractCat(stringFlingDistractValue, gameObject.GetComponent<Tower>());
                cat.GetComponent<CatBase>().slowingEntities.Add(gameObject);
            }
            yield return new WaitForSeconds(stringFlingSlowingDuration);
            foreach (GameObject cat in targets)
            {
                cat.GetComponent<CatBase>().slowingEntities.Remove(gameObject);
            }
            StartCoroutine(StringFlingCooldown());
        }
        else //Starts the coroutine again if there were no cats to distract
        {
            StartCoroutine(StringFling());
        }
        
    }
    IEnumerator StringFlingCooldown()
    {
        yield return new WaitForSeconds(stringFlingCooldown);
        StartCoroutine(StringFling());
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
