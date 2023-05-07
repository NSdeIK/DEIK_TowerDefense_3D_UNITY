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
        //TODO - K�nny� scene
        Debug.Log("K�nny� m�d aktiv�lva");
    }

    public void NewGameWithHardDifficulty()
    {
        //TODO - Neh�z
        //scene
        Debug.Log("Neh�z m�d aktiv�lva");
    }

}
