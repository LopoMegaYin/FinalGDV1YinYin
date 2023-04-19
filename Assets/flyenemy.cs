using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyenemy : MonoBehaviour
{
    public float chaseSpeed = 5f;           // 追逐速度
    public float impactForce = 5f;          // 受击后坐力
    public float minX = -10f;   // 敌人可活动的最小x值
    public float maxX = 35f;    // 敌人可活动的最大x值
    public int maxHealth = 2;               // 敌人最大生命值
    private Vector2 playerPosition;         // 玩家的位置
    private float currentHealth;            // 当前生命值
    private bool isDead;                    // 是否已死亡
    private SpriteRenderer spriteRenderer;  // 敌人的渲染组件
    private Animator animator;              // 敌人的动画组件
    private bool isChasing;                 // 是否正在追踪玩家
    private Rigidbody2D enemyRigidbody;     // 敌人的刚体组件


    // Start is called before the first frame update
    void Start()
    {

        currentHealth = maxHealth;
        isDead = false;
        isChasing = true;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    // Update is called once per frame
    void Update()

    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        if (isDead)
        {
            return;
        }
        if (isChasing)
        {
            ChasePlayer();
        }
        // 限定敌人的活动范围
        float x = transform.position.x;
        x = Mathf.Clamp(x, minX, maxX);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // 检测是否被子弹击中
        if (other.CompareTag("bullet"))
        {
            TakeDamage();
        }
    }
    void TakeDamage()
    {
        currentHealth--;
        // 添加后坐力
        Vector2 direction = ((Vector2)transform.position - playerPosition).normalized;
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
            void ChasePlayer()
            {
                // 计算当前玩家的位置和方向
                Vector2 direction = playerPosition - (Vector2)transform.position;

                // 设置敌人的移动速度和朝向
                enemyRigidbody.velocity = direction.normalized * chaseSpeed;
                if (direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                
            }
        
    
}