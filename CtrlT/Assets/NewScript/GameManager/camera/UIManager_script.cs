using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_script : MonoBehaviour {

    GameObject Main;

    public void DisableBoolAnimation(Animator anim)
    {
        Main = GameObject.Find("Main");
        Main.GetComponent<Main>()._isReverse = false;
        anim.SetBool("isDisplayed", false);
    }

    public void EnableBoolAnimation(Animator anim)
    {
        anim.SetBool("isDisplayed", true);
    }

    public void NavigateTo(int scene)
    {
        Application.LoadLevel(scene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /*public void PauseGame()
    {
        Time.timeScale = 0;
    }*/

    public void UnPauseGame()
    {
        Time.timeScale = 1;
    }

   
}
