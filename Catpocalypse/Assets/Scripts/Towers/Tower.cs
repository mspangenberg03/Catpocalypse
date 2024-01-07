using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private int buildCost;
    [SerializeField]
    private int refundAmount;
    [SerializeField]
    private SphereCollider range;
    
    protected Vector2 targetDirection;
    [SerializeField]
    protected List<NormalCat> targets;
    [SerializeField]
    protected NormalCat currentTarget;
    protected TowerBase baseOfTower;

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null && targets.Count > 0)
        {
            currentTarget = targets.First();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {

            NormalCat cat = (NormalCat) PrefabUtility.GetCorrespondingObjectFromSource(collider.gameObject);
            targets.Add(cat);
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {
            NormalCat cat = (NormalCat)PrefabUtility.GetPrefabInstanceHandle(collider.gameObject);
            targets.Remove(cat);
            if(currentTarget.Equals(collider))
            {
                currentTarget = targets.First();
            }
        }
    }

    public void OnDestroy()
    {
        this.gameObject.SetActive(false);
        Destroy(this);
    }


}
