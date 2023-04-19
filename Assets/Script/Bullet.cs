using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float explosionForce; // 公共变量，爆发推力大小，可以在Unity编辑器中调整
    public float destroyTime = 3f; // 需要删除的时间
    
    private void OnTriggerEnter2D(Collider2D other)
    {
    // 检测是否击中敌人
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
            // 销毁子弹
            Destroy(gameObject);
    }
    }
    // Start is called before the first frame update

    void Start()
    {
        // 在指定的时间之后删除这个物体
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
