using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] public bool move = false; // Flag para controlar se a plataforma se move
    [SerializeField] private float moveDistance = 4f; // Distância de movimento
    [SerializeField] private float moveSpeed = 2f;    // Velocidade de movimento

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = 0; // Plataforma começa sem gravidade

        startPos = transform.position;
        targetPos = startPos + Vector3.right * moveDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            // Move entre startPos e targetPos
            float step = moveSpeed * Time.deltaTime;
            if (movingRight)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, step);
                if (Vector3.Distance(transform.position, targetPos) < 0.01f)
                    movingRight = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, startPos, step);
                if (Vector3.Distance(transform.position, startPos) < 0.01f)
                    movingRight = true;
            }
        }
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
