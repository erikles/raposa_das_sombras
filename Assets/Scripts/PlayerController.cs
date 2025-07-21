using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D coll;
    private LayerMask ground;
    private LayerMask enemyLayer;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource gemAudio;
    [SerializeField] private AudioSource jumpAudio;

    private enum State {idle, running, jumping, falling, hurt};
    private State state = State.idle;
    private int speed = 9;
    private float jumpForce = 25f;
    private float airControl = 0.8f;
    private float hurtForce = 10f;

    public int quantidadeColetaveis = 0;

    // <-- ADICIONADO: Variável para controlar se o jogador pode se mover
    private bool canControl = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        ground = LayerMask.GetMask("Ground");
        enemyLayer = LayerMask.GetMask("EnemyLayer");
    }

    private void Update()
    {
        // <-- ALTERADO: O movimento só é processado se canControl for verdadeiro
        if (state != State.hurt && canControl)
        {
            Movement();
        } 
      
        animator.SetInteger("state", (int)state);
        AnimationState();
    }
    
    // <-- ADICIONADO: Função pública que será chamada pelo evento do Tutorial
    public void ActivatePlayer()
    {
        canControl = true;
        Debug.Log("Controle do jogador ativado!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Collectible")
    {
        // DEBUG: Vamos ver no console se a colisão está acontecendo.
        Debug.Log("Gema coletada!");
        GameManager.Instance.RegistrarGema();
        quantidadeColetaveis++;
        gemAudio.Play();
        Destroy(collision.gameObject);
        
        // CORREÇÃO: Descomente esta linha para que a UI seja atualizada.
        PermanentUI.perm.AddGem(); 
    }
        if (collision.tag == "Enemy")
        {
            state = State.hurt;
            if (collision.gameObject.transform.position.x > transform.position.x) 
            {
                rb.velocity = new Vector2(-hurtForce, hurtForce);
            }
            else
            {
                rb.velocity = new Vector2(hurtForce, hurtForce);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, 1.3f, enemyLayer);
            if (hit.collider != null || state == State.falling)
            {
                // ... (lógica de pular no inimigo)
            }
            else
            {
                // <-- LÓGICA DE MORTE ALTERADA AQUI -->
                // Em vez de apenas tomar dano, agora vamos reiniciar a fase.
                // Você pode adicionar um som de morte ou uma animação aqui antes de reiniciar.
                state = State.hurt; // Mantém a animação de dano se quiser
                Debug.Log("Jogador Morreu! Reiniciando a fase...");

                GameManager.Instance.RegistrarMorte(); 
                PermanentUI.perm.AddDeath();

                // Desativa o controle para evitar movimentos estranhos antes de reiniciar
                canControl = false; 

                // Chama a função de reiniciar do nosso GameManager
                GameManager.Instance.ReiniciarFase();
            }
        }
        if(other.gameObject.tag == "Enemy") 
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            
            RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, 1.3f, enemyLayer);
            if (hit.collider != null || state == State.falling)
            {
                state = State.falling;
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.hurt;
                if (other.gameObject.transform.position.x > transform.position.x) 
                {
                    rb.velocity = new Vector2(-hurtForce, hurtForce);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, hurtForce);
                }
            }
        }
    }
    
    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        bool jumping = Input.GetButtonDown("Jump");
        bool isTouchingGround = coll.IsTouchingLayers(ground);

        // A verificação `if (state != State.hurt)` foi movida para o Update para englobar todo o método.
        if (hDirection < 0 && !isTouchingGround)
        {
            rb.velocity = new Vector2(-speed * airControl, rb.velocity.y);
        }
        else if (hDirection > 0 && !isTouchingGround)
        {
            rb.velocity = new Vector2(speed * airControl, rb.velocity.y);
        }
        else if (hDirection < 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
        else if (hDirection > 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (hDirection == 0 && isTouchingGround)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
        if (jumping)
        {
            RaycastHit2D hit = Physics2D.Raycast(coll.bounds.center, Vector2.down, 0.8f, ground);
            if (hit.collider != null)
                Jump();
        }

        if (hDirection < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (hDirection > 0) // Adicionado 'else if' para não reverter a escala se o input for 0
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void Jump()
    {
        jumpAudio.Play();
        rb.velocity = new Vector2(rb.velocity.x / 2, jumpForce);
        state = State.jumping;
    }

    private void AnimationState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }
        else if (state == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurt)
        {
            if (Mathf.Abs(rb.velocity.x) < 2f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 2f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }

    private void Footstep()
    {
        footstep.Play();
    }
}