using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class countdown : MonoBehaviour
{

    public float timeLeft = 100f;
    public TextMeshProUGUI countdownText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        countdownText.text = "Time Left: " + Mathf.Round(timeLeft).ToString();

        if (timeLeft<0f)
        {
            SceneManager.LoadScene("GameOverLose");
        }

        //if (timeLeft <= 80f)
        //{
        // 在时间小于等于80秒时执行操作
        //GeneratingFlyMonster();
        //}
    }

    //void GeneratingFlyMonster()
    //{
        // 执行你想要的操作
       // Debug.Log("Time is up!");
    //}
}