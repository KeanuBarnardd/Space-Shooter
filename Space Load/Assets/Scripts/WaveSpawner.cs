using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour {

    /// <summary>
    /// Handles the Spawning of Enemy Units - in an increasing difficulty oreder
    /// </summary>

    //Variables
    [Header("Spawning")]
    public Transform UfoPrefab;
    public Transform cruiserPrefab;
    public Transform UFOspawnPoint;
    public Transform CruiserSpawnPoint;


    public float timeBetweenWave = 8f;
    private float countDown = 3f;

    public int numOfEnemies;
    private int waveNumber = 1;
	
	// Update is called once per frame
	void Update () {
        if (countDown <= 0f) {

            //Spawn Enemies
            StartCoroutine(SpawnUFOWave());
            StartCoroutine(SpawnCruiserWave());

            countDown = timeBetweenWave;
            if (waveNumber >= 6) {
                waveNumber = 0;
            }
        }
        countDown -= Time.deltaTime;
	}

    /// <summary>
    /// IMPROVE AT THIS LOCATION DUDE
    /// </summary>
    
    IEnumerator SpawnUFOWave() {
       waveNumber++;
        for (int i = 0; i < waveNumber; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
          
        }
    }

    IEnumerator SpawnCruiserWave() {
       waveNumber++;
        for (int i = 0; i < waveNumber; i++) {
            SpawnEnemyCruiser();
            yield return new WaitForSeconds(5.3f);  
        }
    }

    public void SpawnEnemy() {
        Instantiate(UfoPrefab, UFOspawnPoint.position, UFOspawnPoint.rotation);
    }

    public void SpawnEnemyCruiser() {
        Instantiate(cruiserPrefab, CruiserSpawnPoint.position, CruiserSpawnPoint.rotation);
    }
}
