using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingNode : MonoBehaviour
{
    [Header("Player Targeting Node Settings")]

    [Tooltip("The Node Prefab")]
    [SerializeField]
    private GameObject _Target;

    //TODO: Make the target draggable. Currently draggable anywhere on the 2D plane, not vertically
}