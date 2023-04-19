using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float explosionForce; // ��������������������С��������Unity�༭���е���
    public float destroyTime = 3f; // ��Ҫɾ����ʱ��
    
    private void OnTriggerEnter2D(Collider2D other)
    {
    // ����Ƿ���е���
    if (other.CompareTag("Enemy"))
    {
            Rigidbody2D enemyRigidbody = other.GetComponent<Rigidbody2D>();
            if (enemyRigidbody != null)
            {
                Vector2 bulletDirection = GetComponent<Rigidbody2D>().velocity.normalized;
                enemyRigidbody.AddForce(bulletDirection * explosionForce, ForceMode2D.Impulse);
                //Debug.Log("hitenemy");
               // Debug.Log(bulletDirection);
            }
            // �����ӵ�
            Destroy(gameObject);
    }
    }
    // Start is called before the first frame update

    void Start()
    {
        // ��ָ����ʱ��֮��ɾ���������
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
