using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour {
    public string restartScence;

	public void restartGame()
    {
        SceneManager.LoadScene(restartScence);
    }

    public void Warp(string input)
    {
        SceneManager.LoadScene(input);
    }

    public void exitGame()
    {
        Debug.Log("Exit Button");
        Application.Quit();
    }
}
