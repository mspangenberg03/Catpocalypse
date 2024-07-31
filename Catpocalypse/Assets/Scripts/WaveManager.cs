using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class tracks how many cats are left across all spawners.
/// </summary>
public class WaveManager : MonoBehaviour
{
    public event EventHandler WaveEnded;
    public event EventHandler LevelCleared;



    public static WaveManager Instance;

    private int _TotalWavesInLevel;

    private List<CatSpawner> _CatSpawners;
    private int _TotalCatsInWave;
    private int _CatsRemainingInWave;
    
    private int _CatsDistracted;
    private int _CatsReachedGoal;

    private int _TotalCatsDistracted;
    private int _TotalCatsReachedGoal;

    private float _SecondsSinceLevelStart;
    private float _SecondsSinceWaveStart;

    private int _WaveNumber = 0;
    private bool _WaveInProgress = false;

    private PlayerMoneyManager _PlayerMoneyManager;
    private PlayerCutenessManager _cutenessManager;

    [SerializeField]
    private PlayerUpgradeData _playerUpgradeData;
    private bool _scrapRewarded = false;

    [SerializeField] AudioSource _winSound;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already a WaveManager in this scene. Self destructing!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _PlayerMoneyManager = FindObjectOfType<PlayerMoneyManager>();
        _cutenessManager = GameObject.FindGameObjectWithTag("Goal").GetComponent<PlayerCutenessManager>();

        CatBase.OnCatDied += OnCatDied;
        CatBase.OnCatReachGoal += OnCatReachGoal;
        _CatSpawners = new List<CatSpawner>();
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("CatSpawnPoint");

        foreach(GameObject spawner in spawners)
        {
            CatSpawner catSpawner = spawner.GetComponent<CatSpawner>();
            _CatSpawners.Add(spawner.GetComponent<CatSpawner>());
            if(spawner.GetComponent<CatSpawner>().NumberOfWaves > _TotalWavesInLevel)
            {
                _TotalWavesInLevel = spawner.GetComponent<CatSpawner>().NumberOfWaves;
            }
        }

        HUD.HideWaveDisplay();
    }

    private void Update()
    {
        _SecondsSinceLevelStart += Time.deltaTime;

        if (_WaveInProgress)
            _SecondsSinceWaveStart += Time.deltaTime;


        if (!_WaveInProgress && WaveNumber == _TotalWavesInLevel)
        {
            LevelCleared?.Invoke(this, EventArgs.Empty);
            if (!_scrapRewarded)
            {
                _playerUpgradeData.Scrap += _playerUpgradeData.ScrapReward;
                _scrapRewarded = true;
            }
            HUD.RevealVictory();
        }
    }

    public void StartNextWave()
    {
        // Don't try to start a wave if one is already in progress.
        if (IsWaveInProgress)
            return;

        if (_cutenessManager.CurrentCutenessChallenge != PlayerCutenessManager.CutenessChallenges.None)
        {
            _cutenessManager.CutenessChallenge();
        }
        _WaveInProgress = true;

        _WaveNumber++;


        FindAllSpawners();

        foreach (CatSpawner spawner in _CatSpawners)
        {
            spawner.StartNextWave();
        }

        CalculateTotalCatsInWave();

        _CatsRemainingInWave = _TotalCatsInWave;
        _CatsDistracted = 0;
        _CatsReachedGoal = 0;

        HUD.ShowWaveDisplay();
        HUD.UpdateWaveInfoDisplay(_WaveNumber, _CatsRemainingInWave);
    }

    public void StopAllSpawning()
    {
        foreach (CatSpawner spawner in _CatSpawners)
        {
            spawner.StopSpawner();
        }
    }

    public void OnCatDied(object Sender, EventArgs e)
    {
        _CatsRemainingInWave--;
        _CatsDistracted++;
        _TotalCatsDistracted++;

        HUD.UpdateWaveInfoDisplay(_WaveNumber, _CatsRemainingInWave);

        if (_CatsRemainingInWave < 1)
        {
            HUD.HideWaveDisplay();
            _WaveInProgress = false;

            WaveEnded?.Invoke(this, EventArgs.Empty);

            if (_WaveNumber >= _TotalWavesInLevel && !FindObjectOfType<PlayerHealthManager>().IsPlayerDead)
                HUD.RevealVictory();
            //_winSound.
        }
    }
    public void OnCatReachGoal(object Sender, EventArgs e)
    {
        _CatsRemainingInWave--;
        _CatsReachedGoal++;
        _TotalCatsReachedGoal++;

        HUD.UpdateWaveInfoDisplay(_WaveNumber, _CatsRemainingInWave);

        if (_CatsRemainingInWave < 1)
        {
            HUD.HideWaveDisplay();
            _WaveInProgress = false;

            WaveEnded?.Invoke(this, EventArgs.Empty);

            if (_WaveNumber >= _TotalWavesInLevel && !FindObjectOfType<PlayerHealthManager>().IsPlayerDead)
                HUD.RevealVictory();
        }
    }

    private void CalculateTotalCatsInWave()
    {
        _TotalCatsInWave = 0;
        foreach (CatSpawner spawner in _CatSpawners)
        {
            //Debug.Log($"Spawner: {spawner.CatsInCurrentWave}");
            _TotalCatsInWave += spawner.CatsInCurrentWave();
        }

        //Debug.Log($"Total: {_TotalCatsInWave}");
    }

    private void FindAllSpawners()
    {
        _CatSpawners = FindObjectsByType<CatSpawner>(FindObjectsSortMode.None).ToList();
    }


    private void OnWaveEnded(object sender, EventArgs e)
    {
        
    }



    public int TotalWavesInLevel { get { return _TotalWavesInLevel; } }

    public int WaveNumber { get { return _WaveNumber; } }
    public bool IsWaveInProgress { get { return _WaveInProgress; } }

    public int NumCatsDistractedInWave { get { return _CatsDistracted; } }
    public int NumCatsReachedGoalInWave { get { return _CatsReachedGoal; } }
    public int TotalCatsInWave { get { return _TotalCatsInWave; } }
   
    public int TotalCatsDistractedInLevel { get { return _TotalCatsDistracted; } }
    public int TotalCatsReachedGoalInLevel { get { return _TotalCatsReachedGoal; } }

    public float SecondsElapsedSinceLevelStarted { get { return _SecondsSinceLevelStart; } }
    public float SecondsElapsedSinceWaveStarted { get { return _SecondsSinceWaveStart; } }

}

