using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerMenu : MonoBehaviour
{
    public void CambioEscenaJuego()
    {
        SceneManager.LoadScene("Main");
    }

    public void CambioEscenaTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void CambioEscenaCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void CambioEscenaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
    public void QuitarJuego()
    {
        Application.Quit();
        Debug.Log("Se ha cerrado");
    }


}
