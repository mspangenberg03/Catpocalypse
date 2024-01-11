using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random;


public class CatSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform spawnPoint1;

    [SerializeField] private int _CatsInFirstWave = 5;

    [SerializeField] private GameObject normalCat;
    [SerializeField] private GameObject heavyCat;
    [SerializeField] private GameObject lightCat;
    [SerializeField] private PlayerCutenessManager cutenessManager;

    private List<GameObject> catsToSpawn;


    private int catsInWave;


    // Start is called before the first frame update
    void Start()
    {
        catsInWave = _CatsInFirstWave;

        catsToSpawn = new List<GameObject>();
        catsToSpawn.Add(normalCat);
        catsToSpawn.Add(lightCat);
        catsToSpawn.Add(heavyCat);
    }

    public void StartNextWave()
    {       
        if (WaveManager.Instance.WaveCount == 1)
        {
            StartCoroutine(Spawner());
        }
        else
        {
            catsInWave += 2;
            StartCoroutine(Spawner());
        }
    }
    public void StopSpawner()
    {
        StopCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        for (int i = 0; i < catsInWave; i++)
        {
            int index = Random.Range(0, 3);
            GameObject catPrefab = catsToSpawn[index];

            GameObject cat = Instantiate(catPrefab, spawnPoint1);

            CatBase catComponent = cat.GetComponent<CatBase>();
            cutenessManager.AddCuteness(catComponent.Cuteness);

            yield return new WaitForSeconds(2f);
        }

    }


    public int CatsInWave { get { return _CatsInFirstWave; } }
}
