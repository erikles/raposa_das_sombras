using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerJump : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 15f;

    [Header("Components")]
    [SerializeField] private AudioSource jumpAudio;
    [SerializeField] private AudioSource gemAudio;
    
    [Header("Game Control")]
    [SerializeField] private GeneratePlatform platformGenerator;
    [SerializeField] private GameObject CanvasGameOver;
    [SerializeField] private float startDelay = 1.5f; // NOVO: Controle o atraso pelo Inspector!
    
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isGameStarted = false;
    private float originalGravityScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        transform.position = platformGenerator.startPlatformPosition + new Vector3(0, 0.5f, 0);

        // MELHORIA: Chama o início automático com o atraso definido no Inspector.
        Invoke(nameof(StartGame), startDelay); 
    }

    void Update()
    {
        // Se o jogo não começou, não permite controle.
        if (!isGameStarted)
        {
            return; 
        }

        // Lógica de movimento (só roda depois do jogo começar)
        float moveHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        UpdateSpriteDirection(moveHorizontal);
        animator.SetBool("run", moveHorizontal != 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pular();
        }

        if (rb.velocity.y < -0.5f)
        {
            animator.SetTrigger("Caindo");
            animator.SetBool("Idle", false);
        }
    }
    
    void StartGame()
    {
        isGameStarted = true;
        rb.gravityScale = originalGravityScale;
        pular(); // O primeiro pulo automático!
    }

    public void pular()
    {
        jumpAudio.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        animator.SetTrigger("Pular");
    }

    private void UpdateSpriteDirection(float moveHorizontal)
    {
        if (moveHorizontal > 0)
            spriteRenderer.flipX = false;
        else if (moveHorizontal < 0)
            spriteRenderer.flipX = true;
    }

    // ... (O resto das suas funções: OnTriggerEnter2D, OnCollisionEnter2D, etc., continuam iguais) ...
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver") || collision.gameObject.CompareTag("BalaInimigo"))
        {
            HandleGameOver();
        }
        else if (collision.tag == "Collectible")
        {
            gemAudio.Play();
            Destroy(collision.gameObject);
            PermanentUI.perm.AddGem();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Win"))
        {
            HandleWin();
        }
    }

    void HandleGameOver()
    {
        Debug.Log("Game Over");
        animator.SetTrigger("GameOver");
        rb.simulated = false;
        rb.velocity = Vector2.zero;
        ClearEnemiesAndBullets();
        StartCoroutine(ReloadSceneAfterDelay(2f));
    }

   void HandleWin()
{
    Debug.Log("Win");
    // Agora o jogador não vai mais parar. Ele ainda pode andar e pular.
    
    animator.SetBool("Idle", true); // Podemos manter isso para ele voltar à pose ociosa.
    ClearEnemiesAndBullets(); // É bom manter isso para limpar a tela.

    // Opcional: Adicione aqui o que deve acontecer ao vencer.
    // Exemplo: Ativar um painel de vitória.
    // if (painelDeVitoria != null)
    // {
    //     painelDeVitoria.SetActive(true);
    // }
}
    void ClearEnemiesAndBullets()
    {
        foreach (GameObject inimigo in GameObject.FindGameObjectsWithTag("Inimigo"))
        {
            Destroy(inimigo);
        }
        foreach (GameObject bala in GameObject.FindGameObjectsWithTag("BalaInimigo"))
        {
            Destroy(bala);
        }
    }

    private System.Collections.IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}