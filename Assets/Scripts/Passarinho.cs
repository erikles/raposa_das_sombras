using UnityEngine;

public class Passarinho : MonoBehaviour
{
    private Rigidbody2D rb;
    
    // AUMENTE ESTE VALOR! Se estava 5, tente 15 ou 20.
    [SerializeField] private float forcaVoo = 25f; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        //rb.gravityScale = 0.3f;
        rb.simulated = true;
        rb.freezeRotation = true;
    }

    public void Voar()
    {
        Debug.Log("PASSARINHO " + this.gameObject.name + " RECEBEU A ORDEM PARA VOAR!");
        rb.gravityScale = 0;

        float randomX = Random.Range(-8f, 8f);
        // Move o passarinho suavemente para cima 2 unidades em Y
        StartCoroutine(SubirSuavemente(3f, 0.5f, randomX)); 

        Destroy(this.gameObject, 10f);
    }

    private System.Collections.IEnumerator SubirSuavemente(float distanciaY, float duracao, float targetX)
    {
        Vector3 posInicial = transform.position;
        // Define a posição final com a distânciaY no Y e o targetX no X
        Vector3 posFinal = new Vector3(targetX, posInicial.y + distanciaY, posInicial.z); 
        float tempo = 0f;

        while (tempo < duracao)
        {
            transform.position = Vector3.Lerp(posInicial, posFinal, tempo / duracao);
            tempo += Time.deltaTime;
            yield return null;
        }
        transform.position = posFinal;
    }
}