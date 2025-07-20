using UnityEngine;

public class Passarinho : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // AUMENTE ESTE VALOR! Se estava 5, tente 15 ou 20.
    [SerializeField] private float forcaVoo = 25f; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; 
    }

    public void Voar()
    {
        Debug.Log("PASSARINHO " + this.gameObject.name + " RECEBEU A ORDEM PARA VOAR!");

        rb.isKinematic = false; 
        
        // Vamos diminuir um pouco a gravidade para um voo mais suave
        rb.gravityScale = 0.3f; 

        float direcaoX = Random.Range(-1f, 1f);
        Vector2 direcaoVoo = new Vector2(direcaoX, 1).normalized;
        
        rb.AddForce(direcaoVoo * forcaVoo, ForceMode2D.Impulse);

        Destroy(this.gameObject, 10f);
    }
}