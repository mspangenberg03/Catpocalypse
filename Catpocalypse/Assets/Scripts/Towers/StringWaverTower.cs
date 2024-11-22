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
        //Slows down and distracts each cat in range
        foreach(GameObject cat in targets)
        {
            cat.GetComponent<CatBase>().slowingEntities.Add(gameObject);
            cat.GetComponent<CatBase>().DistractCat(_stringFlingDistractionValue, this);
            yield return new WaitForSeconds(_stringFlingDuration);
            cat.GetComponent<CatBase>().slowingEntities.Remove(gameObject);
        }
        yield return new WaitForSeconds(_stringFlingCooldown);
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
