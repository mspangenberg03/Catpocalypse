using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CatSpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("A list of Wave scriptable Objects that have a list of cats to spawn")]
    [SerializeField]
    private List<Wave> _CatsToSpawn;

    [Tooltip("The time between cats to spawn")]
    [SerializeField]
    private float _TimeBetweenSpawns;

    [Header("Game Object References")]
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform _SpawnPoint1;

    [SerializeField] private GameObject _NormalCat;
    [SerializeField] private GameObject _HeavyCat;
    [SerializeField] private GameObject _LightCat;
    [SerializeField] private PlayerCutenessManager _CutenessManager;

    private int _CurrentWave;


    // Start is called before the first frame update
    void Start()
    {
        _CurrentWave = 0;
    }

    public void StartNextWave()
    {       
        StartCoroutine(Spawner(_CurrentWave++));
    }
    public void StopSpawner()
    {
        StartCoroutine(Spawner(_CurrentWave));
    }
    IEnumerator Spawner(int currentWave)
    {
        int catsLeftInWave = _CatsToSpawn[currentWave].cats.Count;
        for (int i = 0; i < catsLeftInWave; i++)
        {
            GameObject catPrefab = _CatsToSpawn[_CurrentWave].cats[catsLeftInWave];

            GameObject cat = Instantiate(catPrefab, _SpawnPoint1);

            CatBase catComponent = cat.GetComponent<CatBase>();
            _CutenessManager.AddCuteness(catComponent.Cuteness);

            yield return new WaitForSeconds(2f);
        }

    }


    public int CatsInCurrentWave { get { return _CatsToSpawn[_CurrentWave].cats.Count; } }
}
