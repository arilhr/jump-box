using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public string startingLevelScene;

    public Button continueButton;

    private void Start()
    {
        SetMenuUI();
    }

    private void SetMenuUI()
    {
        continueButton.interactable = PlayerPrefs.HasKey("Last Game");
    }

    public void ContinueGame()
    {
        if (PlayerPrefs.HasKey("Last Game"))
        {
            string sceneName = PlayerPrefs.GetString("Last Game");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            StartNewGame();
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(startingLevelScene);
    }
}
