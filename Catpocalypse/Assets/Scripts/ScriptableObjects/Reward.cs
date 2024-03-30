using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;



[CreateAssetMenu(fileName = "NewReward", menuName = "New Reward Asset")]
public class Reward : ScriptableObject
{
    public enum Types
    {
        Money,
    }



    [Tooltip("The type of reward this is.")]
    [SerializeField]
    private Types _Type;
    [Tooltip("The amount of this reward that is given to the player when the conditions are met. This field may be ignored for certain reward types.")]
    [SerializeField]
    private int _Amount;
    [TextArea, Tooltip("This is an optional field for description or notes about this reward.")]
    [SerializeField]
    private string _Description;
    
    [Tooltip("This list defines the condition(s) that must be met for this reward to be given to the player.")]
    [SerializeField]
    private List<RewardCondition> _Conditions;


    private RewardsManager _RewardsManager;



    /// <summary>
    /// Evaluates the conditions required for this reward.
    /// </summary>
    /// <param name="manager">The parent RewardsManager that called this function.</param>
    /// <returns>True if the conditions for this reward have been met, or false otherwise.</returns>
    public bool EvaluateConditions(RewardsManager manager)
    {
        if (manager != null)
            _RewardsManager = manager;
        else
            throw new ArgumentNullException(nameof(manager));


        float value1 = 0f;
        float value2 = 0f;
        foreach (RewardCondition condition in _Conditions)
        {
            // Get value 1.
            if (condition.Value1 != RewardsManager.ValueTypes.CustomValue)
                value1 = _RewardsManager.GetValue(condition.Value1);
            else
                value1 = condition.Value1CustomValue;


            // Get value 2.
            if (condition.Value2 != RewardsManager.ValueTypes.CustomValue)
                value2 = _RewardsManager.GetValue(condition.Value2);
            else
                value2 = condition.Value2CustomValue;


            // Test if the values meet the condition.
            if (!condition.Evaluate(value1, value2))
            {
                return false;
            }

        } // end foreach


        return true;
    }



    public Types Type { get { return _Type; } }
    public float Amount { get { return _Amount; } }

}