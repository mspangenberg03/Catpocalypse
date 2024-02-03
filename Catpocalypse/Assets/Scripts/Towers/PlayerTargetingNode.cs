using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerTargetingNode : MonoBehaviour
{
    [Header("Player Targeting Node Settings")]

    [Tooltip("The Node Prefab")]
    [SerializeField]
    private GameObject _Target;

    private Vector3 _LastPosition;

    public void PointerDownEvent()
    {
        if()
    }

    public void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.GetComponent<TilePathingArrowControl>() != null)
        {

        }
    }
    
    //TODO: Make the target draggable. Currently draggable anywhere on the 2D plane, not vertically
}