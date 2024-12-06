using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWaverTower : Tower
{
    #region String Fling Variables
    [SerializeField]
    private int _stringFlingDuration = 1;
    [SerializeField]
    private int _stringFlingCooldown = 10;
    [SerializeField]
    private int _stringFlingDistractionValue = 10;
    public float _speedDebuff = 1.8f;
    #endregion
    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
        StartCoroutine(DistractCat());
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
        if(targets.Count > 0)
        {
            //Slows down and distracts each cat in range
            Debug.LogWarning("StringFling");
            foreach (GameObject cat in targets)
            {
                if(cat != null)
                {
                    cat.GetComponent<CatBase>().slowingEntities.Add(gameObject);
                    cat.GetComponent<CatBase>().DistractCat(_stringFlingDistractionValue, this);
                    StartCoroutine(StringFlingDuration(cat));
                }
            }
            yield return new WaitForSeconds(_stringFlingCooldown);
        }

        yield return new WaitForSeconds(0);
        StartCoroutine(StringFling());
    }
    IEnumerator StringFlingDuration(GameObject cat)
    {
        yield return new WaitForSeconds(_stringFlingDuration);
        if(cat != null)
        {
            cat.GetComponent<CatBase>().slowingEntities.Remove(gameObject);
        }
       
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
