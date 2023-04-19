using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class score : MonoBehaviour
{
    // 敌人对象
    public GameObject enemy;
    // 计分UI的TextMeshProUGUI对象
    public TextMeshProUGUI scoreText;
    public int scoren;

    private void Start()
    {
        scoreText.text = "Score: " + scoren.ToString();
        scoren = 0;
    }

    private void OnDestroy()
    {
        if (gameObject == enemy)
        {
            // 在这里执行你要处理的代码
            scoren++;
            // 更新计分UI的文本
            scoreText.text = "Score: " + scoren.ToString();
        }
    }
    public void enemykilled()
    {
        // 在这里执行你要处理的代码 
        scoren++;
        // 更新计分UI的文本
        scoreText.text = "Score: " + scoren.ToString();
    }

     void Update()
    {
        if (scoren >9)
            SceneManager.LoadScene("GameOverWin");

    }
}