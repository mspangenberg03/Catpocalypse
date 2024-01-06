using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    public int buildCost;
    [SerializeField]
    public int refundAmount;
    [SerializeField]
    public SphereCollider range;
    
    protected Vector2 targetDirection;
    protected List<NormalCat> targets;
    protected NormalCat currentTarget;
    protected TowerBase baseOfTower;

    public void Start()
    {
        //Instantiate();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null && targets.Count > 0)
        {
            currentTarget = targets.First();
            targets.Remove(currentTarget);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collider = collision.collider.gameObject;
        if (collider.tag.Equals("Cat"))
        {
            NormalCat cat = collider.GetComponent<NormalCat>();
            targets.Add(cat);
        }
    }


    private void OnCollisionExit(Collision collision)
    {

        GameObject collider = collision.collider.gameObject;
        if (collider.tag.Equals("Cat"))
        {
            NormalCat cat = collider.GetComponent<NormalCat>();
            targets.Remove(cat);
            if(currentTarget == cat)
            {
                currentTarget = null;
            }
        }
    }

    public void OnDestroy()
    {
        this.gameObject.SetActive(false);
        Destroy(this);
    }


}
