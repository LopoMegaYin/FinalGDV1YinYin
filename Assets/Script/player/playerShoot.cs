using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class playerShoot : MonoBehaviour
{
    public Rigidbody2D bulletPrefab;
    public float bulletSpeed = 10.0f;
    public float screenShakeDuration = 0.1f;
    public float screenShakeMagnitude = 0.05f;
    public float recoilForce = 10f;
    private Rigidbody2D pgunner;
    private Camera mainCamera;
    private bool screenShake = false;
    public float reloadTime = 2f;
    public float Firetime;
    public int maxAmmo = 10;
    private int currentAmmo;
    private bool isReloading;
    public TMP_Text ammoText;
    public Color reloadingColor = Color.yellow;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool nofire=false;
    private player playerscript;



    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        pgunner = GetComponent<Rigidbody2D>();
        currentAmmo = maxAmmo;
        isReloading = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerscript = GetComponent<player>();

        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerscript.deadbody)
        {
            nofire=true;
        }
        if (Input.GetButtonDown("Fire1")&& currentAmmo>0 && !isReloading && !nofire)
        {
            //鼠标点击射击方向相关
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -mainCamera.transform.position.z;
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mousePos);
            Vector2 shootDir = (mouseWorldPos - transform.position).normalized;

            if (shootDir.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (shootDir.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            //子弹射出相关
            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletInstance.AddForce(shootDir * bulletSpeed, ForceMode2D.Impulse);
            // Add recoil force to the player object
            pgunner.AddForce(-shootDir * recoilForce, ForceMode2D.Impulse);
            //震屏
            screenShake = true;
            //换弹
            currentAmmo--;
            //更新残弹显示
            UpdateAmmoText();
            animator.SetTrigger("shoot");

        }

        

        

        if (Input.GetKeyDown(KeyCode.R)&&currentAmmo<maxAmmo&&!isReloading && !nofire)
        {
            isReloading = true;
            Invoke("Reload", reloadTime);
            UpdateAmmoText();
        }

     
        animator.SetBool("reloading", isReloading);


        if (screenShake)
        { 
            StartCoroutine(ShakeScreen());
            StartCoroutine(ScreenShake());
        }
    }
  

 
    IEnumerator ScreenShake()
    {
        screenShake = true;
        yield return new WaitForSeconds(screenShakeDuration);
        screenShake = false;
    }
    IEnumerator ShakeScreen()
    {
        float ShakeTime = 0.0f;

        while (ShakeTime < screenShakeDuration)
        {
            float x =  Random.Range(-screenShakeMagnitude, screenShakeMagnitude);
            float y =  Random.Range(-screenShakeMagnitude, screenShakeMagnitude);

            mainCamera.transform.localPosition = new Vector3(x, y, 0);

            ShakeTime += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.localPosition = Vector3.zero;
    }
    private void Reload()
    {
        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoText();
    }
    private void UpdateAmmoText()
    {
        if (isReloading)
        {
            ammoText.text = "Reloading...";
            ammoText.color = reloadingColor;

        }
        else
        {
            ammoText.text = "Ammo: " + currentAmmo.ToString() + " / " + maxAmmo.ToString();
            ammoText.color = Color.white;

        }

    }
}
