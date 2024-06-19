using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWaverTower : Tower
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DistractCat());
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
        List<GameObject>.Enumerator cats = targets.GetEnumerator();
        if(cats.Current != null)
        {
            GameObject cat = cats.Current;
            do
            {
                if (targets.Contains(cat) && cat != null)
                {
                    cat.GetComponent<CatBase>().DistractCat(distractValue, this.gameObject.GetComponent<Tower>());

                }
            } while (cats.MoveNext());
        }
        yield return new WaitForSeconds(FireRate);
        StartCoroutine(DistractCat());
    }
}
