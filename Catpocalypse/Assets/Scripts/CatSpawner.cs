using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSpawner : MonoBehaviour
{
    [SerializeField, Tooltip("One possible spawn point for cats")]
    private Transform spawnPoint1;
    private int waveCount = 0;
    private int catsInWave = 5;
    [SerializeField] private GameObject normalCat;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Spawner());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextWave()
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
        }
    }
    IEnumerator Spawner()
    {
        
        for (int i = 0; i < catsInWave; i++)
        {
            
            Instantiate(normalCat, spawnPoint1);
            yield return new WaitForSeconds(2f);
        }
        

    }
}
