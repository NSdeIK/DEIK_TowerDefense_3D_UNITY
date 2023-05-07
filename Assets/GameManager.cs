using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject loadingPanel;

    private GameObject cameraObj;

    MapGenerator mapGenerator;

    void Start()
    {
        cameraObj = GameObject.Find("Main Camera");
        mapGenerator = GetComponent<MapGenerator>();
        StartCoroutine(generateMap());

    }

    IEnumerator generateMap()
    {
        yield return StartCoroutine(mapGenerator.GenerateMap());
        startGame();
    }

    private void startGame()
    {
        loadingPanel.SetActive(false);
        cameraObj.GetComponent<CameraMovement>().enabled = true;
    }

    void Update()
    {
        
    }
}
