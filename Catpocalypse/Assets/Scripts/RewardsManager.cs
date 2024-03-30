using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class RewardsManager : MonoBehaviour
{
    public enum ValueTypes
    {
        CustomValue,

        CatsDistractedInWave,
        TotalCatsInWave,

        TotalCatsDistractedInLevel,
        TotalCatsReachedGoalInLevel,

        WavesCompleted,
        TotalWavesInLevel,
    }



    [Header("Wave End Rewards")]

    [SerializeField]
    private List <Reward> _waveEndRewards;


    PlayerMoneyManager _playerMoneyManager;
    WaveManager _waveManager;



    private void Awake()
    {
        _playerMoneyManager = FindObjectOfType<PlayerMoneyManager>();
        _waveManager = FindObjectOfType<WaveManager>();


        _waveManager.WaveEnded += OnWaveEnded;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckWaveEndRewards()
    {
        for (int i = 0; i < _waveEndRewards.Count; i++) 
        { 
            Reward reward = _waveEndRewards[i];

            if (reward.EvaluateConditions(this))
            {
                GiveReward(reward.Type, reward.Amount);
            }
        }
    }

    public float GetValue(ValueTypes valueType)
    {
        // NOTE: We don't check for ValueTypes.CustomValue here, because this function is
        //       never called in that case.
        switch (valueType)
        {
            case ValueTypes.CatsDistractedInWave:
                return _waveManager.NumCatsDistractedInWave;
            case ValueTypes.TotalCatsInWave:
                return _waveManager.TotalCatsInWave;

            case ValueTypes.TotalCatsDistractedInLevel:
                return _waveManager.TotalCatsDistractedInLevel;
            case ValueTypes.TotalCatsReachedGoalInLevel:
                return _waveManager.TotalCatsReachedGoalInLevel;
            
            case ValueTypes.WavesCompleted:
                return _waveManager.WaveNumber;
            case ValueTypes.TotalWavesInLevel:
                return _waveManager.TotalWavesInLevel;


            default:
                throw new ArgumentException("The passed in value type has not been implemented into this switch statement yet!");

        } // end switch

    }

    public void GiveReward(Reward.Types rewardType, float amount)
    {
        switch (rewardType)
        {
            case Reward.Types.Money:
                _playerMoneyManager.AddMoney(amount);
                Debug.Log($"GAVE REWARD: {amount} money");
                break;


            default:
                throw new ArgumentException("The passed in reward type has not been implemented into this switch statement yet!");

        } // end switch
    }

    private void OnWaveEnded(object sender, EventArgs e)
    {
        CheckWaveEndRewards();
    }

}
