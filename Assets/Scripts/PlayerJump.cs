using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    Rigidbody2D rb;
    [SerializeField] float jumpForce = 500f;
    Animator animator;
    SpriteRenderer spriteRenderer; // Adicione esta linha

    [SerializeField] private GameObject CanvasGameOver;
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource gemAudio;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Adicione esta linha
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        gameObject.transform.Translate(moveHorizontal * speed * Time.deltaTime, 0, 0);

        // Espelha o sprite dependendo da direção
        if (moveHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (moveHorizontal < 0)
            spriteRenderer.flipX = true;

        animator.SetBool("run", moveHorizontal != 0);

        // Detecta se está caindo
        if (rb.velocity.y < -0.1f)
        {
            animator.SetTrigger("Caindo");
            animator.SetBool("Idle", false); 
        }
    }

    
    public void pular()
    {
        jumpAudio.Play();
        rb.AddForce(Vector2.up * jumpForce);
        animator.SetTrigger("Pular"); // Inicia a animação de pulo
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("BalaInimigo"))
        {
            Debug.Log("Game Over");
            animator.SetTrigger("GameOver");
            rb.gravityScale = 0;
            //CanvasGameOver.SetActive(true);
            rb.velocity = Vector2.zero;

            // Destroi todos os inimigos
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            foreach (GameObject inimigo in inimigos)
            {
                Destroy(inimigo);
            }

            // Destroi todas as balas do inimigo
            GameObject[] balas = GameObject.FindGameObjectsWithTag("BalaInimigo");
            foreach (GameObject bala in balas)
            {
                Destroy(bala);
            }

            StartCoroutine(PausarJogoAposDelay(2f));
        }
        else if (collision.tag == "Collectible")
        {
            gemAudio.Play(); // O som toca
            Destroy(collision.gameObject); // O coletável some
            
            // Apenas chame esta função. O PermanentUI faz o resto.
            PermanentUI.perm.AddGem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            Debug.Log("Win");
            animator.SetBool("Idle", true);

            // Destroi todos os inimigos
            GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            foreach (GameObject inimigo in inimigos)
            {
                Destroy(inimigo);
            }

            // Destroi todas as balas do inimigo
            GameObject[] balas = GameObject.FindGameObjectsWithTag("BalaInimigo");
            foreach (GameObject bala in balas)
            {
                Destroy(bala);
            }
            // Adicione aqui outras ações de vitória, se necessário
        }
    }

    private System.Collections.IEnumerator PausarJogoAposDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
