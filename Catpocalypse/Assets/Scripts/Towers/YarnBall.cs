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
        if (parentTower.gameObject.GetComponent<YarnBallTower>().upgraded)
        {
            StartCoroutine(Upgrade());
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
    IEnumerator Upgrade()
    {
        
        GameObject _piece = Instantiate(_string, gameObject.transform.position + new Vector3(0, 2, 0),Quaternion.identity);
        float angle = Mathf.Atan2((transform.position.y - _piece.transform.position.y), (transform.position.x - _piece.transform.position.x));
        angle = Mathf.Rad2Deg * angle;

        //_piece.transform.LookAt(transform,Vector3.right);
        _piece.transform.rotation = Quaternion.Euler(0, 0, angle);
        //Vector3.RotateTowards(_piece.transform.eulerAngles, transform.eulerAngles, 180, 180);
        //Quaternion.RotateTowards(_piece.transform.rotation, transform.rotation, 180);
        _piece.GetComponent<YarnString>().parent = this;
        yield return new WaitForSeconds(_spawnInterval);
        StartCoroutine(Upgrade());
    }
    private void OnDestroy()
    {
        StopCoroutine(Upgrade());
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(_lifespan);
        Destroy(gameObject);
    }
}