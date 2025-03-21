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
    private float _spawnInterval = 2f;

    [SerializeField]
    private float _lifespan = 5;

    [SerializeField]
    private int _stringDamageDelay;

    private ParticleSystem _particles;
    [SerializeField]
    private float _stringDistraction = 1f;
    private void Start()
    {
        _particles = GetComponent<ParticleSystem>();
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
        _particles.Play();  
        yield return new WaitForSeconds(_spawnInterval);
        //StartCoroutine(Upgrade());
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
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Cat"))
        {
            if (!other.GetComponent<CatBase>()._affectedByParticles)
            {
                other.GetComponent<CatBase>().DistractCat(_stringDistraction, parentTower);
                other.GetComponent<CatBase>()._affectedByParticles = true;
                StartCoroutine(ParticleDelay(other));
            }
            
        }
    }
    IEnumerator ParticleDelay(GameObject cat)
    {
        yield return new WaitForSeconds(_stringDamageDelay);
        if (cat != null)
        {
            cat.GetComponent<CatBase>()._affectedByParticles = false;
        }
        
    }
}