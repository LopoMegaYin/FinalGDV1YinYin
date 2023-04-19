using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class direction : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - transform.position;

            // Flip sprite horizontally if the mouse is to the left of the object
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            // Flip sprite horizontally back if the mouse is to the right of the object
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}