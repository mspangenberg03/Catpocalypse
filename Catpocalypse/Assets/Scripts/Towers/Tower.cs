using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;


[RequireComponent(typeof(StateMachine))]
public class Tower : MonoBehaviour
{

    [SerializeField,Min(1)]
    protected float buildCost;
    
    [Tooltip("This is the percentage of the cost that is refunded when the player destroys the tower.")]
    [Range(0f, 1f)]
    [SerializeField]
    protected float refundPercentage = 0.85f;
    
    [SerializeField, Min(1)]
    protected SphereCollider range;
    [SerializeField, Min(1)]
    protected float distractValue;
    [SerializeField]
    protected int numberOfTargets;

    protected Vector3 targetDirection;
    public List<GameObject> targets;

    // This property is currently only used by the laser pointer tower, but I put it
    // here in Tower in case any other tower needs it later.
    protected Type _TargetCatType = typeof(NormalCat);

    protected SphereCollider _Collider;

    protected StateMachine _stateMachine;



    private void OnEnable()
    {
        // This corrects the problem with our prefabs. For example, the laser tower
        // has a scale of 500. It's collider has a radius of 6. This effectively means
        // the true size of the collider is radius = 30,000. This adjusts the collider
        // radius by simply dividing it by the gameObject's scale. It doesn't matter
        // whether we use x, y, or z here since it is a sphere.
        _Collider = GetComponent<SphereCollider>();
        _Collider.radius = _Collider.radius / transform.localScale.x;

        if (_stateMachine == null)
        {
            _stateMachine = GetComponent<StateMachine>();
            if (_stateMachine == null)
                throw new Exception($"The tower \"{gameObject.name}\" does not have a state machine component!");

            InitStateMachine();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Cat"))
        {
            OnNewTargetEnteredRange(collider.gameObject);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Cat"))
        {
            OnTargetWentOutOfRange(collider.gameObject);

            targets.Remove(collider.gameObject);
        }
    }

    /// <summary>
    /// This function sets up the state machine with a very basic setup that just uses the base class for each state.
    /// </summary>
    protected virtual void InitStateMachine()
    {
        // Create tower states.
        TowerState_Active_Base activeState = new TowerState_Active_Base(this);
        TowerState_Disabled_Base disabledState = new TowerState_Disabled_Base(this);
        TowerState_Idle_Base idleState = new TowerState_Idle_Base(this);
        TowerState_Upgrading_Base upgradingState = new TowerState_Upgrading_Base(this);

        // Create and register transitions.
        _stateMachine.AddTransitionFromState(idleState, new Transition(activeState, () => targets.Count > 0));

        _stateMachine.AddTransitionFromAnyState(new Transition(idleState, () => targets.Count < 1 && 
                                                                                _stateMachine.CurrentState.Name != "TowerState_Disabled_Base"));


        // Tell state machine to write in the debug console every time it exits or enters a state.
        //_stateMachine.EnableDebugLogging = true;

        // Set the starting state.
        _stateMachine.SetState(idleState);
    }

    /// <summary>
    /// This function exists so that it can be overriden in subclasses. 
    /// </summary>
    /// <remarks>
    /// The purpose of is function is to allow a given tower type to have its own filters on targets.
    /// Specifically I added this function since the Laser Pointer Tower needs to only care about cats
    /// of a certain type. So it overrides this function to insert it's own functionality.
    /// 
    /// NOTE: Here in the tower base class this function simply adds the target to the list,
    ///       as the base class doesn't need to do any filtering of targets that are in range.
    ///       This defines the default behavior for towers that do not override this function.
    /// </remarks>
    /// <param name="target">The target game object to verify and add to the list.</param>
    protected virtual void OnNewTargetEnteredRange(GameObject target)
    {
        targets.Add(target);
    }

    /// <summary>
    /// This function is just an event hander that subclasses can override to be notified when
    /// a target moves out of range. So it's like NewTargetEnteredRange(), but the opposite.
    /// </summary>
    /// <remarks>
    /// 
    /// NOTE: Subclasses DO NOT need to remove the target from targets.
    ///       This class does that right after it calls this event handler.
    ///       For example, laser tower has an ActiveTargets list, so it must remove
    ///       said target from that list in its override of this method.
    /// </remarks>
    /// <param name="target"></param>
    protected virtual void OnTargetWentOutOfRange(GameObject target)
    {
        Debug.Log("Out");
    }

    /// <summary>
    /// This function is an event handler that subclasses can override to be
    /// notified when a target has "died".
    /// </summary>
    /// <param name="target"></param>
    protected virtual void OnTargetHasDied(GameObject target)
    {

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

    private void OnCatDied(object sender, EventArgs e)
    {
        OnTargetHasDied((GameObject) sender);

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


    public Type TargetCatType
    {
        get { return _TargetCatType; }
        set 
        {
            if (!typeof(CatBase).IsAssignableFrom(value))
            {
                throw new Exception($"The passed in cat type is not a subclass of CatBase! The tower in question is \"{gameObject.name}\" of type {this.GetType()}");
            }

            _TargetCatType = value; 
        }
    }
}
