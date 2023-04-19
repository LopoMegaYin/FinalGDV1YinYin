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
        // ��ʱ��С�ڵ���80��ʱִ�в���
        //GeneratingFlyMonster();
        //}
    }

    //void GeneratingFlyMonster()
    //{
        // ִ������Ҫ�Ĳ���
       // Debug.Log("Time is up!");
    //}
}