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
        float angleH = Vector3.Angle(parentTower.transform.position,transform.position);
        float angle = Mathf.Atan2((transform.position.y - parentTower.transform.position.y), (transform.position.x - parentTower.transform.position.x));
        GameObject _piece = Instantiate(_string, gameObject.transform.position + new Vector3(0, 2, 0),Quaternion.identity);
        
        //angle = Mathf.Rad2Deg * angle;
        //Vector3 rotation = Vector3.Normalize(transform.position - _piece.transform.position);
        //float angleH = Vector3.Angle(_piece.transform.position,transform.position);
        
        //_piece.transform.LookAt(rotation,Vector3.up);
        //_piece.transform.rotation = Quaternion.Euler(0, angle, 90);
        //_piece.transform.rotation = Quaternion.RotateTowards(_piece.transform.eulerAngles, transform.eulerAngles, 180, 180);
       // _piece.transform.rotation = Quaternion.RotateTowards(_piece.transform.rotation, transform.rotation, 180);
        //_piece.transform.rotation = new Quaternion(90,_piece.transform.rotation.y,_piece.transform.rotation.z,_piece.transform.rotation.w);
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