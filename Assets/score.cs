using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class score : MonoBehaviour
{
    // ���˶���
    public GameObject enemy;
    // �Ʒ�UI��TextMeshProUGUI����
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
            // ������ִ����Ҫ����Ĵ���
            scoren++;
            // ���¼Ʒ�UI���ı�
            scoreText.text = "Score: " + scoren.ToString();
        }
    }
    public void enemykilled()
    {
        // ������ִ����Ҫ����Ĵ��� 
        scoren++;
        // ���¼Ʒ�UI���ı�
        scoreText.text = "Score: " + scoren.ToString();
    }

     void Update()
    {
        if (scoren >9)
            SceneManager.LoadScene("GameOverWin");

    }
}