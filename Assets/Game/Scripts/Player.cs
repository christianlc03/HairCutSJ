using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    //VARIABLES
    public float velocidad = 8f;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal");
        float velocidadY = Input.GetAxis("Vertical");

        Vector2 velocidadMov = new Vector2(velocidadX * velocidad, velocidadY * velocidad);
        rb.velocity = velocidadMov;

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
