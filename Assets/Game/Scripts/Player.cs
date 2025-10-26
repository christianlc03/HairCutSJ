using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // MOVIMIENTO
    [Header("Movimiento")]
    public float velocidad = 8f;
    private Rigidbody2D rb;

    // DASH
    [Header("Dash")]
    public float distanciaDash = 5f;
    public float duracionDash = 0.2f;
    public float cooldownDash = 0.5f;
    private bool enDash = false;
    private bool puedeDash = true;
    private Vector2 direccionDash;

    //VIDA
    [Header("Vida")]
    public float vidaMax = 5f;
    public float vidaActual = 5f;
    public Image barraVidaImagen;

    //ATAQUE ARANAZO
    [Header("Ataque aranazo")]
    public float rangoAtaque = 1.5f; 
    public float danoAtaque = 1f;    
    public Transform puntoAtaque;
    public string tagEnemigo = "Enemigo";

    //ATAQUE PELUSA AREA
    [Header("Ataque especial bola pelo")]
    public GameObject uiBolaDePeloActivado;    // UI que aparece cuando puedes lanzar
    public GameObject uiBolaDePeloDesactivado; // UI que aparece cuando NO puedes
    public GameObject prefabBolaPelo;          // Prefab que se lanza
    public Transform puntoInstanciacionBola;

    //DANO
    [Header("Dano")]
    public bool envenenado = false;

    //PELO
    [Header("Pelo")]
    public int peloActual = 0;
    public int maxPelo = 5;
    public Image barraPeloImagen;

    //PELO VISUAL
    [Header("Pelos visuales en sprite")]
    public GameObject[] pelosVisuales;

    //ANIMACIONES
    [Header("Animaciones")]
    private Animator anim;
    private bool andando;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        vidaActual = vidaMax;
        ActualizarBarraVida();
        ActualizarBarraPelo();
        ActualizarPelosVisuales();
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
            anim.SetTrigger("dash");
        }

        anim.SetBool("walk", andando);

        if (!(velocidadX == 0 && velocidadY == 0))
        {
            andando = true;
        }
        else
        {
            andando = false;
        }



        //CURACION
        if (Input.GetKeyDown(KeyCode.C))
            Curar();

        //LANZAR BOLA PELO
        if (Input.GetKeyDown(KeyCode.X) && peloActual >= maxPelo)
            LanzarBolaDePelo();

        //ARAÑAR
        if (Input.GetKeyDown(KeyCode.Z))
            Atacar();
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
        if (vidaActual <= 0) return;

        vidaActual -= 1;
        ActualizarBarraVida();

        ActualizarPelosVisuales();

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }
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

    //ATAQUE ARANAZO
    private void Atacar()
    {
        Collider2D[] objetosGolpeados = Physics2D.OverlapCircleAll(puntoAtaque.position, rangoAtaque);

        foreach (Collider2D col in objetosGolpeados)
        {
            if (col.CompareTag(tagEnemigo))
            {
                col.GetComponent<EnemyController>()?.MorirInstantaneo();
            }
        }
    }

    //CURACION
    public void Curar()
    {
        if (vidaActual < vidaMax)
        {
            int vidasRecuperadas = (int)(vidaMax - vidaActual);
            vidaActual = vidaMax;
            ActualizarBarraVida();

            for (int i = 0; i < pelosVisuales.Length; i++)
            {
                pelosVisuales[i].SetActive(false);
            }

            peloActual += vidasRecuperadas;
            if (peloActual > maxPelo) peloActual = maxPelo;
            ActualizarBarraPelo();
        }
    }

     

    //BARRA VIDA
    private void ActualizarBarraVida()
    {
        if (barraVidaImagen != null)
        {
            barraVidaImagen.fillAmount = Mathf.Lerp(0, 1, ((float)vidaActual) / vidaMax); 
        }
    }

    //BARRA PELO
    private void ActualizarBarraPelo()
    {
        if (barraPeloImagen != null)
        {
            barraPeloImagen.fillAmount = (float)peloActual / maxPelo;
        }

        // Activar/desactivar indicador de ataque listo
        if (peloActual >= maxPelo)
        {
            if (uiBolaDePeloActivado != null) uiBolaDePeloActivado.SetActive(true);
            if (uiBolaDePeloDesactivado != null) uiBolaDePeloDesactivado.SetActive(false);
        }
        else
        {
            if (uiBolaDePeloActivado != null) uiBolaDePeloActivado.SetActive(false);
            if (uiBolaDePeloDesactivado != null) uiBolaDePeloDesactivado.SetActive(true);
        }
    }
    private void ActualizarPelosVisuales()
    {
        int pelusasActivas = (int)(vidaMax - vidaActual);

        for (int i = 0; i < pelosVisuales.Length; i++)
        {
            pelosVisuales[i].SetActive(i < pelusasActivas);
        }
    }

    //BOTON LIMPIEZA
    private void limpieza()
    {
        if (vidaActual != vidaMax)
        {
            vidaActual = vidaMax;
        }
    }

    //LANZAR BOLA AREA
    private void LanzarBolaDePelo()
    {
        Instantiate(prefabBolaPelo, puntoInstanciacionBola.position, Quaternion.identity);
        peloActual = 0;
        ActualizarBarraPelo();
    }


    //GIZMO
    private void OnDrawGizmosSelected()
    {
        if (puntoAtaque == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(puntoAtaque.position, rangoAtaque);
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

    public void Desaparecer()
    {
        anim.SetTrigger("invisible");
    }
}
