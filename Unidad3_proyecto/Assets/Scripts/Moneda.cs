using UnityEngine;

public class Moneda : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int valor = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarMoneda(valor);
            Destroy(gameObject);
        }
    }
}
