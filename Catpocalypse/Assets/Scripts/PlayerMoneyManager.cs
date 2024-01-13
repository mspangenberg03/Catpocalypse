using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMoneyManager : MonoBehaviour
{
    [SerializeField] private float _Money = 200f;

    [Header("Money Earning Amounts")]
    [SerializeField] private float _NormalCatPayAmount;
    [SerializeField] private float _LightCatPayAmount;
    [SerializeField] private float _HeavyCatPayAmount;


    private void Start()
    {
        HUD.UpdatePlayerMoneyDisplay(_Money);


        // All cat types should be able to subsribe this same handler to their OnCatDied events.
        // That function below calls another that checks the cat type.
        NormalCat.OnCatDied += OnCatDied;

    }

    /// <summary>
    /// This function is used to spend money.
    /// </summary>
    /// <remarks>
    /// If the player has enough money, the passed in amount is deducted and this function returns true.
    /// Otherwise, this function will simply return false to indicate there isn't enough money.
    /// 
    /// It also calls the HUD function to update the money display.
    /// </remarks>
    /// <param name="amount">The amount to deduct from the player's money.</param>
    /// <returns>False if funds are not sufficient, or true otherwise.</returns>
    public bool SpendMoney(float amount)
    {
        if (amount > _Money)
        {
            return false;
        }

        _Money -= amount;
        HUD.UpdatePlayerMoneyDisplay(_Money);

        return true;
    }

    private void OnCatDied(object sender, EventArgs e)
    {
        _Money += GetPayAmount(sender);
        HUD.UpdatePlayerMoneyDisplay(_Money);
    }

    private float GetPayAmount(object deadCat)
    {
        float pay = 0f;

        if (deadCat is NormalCat)
            pay = _NormalCatPayAmount;
        else if (deadCat is LightCat)
            pay = _LightCatPayAmount;
        else if (deadCat is HeavyCat)
            pay = _HeavyCatPayAmount;


        return pay;

    }
}
