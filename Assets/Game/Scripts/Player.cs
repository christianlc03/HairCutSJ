using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Player : MonoBehaviour
{
    // MOVIMIENTO
    public float velocidad = 8f;
    private Rigidbody2D rb;

    // DASH
    public float distanciaDash = 5f;
    public float duracionDash = 0.2f;
    public float cooldownDash = 0.5f;
    private bool enDash = false;
    private bool puedeDash = true;
    private Vector2 direccionDash;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float velocidadX = Input.GetAxis("Horizontal");
        float velocidadY = Input.GetAxis("Vertical");

        // GIRAR
        if (velocidadX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (velocidadX > 0)
            transform.localScale = new Vector3(1, 1, 1);

        // DASH
        if (Input.GetKeyDown(KeyCode.Space) && !enDash && puedeDash)
        {
            StartCoroutine(Dash(new Vector2(velocidadX, velocidadY)));
        }

        // MOVIMIENTO NORMAL
        if (!enDash)
        {
            Vector2 velocidadMov = new Vector2(velocidadX * velocidad, velocidadY * velocidad);
            rb.velocity = velocidadMov;
        }
    }

    // CORUTINA DEL DASH
    private IEnumerator Dash(Vector2 dir)
    {
        enDash = true;
        puedeDash = false;
        direccionDash = dir.normalized;

        float tiempo = 0f;
        while (tiempo < duracionDash)
        {
            rb.velocity = direccionDash * distanciaDash / duracionDash;
            tiempo += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        enDash = false;

        StartCoroutine(CooldownDash());
    }

    // CORUTINA DEL COOLDOWN
    private IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(cooldownDash);
        puedeDash = true;
    }


}
