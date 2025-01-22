using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCucumber : MonoBehaviour
{
    public Tower parentTower;
    [SerializeField, Tooltip("How long the sub cucumber lasts")]
    private int _subCucumberLifespan = 2;
    [SerializeField, Tooltip("")]
    private int _subCucumberDistractionValue = 10;
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("SubCuc spawned");
        StartCoroutine(SubCucLife());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            other.GetComponent<CatBase>().DistractCat(_subCucumberDistractionValue, parentTower);
        }
    }
    //Destroys the sub cucumber at the end of it's life
    IEnumerator SubCucLife()
    {
        yield return new WaitForSeconds(_subCucumberLifespan);
        Destroy(gameObject);
    }
}
