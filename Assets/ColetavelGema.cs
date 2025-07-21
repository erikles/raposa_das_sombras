using UnityEngine;

public class ColetavelGema : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu é o jogador (pela tag "Player")
        if (other.CompareTag("Player"))
        {
            // Tenta encontrar o GameManager e registrar a gema
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegistrarGema();
            }
            // Destrói o objeto da gema
            Destroy(gameObject);
        }
    }
}