using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{

    public GameObject NewGameButton;
    public GameObject DifficultyPanel;

    public void NewGame()
    {
        NewGameButton?.gameObject.SetActive(false);
        DifficultyPanel?.gameObject.SetActive(true);
    }

    public void NewGameWithEasyDifficulty()
    {
        //TODO - Könnyû scene
        Debug.Log("Könnyû mód aktiválva");
    }

    public void NewGameWithHardDifficulty()
    {
        //TODO - Nehéz
        //scene
        Debug.Log("Nehéz mód aktiválva");
    }

}
