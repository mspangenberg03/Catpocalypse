using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{

    [SerializeField]
    protected int buildCost;
    [SerializeField]
    protected int refundAmount;
    [SerializeField]
    protected SphereCollider range;
    [SerializeField]
    protected int distractValue;
    [SerializeField]
    protected int numberOfTargets;

    protected Vector2 targetDirection;
    [SerializeField]
    protected List<GameObject> targetableCats;
    [SerializeField]
    protected List<GameObject> targets;
    protected TowerBase baseOfTower;

    // Update is called once per frame
    void Update()
    {
        if (targets.Count < numberOfTargets && targetableCats.Count > 0)
        {
            targets.Add(targetableCats.First());
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {
            targets.Add(collider.gameObject);
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {
            targets.Remove(collider.gameObject);
            targets[0] = targets.First();
           
        }
    }

    public void OnDestroy()
    {
        this.gameObject.SetActive(false);
        Destroy(this);
    }


}
