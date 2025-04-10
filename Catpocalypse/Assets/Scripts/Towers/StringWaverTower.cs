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
    private bool _canStringFling = false;
    private bool _stringFlingUnlocked = false;
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
        _canStringFling = true;
        _stringFlingUnlocked = true;
        StringFling(targets);
    }
    private void StringFling(List<GameObject> catsInRange)
    {
        GameObject[] cats = catsInRange.ToArray();
        if (catsInRange.Count > 0) 
        {
            Debug.Log("String Fling called");
            _canStringFling = false;
            for (int i = 0; i < cats.Length;i++)
            {
                if(cats[i] != null)
                {
                    cats[i].GetComponent<CatBase>().DistractCat(stringFlingDistractValue, gameObject.GetComponent<Tower>());
                    cats[i].GetComponent<CatBase>().slowingEntities.Add(gameObject);
                }
                else
                {
                    Debug.LogError("Cat is null");
                }
            }
            StartCoroutine(UnslowCats(catsInRange));
            StartCoroutine(StringFlingCooldown());
        }
        else
        {
            StartCoroutine(RecallStringFling());
        }
        
    }
    IEnumerator UnslowCats(List<GameObject> catsInRange)
    {
        GameObject[] catList = targets.ToArray();
        
        yield return new WaitForSeconds(stringFlingSlowingDuration);
        for(int i = 0;i<catList.Length;i++)
        {
            if (catList[i] != null)
            {
                catList[i].GetComponent<CatBase>().slowingEntities.Remove(gameObject);
            }
            else
            {
                Debug.LogError("Unslow cat is null");
                //targets.Remove(cat);
            }

        }
    }
    IEnumerator StringFlingCooldown()
    {
        yield return new WaitForSeconds(stringFlingCooldown);
        _canStringFling = true;
        StringFling(targets);
    }
    IEnumerator RecallStringFling()
    {

        yield return new WaitForSeconds(.5f);
        StringFling(targets);
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
