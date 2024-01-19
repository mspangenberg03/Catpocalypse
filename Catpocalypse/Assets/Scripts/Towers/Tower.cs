using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class Tower : MonoBehaviour
{

    [SerializeField]
    protected float buildCost;
    
    [Tooltip("This is the percentage of the cost that is refunded when the player destroys the tower.")]
    [Range(0f, 1f)]
    [SerializeField]
    protected float refundPercentage = 0.85f;
    
    [SerializeField]
    protected SphereCollider range;
    [SerializeField]
    protected float distractValue;
    [SerializeField]
    protected int numberOfTargets;

    protected Vector3 targetDirection;
    public List<GameObject> targets;



    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Cat"))
        {
            targets.Add(collider.gameObject);
        }
    }

    void OnMouseEnter()
    {
        gameObject.GetComponentInParent<TowerBase>().hoveredOver = true;
        gameObject.GetComponent<Renderer>().material = gameObject.GetComponentInParent<TowerBase>().towerHovered;
    }

    void OnMouseExit()
    {
        gameObject.GetComponentInParent<TowerBase>().hoveredOver = false;
        gameObject.GetComponent<Renderer>().material = gameObject.GetComponentInParent<TowerBase>().towerNotHovered;
    }

    void OnMouseUpAsButton()
    {

        if (enabled)
        {
            

        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Cat"))
        {
            targets.Remove(collider.gameObject);
            if(targets.Count > 0) 
            {
                targets[0] = targets.First();
            }
            
           
        }
    }

    private void OnCatDied(object sender, EventArgs e)
    {
        targets.Remove(sender as GameObject);
    }

    public void OnDestroy()
    {
        Destroy(this);
    }
    public float GetDistractionValue()
    {
        return distractValue;
    }

    public float GetBuildCost()
    {
        return buildCost;
    }

    public float GetRefundPercentage()
    {
        return refundPercentage;
    }



    public float BuildCost { get { return buildCost; } }
    public float DistractValue { get { return distractValue; } }

}
