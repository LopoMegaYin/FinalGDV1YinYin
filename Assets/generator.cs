using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    // 敌人的预制体
    public GameObject enemyPrefab;
    // 生成敌人的时间间隔
    public float spawnTime = 2f;
    // 最大敌人数量
    public int maxEnemies = 5;
    // 敌人生成的x轴范围
    public float xMin = -5f, xMax = 5f;
    // 敌人生成的y轴范围
    public float yMin = 0f, yMax = 5f;
    // 生成敌人时与Player保持的最小距离
    public float minDistance;
    // 生成敌人时与已有敌人保持的最小距离
    public float minDistanceBetweenEnemies;

    // Player对象
    private GameObject player;
    // 当前敌人数量
    private int enemyCount;

    // Start 方法在游戏对象被启用时被调用，此处用来初始化
    private void Start()
    {
        // 通过标签获取 Player 对象
        player = GameObject.FindGameObjectWithTag("Player");
        // 通过 InvokeRepeating 方法定时生成敌人
        InvokeRepeating("SpawnEnemy", 0f, spawnTime);
    }

    // 生成敌人的方法
    private void SpawnEnemy()
    {
        // 如果当前敌人数量已经达到最大值，则不再生成
        if (enemyCount >= maxEnemies)
            return;

        // 获取随机的敌人生成位置
        Vector2 spawnPosition = GetRandomSpawnPosition();
        // 如果生成位置距离 Player 过近，则重新生成
        while (Vector2.Distance(spawnPosition, player.transform.position) < minDistance)
            spawnPosition = GetRandomSpawnPosition();

        // 如果生成位置距离已有敌人过近，则重新生成
        if (!IsTooCloseToOtherEnemies(spawnPosition))
        {
            // 生成敌人并增加敌人数量
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            enemyCount++;
        }
    }

    // 获取随机的敌人生成位置
    private Vector2 GetRandomSpawnPosition()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        return new Vector2(x, y);
    }

    // 判断生成位置是否距离已有敌人过近
    private bool IsTooCloseToOtherEnemies(Vector2 position)
    {
        // 获取所有标签为 Enemy 的游戏对象
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            // 如果敌人之间距离过近，则返回 true
            if (Vector2.Distance(position, enemy.transform.position) < minDistanceBetweenEnemies)
                return true;
        }
        return false;
    }

    // 减少敌人数量
    public void DecreaseEnemyCount()
    {
        enemyCount--;
    }

    // 增加敌人数量
    public void IncreaseEnemyCount()
    {
        enemyCount++;
    }
}