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
    public List<Wave> waves;

    public GameObject[] enemyPrefabs;

    private Transform spawnPoint;
    private List<Vector2Int> pathCells;

    private int waveLevel = 0;
    private bool nextWave = false;
    private int allEnemyCount = 0;

    IEnumerator spawnEnemy()
    {
        Debug.Log("WaveManager - Ellenfelek spawnolasa");
        foreach (WaveParameters enemy in waves[waveLevel].parameters)
        {
            if(enemy.spawnCount.Length > 0 && enemy.enemyPrefabIndex.Length > 0)
            {
                int temp = 0;
                allEnemyCount = enemy.spawnCount.Sum(x => x);
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

    private void nextWaveLevel()
    {
        if (spawnPoint != null)
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

    public bool getNextWave()
    {
        Debug.Log("WaveManager - Kovetkezo wave lekerese");
        if(waveLevel != waves.Count)
        {
            this.nextWave = true;
            nextWaveLevel();
            return true;
        }

        return false;
    }

    public int getWaveLevel()
    {
        return this.waveLevel;
    }

    public int getMaxWaveLevel()
    {
        return this.waves.Count;
    }

    public List<Vector2Int> getEnemyPathCells()
    {
        //Debug.Log("WaveManager - Cellak lekerese");
        return pathCells;
    }

    public int GetAllEnemyCount()
    {
        return allEnemyCount;
    }
}
