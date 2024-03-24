using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

using Random = UnityEngine.Random;


public class PlayerCutenessManager : MonoBehaviour
{
    public static PlayerCutenessManager Instance;


    [Header("Cuteness Bar Settings")]

    [SerializeField] private float _MaxCuteness = 100f;

    [SerializeField]
    private TextMeshProUGUI cutenessMeterMaxedText;

    [SerializeField]
    private TextMeshProUGUI cutenessChallengeText;

    [Header("Cuteness Challenges Parameters")]

    [Tooltip("This sets how much less effective (as a percentage) distraction is on the cats.")]
    [Range(0f, 1f)]
    [SerializeField] private float _CatsDistractionThresholdDebuffPercent = 0.5f;

    [SerializeField, 
    Tooltip("The percent debuff to the fire rate of the tower"),
    Range(0f, 1f)]
    private float _TowerFireRateDebuffPercent;

    private CutenessChallenges _CurrentCutenessChallenge = CutenessChallenges.None;

    private float _Cuteness = 0f;

    private TowerTypes _TowerType;

    //public bool _cutenessMeterFull = false;



    /// <summary>
    /// This enum defines the challenges caused by maxing out the cuteness meter.
    /// </summary>
    public enum CutenessChallenges
    {
        None, // This is used when there is no active cuteness challenge
        CatsGetHarderToDistract, //Applies a debuff to tower damage
        BuffCatType, //Buffs a type of cat
        DebuffTowerType, //Debuffs a type of tower
        NonAllergicStrike, //Disables the Non-Allergic Towers
        CucumberTowerBuffsCats, //The Cucumber Tower buffs cats instead of distracting them
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already a PlayerCutenessManager in this scene. Self destructing!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        cutenessMeterMaxedText.gameObject.SetActive(false);
    }

    private void Start()
    {
        WaveManager.Instance.WaveEnded += OnWaveEnded;

        HUD.UpdateCutenessDisplay(_Cuteness, _MaxCuteness);
    }
    private void Update()
    {

    }

    public void AddCuteness(int amount)
    {
        _Cuteness = Mathf.Clamp(_Cuteness + amount, 0, _MaxCuteness);
        HUD.UpdateCutenessDisplay(_Cuteness, _MaxCuteness);
    }

    private void OnWaveEnded(object sender, EventArgs e)
    {
        EndCutenessChallenge();
        // Reset this in case one was active in the previous wave.
        _CurrentCutenessChallenge = CutenessChallenges.None;

        // Did the bar get maxed during the previous wave?
        if (_Cuteness >= _MaxCuteness)
        {            
            _Cuteness = 0f;
            HUD.UpdateCutenessDisplay(_Cuteness, _MaxCuteness);

            cutenessMeterMaxedText.gameObject.SetActive(true);

            // Randomly select a cuteness challenge for the next wave.
            SelectCutenessChallenge();
        }
    }
        
    private void SelectCutenessChallenge()
    {
        var challenges = Enum.GetValues(typeof(CutenessChallenges));

        // We're starting the random range at 1, so we don't randomly select 0 (the "None" option).
        int index = Random.Range(1, challenges.Length - 1);

        _CurrentCutenessChallenge = (CutenessChallenges) challenges.GetValue(index);
       
    }



    public float Cuteness { get { return _Cuteness; } }
    public CutenessChallenges CurrentCutenessChallenge { get { return _CurrentCutenessChallenge; } }

    public float CuteChallenge_CatsGetHarderToDistract_DebuffPercent { get { return _CatsDistractionThresholdDebuffPercent; } }
    //Executes the cuteness challenge
    public void CutenessChallenge()
    {
        if (CurrentCutenessChallenge == CutenessChallenges.CatsGetHarderToDistract)
        {
            foreach(Tower tower in FindObjectsOfType<Tower>())
            {
                tower.DistractValue *= CuteChallenge_CatsGetHarderToDistract_DebuffPercent;
            }
            cutenessChallengeText.text = "Towers do less damage";
        }
        if (CurrentCutenessChallenge == CutenessChallenges.DebuffTowerType)
        {
            var towerTypes = Enum.GetValues(typeof(TowerTypes));
            int index = Random.Range(0,towerTypes.Length-1);
            _TowerType = (TowerTypes) towerTypes.GetValue(index);
            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                //If the tower is the type that is getting debuffed
                if(tower.TowerTypeTag == _TowerType)
                {
                    tower.FireRate *= _TowerFireRateDebuffPercent;
                }
                cutenessChallengeText.text = _TowerType+" towers fire slower";
            }
        }
        if(CurrentCutenessChallenge == CutenessChallenges.NonAllergicStrike)
        {
            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                if(tower.TowerTypeTag == TowerTypes.NonAllergic)
                {
                    tower.gameObject.GetComponent<NonAllergicTower>().DisableTower();
                }
            }
            cutenessChallengeText.text = "Non-Allergic towers are disabled";
        }
    }
    //Ends the cuteness challenge when the wave ends
    private void EndCutenessChallenge()
    {
        if (CurrentCutenessChallenge == CutenessChallenges.CatsGetHarderToDistract)
        {
            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                tower.DistractValue /= CuteChallenge_CatsGetHarderToDistract_DebuffPercent;
            }
        }
        if (CurrentCutenessChallenge == CutenessChallenges.DebuffTowerType)
        {
            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                //If the tower is the type that got debuffed
                if (tower.TowerTypeTag == _TowerType)
                {
                    tower.FireRate /= _TowerFireRateDebuffPercent;
                }
            }
        }
        if(CurrentCutenessChallenge == CutenessChallenges.NonAllergicStrike)
        {
            foreach (Tower tower in FindObjectsOfType<Tower>())
            {
                if (tower.TowerTypeTag == TowerTypes.NonAllergic)
                {
                    tower.gameObject.GetComponent<NonAllergicTower>().Enabled = true;
                }
            }
        }

        cutenessChallengeText.text = "";
    }
}
