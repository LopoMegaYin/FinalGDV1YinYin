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
        
        // ��ȡˮƽ���������ֵ
        float horizontalInput = Input.GetAxis("Horizontal");
        

        if (horizontalInput < 0)
        {
            gunnersprite.flipX = true;
        }
        else
        {
            gunnersprite.flipX = false;
        }
        // ��������ֵ�ƶ�����
        if (!deadbody)
        {
            transform.position += new Vector3(horizontalInput * moveSpeed, 0f, 0f) * Time.deltaTime;
        }

        animator.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal"))); 

        // �ж��Ƿ���Ծ
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
        // �ж��Ƿ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    //��������
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
