using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScratchingPost : MonoBehaviour
{
    [Header("Scratching Post Settings")]

    [Tooltip("The amount a nearby cat's speed is slowed by")]
    [SerializeField]
    [Min(0f)]
    public float speedDebuff = 1.8f;

    [Tooltip("The duration the Scratching Post will last")]
    [SerializeField]
    [Min(0f)]
    private float _Duration;

    [Tooltip("The time between Duration ticks")]
    [SerializeField]
    [Min(0f)]
    private float _DurationTickTime;

    [Tooltip("The durability of the Scratching Post")]
    [SerializeField]
    [Min(0f)]
    private float _Durability;

    [Tooltip("The amount of Durability removed per cat per tick")]
    [SerializeField]
    [Min(0f)]
    private float _DurabilityRemovedByCat;

    private List<GameObject> _Cats = new List<GameObject>();

    private bool _Destroying = false;

    public GameObject parentTower;

    public void Start()
    {
        StartCoroutine(DurationCountDown(_Duration));
    }

    public void Update()
    {
        if(_Destroying)
        {
            return;
        }
        if (_Durability <= 0)
        {
            _Destroying = true;
            DestroyPost();
        }
        if (_Cats.Count > 0)
        {
            _Durability -= _Cats.Count * _DurabilityRemovedByCat * Time.deltaTime;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            _Cats.Add(other.gameObject);
            other.GetComponent<CatBase>().slowingEntities.Add(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Cat")
        {
            _Cats.Remove(other.gameObject);
            other.GetComponent<CatBase>().slowingEntities.Remove(gameObject);
        }
    }

    private void RemoveDurability()
    {
        _Durability -= _DurabilityRemovedByCat;
        if (_Durability <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void DestroyPost()
    {
        //TODO: OnDestroy, OnTriggerEnter, and OnTriggerExit needs to account for if there is another tower affecting the Cat speed and set
        //      it to that speed without setting IsSlowed to false. To do this, we might need to set IsSlowed to an int value (or smaller if we want)
        //      and have it act as a counter instead. From there, the Cat needs to find the greatest slowing effect and set its' speed to that.
        //      This accounts for multiple slow towers of varying effect to occur.

        foreach (GameObject obj in _Cats)
        {
            if (obj != null)
            {
                obj.GetComponent<CatBase>().slowingEntities.Remove(gameObject);
            }
        }
        parentTower.GetComponent<ScratchingPostTower>().postCount--;
        Destroy(gameObject);
    }

    private IEnumerator DurationCountDown(float currentTimeLeft)
    {
        if (currentTimeLeft == 0)
        {
            DestroyPost();
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForSeconds(_DurationTickTime);
            StartCoroutine(DurationCountDown(--currentTimeLeft));
        }
    }

    private IEnumerator DistractCats()
    {
        if (_Cats.Count > 0)
        {
            foreach (GameObject obj in _Cats)
            {
                if (obj != null)
                {
                    CatBase cat = obj.GetComponent<CatBase>();
                    cat.DistractCat(
                        gameObject.GetComponentInParent<ScratchingPostTower>().DistractValue,
                        gameObject.GetComponentInParent<ScratchingPostTower>()
                        );
                    RemoveDurability();
                }
                else
                {
                    _Cats.Remove(obj);
                }
            }
            yield return new WaitForSeconds(_DurationTickTime);
            StartCoroutine(DistractCats());
        }
    }
}