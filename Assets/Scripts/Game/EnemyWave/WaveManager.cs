using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class WaveParameters
{
    public string name;
    public int[] enemyPrefabIndex;
    public int[] spawnCount;
    public int spawnDelayMin;
    public int spawnDelayMax;
}

[System.Serializable]
public class Wave
{
    public string name;
    public List<WaveParameters> parameters;
}

public class WaveManager : MonoBehaviour
{
    public float difficultyFactor = 0.2f;
    public List<Wave> waves;

    public GameObject[] enemyPrefabs;
    public int waveLevel = 0;

    private Transform spawnPoint;
    private List<Vector2Int> pathCells;


    public bool nextWave = true;

    void Update()
    {
        if(spawnPoint != null)
        {
            if (nextWave)
            {
                Debug.Log("WaveManager - Kovetkezo wave feldolgozasa");
                StartCoroutine(spawnEnemy());
                waveLevel++;
                nextWave = false;
            }
        }
    }

    IEnumerator spawnEnemy()
    {
        Debug.Log("WaveManager - Ellenfelek spawnolasa");
        foreach (WaveParameters enemy in waves[waveLevel].parameters)
        {
            if(enemy.spawnCount.Length > 0 && enemy.enemyPrefabIndex.Length > 0)
            {
                int temp = 0;
                foreach(int enemyCount in enemy.spawnCount)
                {
                    for(int i = 0; i < enemyCount; i++)
                    {
                        int spawnDelay = Random.Range(enemy.spawnDelayMin, enemy.spawnDelayMax);
                        if(enemyPrefabs.Length > temp && enemyPrefabs[temp] != null)
                        {
                            GameObject enemyPrefab = Instantiate(enemyPrefabs[temp], new Vector3(spawnPoint.position.x, 2.5f, spawnPoint.position.z), spawnPoint.rotation);
                            yield return new WaitForSeconds(spawnDelay);
                        }
                    }
                    temp++;
                }
            }
        }
        yield return null;
    }

    public void SetPathCells(List<Vector2Int> pathCells)
    {
        Debug.Log("WaveManager - Cellak beallitva");
        this.pathCells = pathCells;
    }

    public void SetEnemyBeginCell(Transform EnemyBeginCell)
    {
        Debug.Log("WaveManager - Enemy bazis beallitva");
        spawnPoint = EnemyBeginCell;
    }

    public void getNextWave()
    {
        Debug.Log("WaveManager - Kovetkezo wave lekerese");
        this.nextWave = true;
    }

    public List<Vector2Int> getEnemyPathCells()
    {
        //Debug.Log("WaveManager - Cellak lekerese");
        return pathCells;
    }
}
