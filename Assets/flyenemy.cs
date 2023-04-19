using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyenemy : MonoBehaviour
{
    public float chaseSpeed = 5f;           // ׷���ٶ�
    public float impactForce = 5f;          // �ܻ�������
    public float minX = -10f;   // ���˿ɻ����Сxֵ
    public float maxX = 35f;    // ���˿ɻ�����xֵ
    public int maxHealth = 2;               // �����������ֵ
    private Vector2 playerPosition;         // ��ҵ�λ��
    private float currentHealth;            // ��ǰ����ֵ
    private bool isDead;                    // �Ƿ�������
    private SpriteRenderer spriteRenderer;  // ���˵���Ⱦ���
    private Animator animator;              // ���˵Ķ������
    private bool isChasing;                 // �Ƿ�����׷�����
    private Rigidbody2D enemyRigidbody;     // ���˵ĸ������


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
        // �޶����˵Ļ��Χ
        float x = transform.position.x;
        x = Mathf.Clamp(x, minX, maxX);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // ����Ƿ��ӵ�����
        if (other.CompareTag("bullet"))
        {
            TakeDamage();
        }
    }
    void TakeDamage()
    {
        currentHealth--;
        // ��Ӻ�����
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
                // ���������������Ч�ȴ���
                Destroy(gameObject);
        score myscore = GameObject.FindObjectOfType<score>();
        myscore.enemykilled();
    }
            void ChasePlayer()
            {
                // ���㵱ǰ��ҵ�λ�úͷ���
                Vector2 direction = playerPosition - (Vector2)transform.position;

                // ���õ��˵��ƶ��ٶȺͳ���
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