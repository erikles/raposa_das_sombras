using UnityEngine;
using UnityEngine.SceneManagement;

public class Bala : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float tempoDeVida;
    [SerializeField] bool inimigo = false;

    private Vector3 direcao; // Direção da bala

    void Start()
    {
        Destroy(gameObject, tempoDeVida);

        if (inimigo)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                direcao = (player.transform.position - transform.position).normalized;
            }
            else
            {
                direcao = Vector3.down; // fallback
            }
        }
        else
        {
            direcao = transform.up;
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(direcao * speed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (inimigo && collision.CompareTag("Player"))
        {
            // Dano ao jogador
            Destroy(gameObject);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (collision.CompareTag("Ground"))
        {
            // Destroi a bala ao colidir com o chão
            Destroy(gameObject);
        }
        if (!inimigo && collision.CompareTag("BalaInimigo"))
        {
            // Destroi a bala ao colidir com o inimigo
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
