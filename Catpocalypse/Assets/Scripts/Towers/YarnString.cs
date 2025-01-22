using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnString : MonoBehaviour
{
    public YarnBall parent;
    [SerializeField, Tooltip("How long the string piece lasts")]
    private int _stringDuration = 2;

    private float _distractValue = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StringLifespan());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cat"))
        {
            //Debug.Log(transform.parent.gameObject);
            other.gameObject.GetComponent<CatBase>().DistractCat(_distractValue,parent.parentTower);
            Debug.LogWarning("Cat distracted by yarn string");
        }
    }
    IEnumerator StringLifespan()
    {
        yield return new WaitForSeconds(_stringDuration);
        Destroy(gameObject);
    }
}
