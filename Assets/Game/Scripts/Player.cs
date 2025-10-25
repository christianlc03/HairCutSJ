using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    //VIDA
    public float vidaMax = 100f;
    public float vidaActual = 100f;
    public Image barraVidaImagen;

    //DANO
    public bool envenenado = false;

    //PELO
    public int peloActual = 0;
    public int maxPelo = 20;
    public Image barraPeloImagen;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaActual = vidaMax;
        ActualizarBarraVida();
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

        // MOVIMIENTO NORMAL
        if (!enDash)
        {
            Vector2 velocidadMov = new Vector2(velocidadX * velocidad, velocidadY * velocidad);
            rb.velocity = velocidadMov;
        }

        // DASH
        if (Input.GetKeyDown(KeyCode.Space) && !enDash && puedeDash)
        {
            StartCoroutine(Dash(new Vector2(velocidadX, velocidadY)));
        }
    }


    // CO RUTINA DEL DASH
    public IEnumerator Dash(Vector2 dir)
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

    // CO RUTINA DEL COOLDOWN
    private IEnumerator CooldownDash()
    {
        yield return new WaitForSeconds(cooldownDash);
        puedeDash = true;
    }

    //RECIBIR DANO ARANAZO
    public void RecibirDanoAranazo()
    {
        vidaActual -= 7;
        ActualizarBarraVida();

        peloActual += 2;
        ActualizarBarraPelo();

        if (vidaActual < 0)
        {
            vidaActual = 0;
            ActualizarBarraVida();
        }

        if (vidaActual <= 0)
        {
            Morir();
        }

        peloActual += 2;
        ActualizarBarraPelo();

        if (peloActual > maxPelo)
        {
            peloActual = maxPelo;
        }

        Debug.Log("Pelo acumulado: " + peloActual);
    }

    // RECIBIR DAÑO POR VENENO
    public void ActivarVeneno(float danoPorSegundo, float duracion)
    {
        if (!envenenado)
        {
            StartCoroutine(DanoPorVeneno(danoPorSegundo, duracion));
        }
    }

    private IEnumerator DanoPorVeneno(float dano, float duracion)
    {
        envenenado = true;
        float tiempo = 0f;

        while (tiempo < duracion)
        {
            vidaActual -= dano;
            if (vidaActual <= 0)
            {
                vidaActual = 0;
                ActualizarBarraVida();
                Morir();
                yield break;
            }

            ActualizarBarraVida();
            tiempo += 1f;
            yield return new WaitForSeconds(1f);
        }

        envenenado = false;
    }

    //CURACION
    public void Curar(float cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMax)
        {
            vidaActual = vidaMax;
            ActualizarBarraVida();
        }
    }

    //BARRA VIDA
    private void ActualizarBarraVida()
    {
        if (barraVidaImagen != null)
        {
            barraVidaImagen.fillAmount = vidaActual / vidaMax;
        }
    }

    //BARRA PELO
    private void ActualizarBarraPelo()
    {
        if (barraVidaImagen != null)
        {
            barraPeloImagen.fillAmount = Mathf.Lerp(0,1,((float) peloActual)/ maxPelo);
        }
    }

    //MUERTE
    private void Morir()
    {
        rb.velocity = Vector2.zero;
        enDash = false;
        puedeDash = false;

        /*if (animator != null)
        {
            animator.SetTrigger("Morir"); */

        //float duracionAnimacion = 1.5f; 
        //StartCoroutine(CambiarEscenaDespues(duracionAnimacion));
    }

   /* private IEnumerator CambiarEscenaDespues(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        SceneManager.LoadScene("GameOver");
    }*/
}
