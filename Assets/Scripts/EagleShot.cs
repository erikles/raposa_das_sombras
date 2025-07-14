using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleShot : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float radius = 12f;
    [SerializeField] private float lifetime = 5f; // Tempo de vida da bola

    private Rigidbody2D rb;
    private LayerMask playerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMask = LayerMask.GetMask("Player");

        // Encontra o jogador e se move em sua direção inicial
        Collider2D player = Physics2D.OverlapCircle(transform.position, radius, playerMask);
        if (player != null)
        {
            // Calcula a direção e aplica a velocidade
            Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
            rb.velocity = direction * speed;
        }

        // Destrói o objeto depois de um tempo, caso não atinja nada
        Destroy(this.gameObject, lifetime);
    }

    // Este método é chamado quando este objeto colide com outro
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (other.CompareTag("Player"))
        {
            // Destrói este objeto (a bola)
            Destroy(this.gameObject);
        }
    }
}