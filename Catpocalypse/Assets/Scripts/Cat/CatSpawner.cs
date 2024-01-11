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

    [SerializeField] private GameObject normalCat;
    [SerializeField] private GameObject heavyCat;
    [SerializeField] private GameObject lightCat;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private PlayerHealthManager healthManager;
    [SerializeField] private PlayerCutenessManager cutenessManager;

    private List<GameObject> catsToSpawn;

    private int waveCount = 0;
    private int catsInWave = 5;
    private int catsRemaining;


    // Start is called before the first frame update
    void Start()
    {
        catsToSpawn = new List<GameObject>();
        catsToSpawn.Add(normalCat);
        catsToSpawn.Add(lightCat);
        catsToSpawn.Add(heavyCat);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] cats = GameObject.FindGameObjectsWithTag("Cat");
        if (cats.Length == 0 && healthManager.GetPlayerOutOfHealth() != true)
        {
            startWaveButton.enabled = true;
        }                
    }
    public void StartNextWave()
    {
        HUD.ShowWaveDisplay();

        waveCount++;
        if(waveCount == 1)
        {
            StartCoroutine(Spawner());
        }
        else
        {
            catsInWave += 2;
            StartCoroutine(Spawner());
        }
        startWaveButton.enabled = false;
    }
    public void StopSpawner()
    {
        StopCoroutine(Spawner());
        HUD.HideWaveDisplay();
    }
    IEnumerator Spawner()
    {
        CatBase.OnCatDied += OnCatDied;

        catsRemaining = catsInWave;
        
        for (int i = 0; i < catsInWave; i++)
        {
            int index = Random.Range(0,3);
            GameObject catPrefab = catsToSpawn[index];
            
            GameObject cat = Instantiate(catPrefab, spawnPoint1);
            
            CatBase catComponent = cat.GetComponent<CatBase>();
            cutenessManager.AddCuteness(catComponent.Cuteness);            

            yield return new WaitForSeconds(2f);
        }
        
    }

    private void OnCatDied(object sender, EventArgs e)
    {
        catsRemaining--;
        HUD.UpdateWaveInfoDisplay(catsInWave, catsRemaining);

        if (catsRemaining < 1)
            HUD.HideWaveDisplay();
    }
}
