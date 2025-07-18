using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 0; // Plataforma começa sem gravidade
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.relativeVelocity.y < 0)
            {
                // Permite o pulo do player
                collision.gameObject.GetComponent<PlayerJump>().pular();

                // Ativa a gravidade e destrói a plataforma após 2 segundos
                if (rb != null)
                {
                    rb.gravityScale = 1; // Ativa a gravidade
                    Destroy(gameObject, 2f); // Destroi após 2 segundos
                }
            }
        }
    }
}
