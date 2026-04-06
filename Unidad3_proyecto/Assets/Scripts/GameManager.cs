using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Monedas")]
    public int monedas = 0;
    public TextMeshProUGUI textoMonedas;

    FirebaseFirestore db;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        Debug.Log("Firestore listo");
        CargarMonedas();
      }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Busca el texto en la nueva escena usando tag
        GameObject obj = GameObject.FindWithTag("TextoMonedas");

        if (obj != null)
        {
            textoMonedas = obj.GetComponent<TextMeshProUGUI>();
            ActualizarMonedasUI();
        }
    }

    public void SumarMoneda(int cantidad)
    {
        monedas += cantidad;
        ActualizarMonedasUI();
        GuardarMonedas();
    }
    void ActualizarMonedasUI()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = "x" + monedas.ToString(); 
        }
    }

    void GuardarMonedas()
    {
        DocumentReference docRef = db.Collection("jugadores").Document("player1");
        Dictionary<string, object> datos = new Dictionary<string, object>()
        {
            { "monedas", monedas }
        };

        docRef.SetAsync(datos).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Monedas guardadas en Firestore ");
            }
            else
            {
                Debug.LogError("Error al guardar en Firestore ");
            }
        });
    }
    void CargarMonedas()
    {
        var docRef = db.Collection("jugadores").Document("player1");

        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            if (!task.IsCompleted || !task.Result.Exists) return;

            monedas = task.Result.GetValue<int>("monedas");
            ActualizarMonedasUI();
        });
    }
}
