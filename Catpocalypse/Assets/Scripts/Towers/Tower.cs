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
    protected List<GameObject> targets;
    [SerializeField]
    protected TowerBase baseOfTower;

    public void Start()
    {
        baseOfTower = this.gameObject.GetComponentInParent<TowerBase>();
    }

    void OnMouseEnter()
    {
       baseOfTower.hoveredOver = true;
       baseOfTower.gameObject.GetComponent<Renderer>().material= baseOfTower.towerHovered;
    }

    void OnMouseExit()
    {
        baseOfTower.hoveredOver = false;
        baseOfTower.gameObject.GetComponent<Renderer>().material = baseOfTower.towerNotHovered;
    }

    void OnMouseUpAsButton()
    {

        if(enabled)
        {
            if(baseOfTower.hoveredOver){
                baseOfTower.towerDestroyerUI.gameObject.SetActive(true);
                baseOfTower.towerDestroyerUI.SetCurrentSelectedBase(baseOfTower);
            }
            
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
            if(targets.Count > 0) {
                targets[0] = targets.First();
            }
            
           
        }
    }

    public void OnDestroy()
    {
        Destroy(this);
    }


}
