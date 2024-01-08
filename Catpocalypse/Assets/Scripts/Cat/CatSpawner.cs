using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CatSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform spawnPoint1;

    [SerializeField] private GameObject normalCat;
    [SerializeField] private GameObject heavyCat;
    [SerializeField] private GameObject lightCat;
    [SerializeField] private Button startWaveButton;
    [SerializeField] private PlayerHealthManager healthManager;
    private List<GameObject> catsToSpawn;

    private int waveCount = 0;
    private int catsInWave = 5;


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
        waveCount++;
        if(waveCount == 1)
        {
            StartCoroutine(Spawner());
        }
        else
        {
            catsInWave += 2;
            StartCoroutine(Spawner());
            HUD.UpdateWaveNumberDisplay(waveCount);
        }
        startWaveButton.enabled = false;
    }
    public void StopSpawner()
    {
        StopCoroutine(Spawner());
    }
    IEnumerator Spawner()
    {
        
        for (int i = 0; i < catsInWave; i++)
        {
            int index = Random.Range(0,3);
            GameObject cat = catsToSpawn[index];
            Instantiate(cat, spawnPoint1);
            yield return new WaitForSeconds(2f);
        }
        

    }
}
