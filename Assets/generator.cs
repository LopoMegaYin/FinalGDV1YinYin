using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    // ���˵�Ԥ����
    public GameObject enemyPrefab;
    // ���ɵ��˵�ʱ����
    public float spawnTime = 2f;
    // ����������
    public int maxEnemies = 5;
    // �������ɵ�x�᷶Χ
    public float xMin = -5f, xMax = 5f;
    // �������ɵ�y�᷶Χ
    public float yMin = 0f, yMax = 5f;
    // ���ɵ���ʱ��Player���ֵ���С����
    public float minDistance;
    // ���ɵ���ʱ�����е��˱��ֵ���С����
    public float minDistanceBetweenEnemies;

    // Player����
    private GameObject player;
    // ��ǰ��������
    private int enemyCount;

    // Start ��������Ϸ��������ʱ�����ã��˴�������ʼ��
    private void Start()
    {
        // ͨ����ǩ��ȡ Player ����
        player = GameObject.FindGameObjectWithTag("Player");
        // ͨ�� InvokeRepeating ������ʱ���ɵ���
        InvokeRepeating("SpawnEnemy", 0f, spawnTime);
    }

    // ���ɵ��˵ķ���
    private void SpawnEnemy()
    {
        // �����ǰ���������Ѿ��ﵽ���ֵ����������
        if (enemyCount >= maxEnemies)
            return;

        // ��ȡ����ĵ�������λ��
        Vector2 spawnPosition = GetRandomSpawnPosition();
        // �������λ�þ��� Player ����������������
        while (Vector2.Distance(spawnPosition, player.transform.position) < minDistance)
            spawnPosition = GetRandomSpawnPosition();

        // �������λ�þ������е��˹���������������
        if (!IsTooCloseToOtherEnemies(spawnPosition))
        {
            // ���ɵ��˲����ӵ�������
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyCount++;
        }
    }

    // ��ȡ����ĵ�������λ��
    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        return new Vector2(x, y);
    }

    // �ж�����λ���Ƿ�������е��˹���
    private bool IsTooCloseToOtherEnemies(Vector2 position)
    {
        // ��ȡ���б�ǩΪ Enemy ����Ϸ����
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // �������֮�����������򷵻� true
            if (Vector2.Distance(position, enemy.transform.position) < minDistanceBetweenEnemies)
                return true;
        }
        return false;
    }

    // ���ٵ�������
    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }

    // ���ӵ�������
    public void IncreaseEnemyCount()
    {
        enemyCount++;
    }
}