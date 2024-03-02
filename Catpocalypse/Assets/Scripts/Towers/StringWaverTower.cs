using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringWaverTower : Tower
{
    [SerializeField, Tooltip("The number of seconds between the tower distracting cats")]
    private int distractionInterval = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Cat")
        {
            targets.Add(other.gameObject);
            StartCoroutine(DistractCat(other.gameObject));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            targets.Remove(other.gameObject);
            StopCoroutine(DistractCat(other.gameObject));
        }
    }
    public override void Upgrade()
    {
        base.Upgrade();
    }
    IEnumerator DistractCat(GameObject cat)
    {
        
        if(targets.Contains(cat) && cat != null)
        {
            cat.GetComponent<CatBase>().DistractCat(distractValue, this.gameObject.GetComponent<Tower>());
            yield return new WaitForSeconds(distractionInterval);
            StartCoroutine(DistractCat(cat));
        }
        
    }
}
