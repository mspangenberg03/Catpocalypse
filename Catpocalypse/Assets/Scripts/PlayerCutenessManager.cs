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


    [Header("Cuteness Challenges Parameters")]

    [Tooltip("This sets how much less effective (as a percentage) distraction is on the cats.")]
    [Range(0f, 1f)]
    [SerializeField] private float _CatsDistractionThresholdDebuffPercent = 0.5f;


    private CutenessChallenges _CurrentCutnessChallenge = CutenessChallenges.None;

    private float _Cuteness = 0f;



    /// <summary>
    /// This enum defines the challenges caused by maxing out the cuteness meter.
    /// </summary>
    public enum CutenessChallenges
    {
        None, // This is used when there is no active cuteness challenge
        CatsGetHarderToDistract,
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
        // Reset this in case one was active in the previous wave.
        _CurrentCutnessChallenge = CutenessChallenges.None;

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

        _CurrentCutnessChallenge = (CutenessChallenges) challenges.GetValue(index);
    }



    public float Cuteness { get { return _Cuteness; } }
    public CutenessChallenges CurrentCutenessChallenge { get { return _CurrentCutnessChallenge; } }

    public float CuteChallenge_CatsGetHarderToDistract_DebuffPercent { get { return _CatsDistractionThresholdDebuffPercent; } }
}
