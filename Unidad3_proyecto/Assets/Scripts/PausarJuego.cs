using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PausarJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject btnReanudar;
    public bool juegoPausado = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Reanudar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        btnReanudar.SetActive(true);
        Time.timeScale = 1;
        juegoPausado = false;
    }
    public void Pausar()
    {
        menuPausa.SetActive(true);
        btnReanudar.SetActive(false);
        Time.timeScale = 0;
        juegoPausado = true;
    }

    public void IrAlMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void IrAlSelector()
    {
        SceneManager.LoadScene("Niveles");
    }
}
