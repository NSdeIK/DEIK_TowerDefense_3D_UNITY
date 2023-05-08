using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject loadingPanel;
    public GameObject turretsPanel;

    private GameObject cameraObj;

    MapGenerator mapGenerator;
    WaveManager waveManager;

    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
        mapGenerator = GetComponent<MapGenerator>();
        waveManager = GetComponent<WaveManager>();
        StartCoroutine(generateMap());

    }

    IEnumerator generateMap()
    {
        yield return StartCoroutine(mapGenerator.GenerateMap());
        yield return setWaveManagerConfig();
        startGame();
    }

    IEnumerator setWaveManagerConfig()
    {
        waveManager.SetPathCells(mapGenerator.getPathCells());
        waveManager.SetEnemyBeginCell(mapGenerator.getEnemyBaseObject().transform);
        yield return null;
    }

    private void startGame()
    {
        loadingPanel.SetActive(false);
        turretsPanel.SetActive(true);
        cameraObj.GetComponent<CameraMovement>().enabled = true;
    }

    void Update()
    {
        
    }
}
