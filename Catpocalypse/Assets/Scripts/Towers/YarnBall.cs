using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnBall : MonoBehaviour
{
    public Tower parentTower;
    [SerializeField]
    private AudioSource _landingSound;

    [SerializeField]
    private PlayerUpgradeData _upgradeData;

    [SerializeField]
    private GameObject _string;

    [SerializeField]
    private float _spawnInterval = 2f;

    [SerializeField]
    private float _lifespan = 5;

    private void Start()
    {
        StartCoroutine(Life());
        if (_upgradeData.YarnThrowerTierFiveReached)
        {
            StartCoroutine(TierFive());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 3)
        {
            Distract(other.gameObject.GetComponent<CatBase>());
        }
    }
   

    private void Distract(CatBase cat)
    {
        if (cat != null)
        {
            cat.GetComponent<CatBase>().DistractCat(parentTower.GetDistractionValue(), parentTower);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        _landingSound.Play();
        if (collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator TierFive()
    {
        GameObject _piece = Instantiate(_string, gameObject.transform.position + new Vector3(0,2,0),Quaternion.identity);
        _piece.GetComponent<YarnString>().parent = this;
        Debug.LogWarning("String spawned");
        yield return new WaitForSeconds(_spawnInterval);
        StartCoroutine(TierFive());
    }
    private void OnDestroy()
    {
        StopCoroutine(TierFive());
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifespan);
        Destroy(gameObject);
    }
}