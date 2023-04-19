using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseenemy : MonoBehaviour
{
    public float patrolSpeed = 1f;          // Ѳ���ٶ�
    public float chaseSpeed = 3f;           // ׷���ٶ�
    public float patrolRange = 5f;          // Ѳ�߷�Χ
    public float chaseRange = 10f;          // ׷��Χ
    public float impactForce = 5f;          // �ܻ�������
    public float minX = -10f;   // ���˿ɻ����Сxֵ
    public float maxX = 35f;    // ���˿ɻ�����xֵ
    public float miny = 0f;  // ���˿��ƶ�����С Y ����
    public float maxy = 0f;  // ���˿��ƶ������ Y ����
    public int maxHealth = 3;               // �����������ֵ
    public float hittimer = 0;
    public float hitduration;

    private float currentHealth;            // ��ǰ����ֵ
    private bool isDead;                    // �Ƿ�������
    private bool isChasing;                 // �Ƿ�����׷�����
    private bool isHiting;
    private Rigidbody2D enemyRigidbody;     // ���˵ĸ������
    private Vector2[] patrolPoints;         // Ѳ�ߵ�
    private int currentPatrolIndex;         // ��ǰѲ�ߵ���±�
    private Vector2 playerPosition;         // ��ҵ�λ��
    private SpriteRenderer spriteRenderer;  // ���˵���Ⱦ���
    private Animator animator;              // ���˵Ķ������

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        isChasing = false;
        enemyRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        // ��ʼ��Ѳ�ߵ�
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

        // �������Ƿ����׷��Χ��
        float distance = Vector2.Distance(transform.position, playerPosition);
        if (distance <= chaseRange)
        {
            isChasing = true;
        }
        

        if (isHiting)
        {
            HittenEnemy();
        }
        // Ѳ�߻�׷�����
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


        // �޶����˵Ļ��Χ
        float x = transform.position.x;
        x = Mathf.Clamp(x, minX, maxX);
        float y = transform.position.y;
        y = Mathf.Clamp(y, miny, maxy);
        transform.position = new Vector3(x, y, transform.position.z);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // ����Ƿ��ӵ�����
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
        // ��Ӻ�����
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
        // ���������������Ч�ȴ���
        Destroy(gameObject);
        score myscore = GameObject.FindObjectOfType<score>();
        myscore.enemykilled();
    }

    void Patrol()
    {
        // ���㵱ǰĿ���ķ���
        Vector2 direction = patrolPoints[currentPatrolIndex] - (Vector2)transform.position;
        direction.y = 0;

        // ��������˵�ǰĿ��㣬��ת����һ��Ŀ���
        if (direction.magnitude < 0.1f)
        {
        
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            direction = patrolPoints[currentPatrolIndex] - (Vector2)transform.position;
        }
        // ���õ��˵��ƶ��ٶȺͳ���
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
        // ���㵱ǰ��ҵ�λ�úͷ���
        Vector2 direction = playerPosition - (Vector2)transform.position;

        // ���õ��˵��ƶ��ٶȺͳ���
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
        // ���Ƶ�����y���ϵ��ƶ���Χ
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
