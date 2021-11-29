using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    [HideInInspector] public bool IsPlaying;

    public GameObject winPanel;

    public string nextLevel;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IsPlaying = true;
    }

    public void Win()
    {
        IsPlaying = false;

        // TODO: show win ui
        winPanel.SetActive(true);

        if (nextLevel != string.Empty)
            PlayerPrefs.SetString("Last Game", nextLevel);
    }

    public void GoToNextLevel()
    {
        if (nextLevel == string.Empty)
        {
            SceneManager.LoadScene("Menu");
            return;
        }

        SceneManager.LoadScene(nextLevel);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
