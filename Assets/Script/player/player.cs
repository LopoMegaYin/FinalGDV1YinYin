using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class player : MonoBehaviour

{
    public GameObject playergunner;
    public Transform playerPosition;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    public bool deadbody = false;

    private Rigidbody2D pgunner;
    public Animator animator;
    public SpriteRenderer gunnersprite;
    public float deadtime = 2f;

    // Start is called before the first frame update
    void Start()
    {
        pgunner = GetComponent<Rigidbody2D>();
        playerPosition = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        
        // 获取水平方向的输入值
        float horizontalInput = Input.GetAxis("Horizontal");
        

        if (horizontalInput < 0)
        {
            gunnersprite.flipX = true;
        }
        else
        {
            gunnersprite.flipX = false;
        }
        // 根据输入值移动物体
        if (!deadbody)
        {
            transform.position += new Vector3(horizontalInput * moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal"))); 

        // 判断是否跳跃
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded &&!deadbody)
        {
            pgunner.AddForce(new Vector2(pgunner.velocity.x, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
        playerMoveLimitation();

        animator.SetBool("death", deadbody);

        if (deadbody)
        {
            horizontalInput = 0;
        }

    }


    void playerMoveLimitation()//player position limitation
    {
        playerPosition.position = new Vector3(Mathf.Clamp(playerPosition.position.x, -8f, 34f), playerPosition.position.y, playerPosition.position.z);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 判断是否落地
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    //死亡动画
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(Death());

        }

        IEnumerator Death()
        {
            deadbody = true;
            yield return new WaitForSeconds(deadtime);
            Destroy(gameObject);
            SceneManager.LoadScene("GameOverLose");

        }

      
    }
}
