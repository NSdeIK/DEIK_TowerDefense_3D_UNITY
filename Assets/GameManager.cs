using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    MapGenerator mapGenerator;

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        mapGenerator.GenerateMap();
    }

    void Update()
    {
        
    }
}
