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



    [SerializeField] private int _TotalWavesInLevel = 5;

    
    private List<CatSpawner> _CatSpawners;
    private int _TotalCatsInWave;
    private int _CatsRemainingInWave;

    private int _WaveNumber = 0;
    private bool _WaveInProgress = false;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is already a WaveManager in this scene. Self destructing!");
            Destroy(gameObject);
            return;
        }

        Instance = this;

        CatBase.OnCatDied += OnCatDied;

        HUD.HideWaveDisplay();
    }

    private void Update()
    {
        if (!_WaveInProgress && WaveCount == _TotalWavesInLevel)
        {
            LevelCleared?.Invoke(this, EventArgs.Empty);

            HUD.RevealVictory();
        }
    }

    public void StartNextWave()
    {
        // Don't try to start a wave if one is already in progress.
        if (IsWaveInProgress)
            return;


        _WaveInProgress = true;

        _WaveNumber++;


        FindAllSpawners();

        foreach (CatSpawner spawner in _CatSpawners)
        {
            spawner.StartNextWave();
        }

        CalculateTotalCatsInWave();

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

        HUD.UpdateWaveInfoDisplay(_WaveNumber, _CatsRemainingInWave);

        if (_CatsRemainingInWave < 1)
        {
            HUD.HideWaveDisplay();
            _WaveInProgress = false;

            WaveEnded?.Invoke(this, EventArgs.Empty);

            CheckIfPlayerBeatLevel();
        }
    }

    private bool CheckIfPlayerBeatLevel()
    {
        if (_WaveNumber >= _TotalWavesInLevel && !FindObjectOfType<PlayerHealthManager>().IsPlayerDead)
        {
            FindObjectOfType<VictoryScreen>()?.Show();

            LevelCleared?.Invoke(this, EventArgs.Empty);

            return true;
        }


        return false;
    }

    private void CalculateTotalCatsInWave()
    {
        _TotalCatsInWave = 0;
        foreach (CatSpawner spawner in _CatSpawners)
        {
            //Debug.Log($"Spawner: {spawner.CatsInCurrentWave}");
            _TotalCatsInWave += spawner.CatsInCurrentWave;
        }

        //Debug.Log($"Total: {_TotalCatsInWave}");

        _CatsRemainingInWave = _TotalCatsInWave;
    }

    private void FindAllSpawners()
    {
        _CatSpawners = FindObjectsByType<CatSpawner>(FindObjectsSortMode.None).ToList();
    }



    public int WaveCount { get { return _WaveNumber; } }
    public bool IsWaveInProgress { get { return _WaveInProgress; } }

}

