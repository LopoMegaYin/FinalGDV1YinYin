using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class losemenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Instruction");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
