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
    public static WaveManager Instance;

    [SerializeField] private int _TotalWavesInLevel = 5;

    private List<CatSpawner> _CatSpawners;
    private int _TotalCatsInWave;
    private int _CatsRemainingInWave;

    private int _WaveCount = 0;
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
        CatBase.OnCatReachGoal += OnCatReachGoal;

        HUD.HideWaveDisplay();
    }

    private void Update()
    {
        if (!_WaveInProgress && WaveCount == _TotalWavesInLevel)
        {
            HUD.RevealVictory();
        }
    }

    public void StartNextWave()
    {
        // Don't try to start a wave if one is already in progress.
        if (IsWaveInProgress)
            return;

        _WaveInProgress = true;

        _WaveCount++;


        FindAllSpawners();

        foreach (CatSpawner spawner in _CatSpawners)
        {
            spawner.StartNextWave();
        }

        CalculateTotalCatsInWave();

        HUD.ShowWaveDisplay();
        HUD.UpdateWaveInfoDisplay(_WaveCount, _CatsRemainingInWave);
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

        HUD.UpdateWaveInfoDisplay(_TotalCatsInWave, _CatsRemainingInWave);

        if (_CatsRemainingInWave < 1)
        {
            HUD.HideWaveDisplay();
            _WaveInProgress = false;

            if (_WaveCount >= _TotalWavesInLevel && !FindObjectOfType<PlayerHealthManager>().IsPlayerDead)
                FindObjectOfType<VictoryScreen>()?.Show();
        }
    }
    public void OnCatReachGoal(object Sender, EventArgs e)
    {
        _CatsRemainingInWave--;

        HUD.UpdateWaveInfoDisplay(_TotalCatsInWave, _CatsRemainingInWave);

        if (_CatsRemainingInWave < 1)
        {
            HUD.HideWaveDisplay();
            _WaveInProgress = false;

            if (_WaveCount >= _TotalWavesInLevel && !FindObjectOfType<PlayerHealthManager>().IsPlayerDead)
                FindObjectOfType<VictoryScreen>()?.Show();
        }
    }

    private void CalculateTotalCatsInWave()
    {
        _TotalCatsInWave = 0;
        foreach (CatSpawner spawner in _CatSpawners)
        {
            _TotalCatsInWave += spawner.CatsInWave;
        }

        _CatsRemainingInWave = _TotalCatsInWave;
    }

    private void FindAllSpawners()
    {
        _CatSpawners = FindObjectsByType<CatSpawner>(FindObjectsSortMode.None).ToList();
    }

    public int WaveCount { get { return _WaveCount; } }
    public bool IsWaveInProgress { get { return _WaveInProgress; } }

}

