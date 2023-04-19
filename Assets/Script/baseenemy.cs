using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseenemy : MonoBehaviour
{
    public float patrolSpeed = 1f;          // 巡逻速度
    public float chaseSpeed = 3f;           // 追逐速度
    public float patrolRange = 5f;          // 巡逻范围
    public float chaseRange = 10f;          // 追逐范围
    public float impactForce = 5f;          // 受击后坐力
    public float minX = -10f;   // 敌人可活动的最小x值
    public float maxX = 35f;    // 敌人可活动的最大x值
    public float miny = 0f;  // 敌人可移动的最小 Y 坐标
    public float maxy = 0f;  // 敌人可移动的最大 Y 坐标
    public int maxHealth = 3;               // 敌人最大生命值
    public float hittimer = 0;
    public float hitduration;

    private float currentHealth;            // 当前生命值
    private bool isDead;                    // 是否已死亡
    private bool isChasing;                 // 是否正在追踪玩家
    private bool isHiting;
    private Rigidbody2D enemyRigidbody;     // 敌人的刚体组件
    private Vector2[] patrolPoints;         // 巡逻点
    private int currentPatrolIndex;         // 当前巡逻点的下标
    private Vector2 playerPosition;         // 玩家的位置
    private SpriteRenderer spriteRenderer;  // 敌人的渲染组件
    private Animator animator;              // 敌人的动画组件

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        isChasing = false;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // 初始化巡逻点
        Vector2 pos = transform.position;
        patrolPoints = new Vector2[2];
        patrolPoints[0] = pos + new Vector2(patrolRange, 0f);
        patrolPoints[1] = pos - new Vector2(patrolRange, 0f);
        currentPatrolIndex = 0;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        // 检测玩家是否进入追逐范围内
        float distance = Vector2.Distance(transform.position, playerPosition);
        if (distance <= chaseRange)
        {
            isChasing = true;
        }
        

        if (isHiting)
        {
            HittenEnemy();
        }
        // 巡逻或追踪玩家
        else if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        animator.SetBool("dash", isChasing);
        animator.SetBool("hurt", isHiting);


        // 限定敌人的活动范围
        float x = transform.position.x;
        x = Mathf.Clamp(x, minX, maxX);
        float y = transform.position.y;
        y = Mathf.Clamp(y, miny, maxy);
        transform.position = new Vector3(x, y, transform.position.z);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // 检测是否被子弹击中
        if (other.CompareTag("bullet"))
        {
            TakeDamage();
            isChasing = true;
            isHiting = true;
            hittimer = 0f;
        }
    }

    void TakeDamage()
    {
        currentHealth--;
        // 添加后坐力
        Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
        //direction.y = 0;
        enemyRigidbody.velocity = new Vector3 (0,0,0);
        enemyRigidbody.AddForce(direction * impactForce, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // 添加死亡动画和音效等代码
        Destroy(gameObject);
        score myscore = GameObject.FindObjectOfType<score>();
        myscore.enemykilled();
    }

    void Patrol()
    {
        // 计算当前目标点的方向
        Vector2 direction = patrolPoints[currentPatrolIndex] - (Vector2)transform.position;
        direction.y = 0;

        // 如果到达了当前目标点，则转向下一个目标点
        if (direction.magnitude < 0.1f)
        {
        
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            direction = patrolPoints[currentPatrolIndex] - (Vector2)transform.position;
        }
        // 设置敌人的移动速度和朝向
        enemyRigidbody.velocity = direction.normalized * patrolSpeed;
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    void ChasePlayer()
    {
        // 计算当前玩家的位置和方向
        Vector2 direction = playerPosition - (Vector2)transform.position;

        // 设置敌人的移动速度和朝向
        enemyRigidbody.velocity = direction.normalized * chaseSpeed;
        //enemyRigidbody.AddForce(direction.normalized * chaseSpeed * Time.deltaTime);
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        // 限制敌人在y轴上的移动范围
        float newY = Mathf.Clamp(transform.position.y, miny, maxy);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    void HittenEnemy()
    {
        hittimer += Time.deltaTime;
        if (hittimer > hitduration)
        {
            isHiting = false;
        }
        //Debug.Log(enemyRigidbody.velocity);
        //if (enemyRigidbody.velocity.magnitude<0.1f)
        //{
            //isHiting = false;
        //}

    }

}
