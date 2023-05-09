using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    public TMP_InputField inputField;
    int reqScore;
    string gameMode;
    public void PlayGame(string sceneName)
    {
        reqScore = int.Parse(inputField.text);
        PlayerPrefs.SetInt("reqScore", reqScore);
        PlayerPrefs.SetString("gameMode", gameMode);
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
