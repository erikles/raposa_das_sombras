using System.Collections.Generic;
using UnityEngine;

public class GerenciadorGaiola : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject[] passarinhosPrefabs;
    [SerializeField] private Transform pontoDeSpawn;

    private List<GameObject> passarinhosNaGaiola = new List<GameObject>();
    private bool jaForamSoltos = false;
    
    // NOVO: Variável para guardar o total de coletáveis da fase
    private int totalDeColetaveisNaFase;

    void Start()
    {
        if (passarinhosPrefabs == null || passarinhosPrefabs.Length == 0)
        {
            Debug.LogError("ERRO: Nenhum prefab de passarinho foi adicionado ao GerenciadorGaiola!");
            return;
        }
        GerarPassarinhosIniciais();
    }

    private void GerarPassarinhosIniciais()
    {
        // NOVO: Guardamos o total de coletáveis na nossa nova variável
        totalDeColetaveisNaFase = GameObject.FindGameObjectsWithTag("Collectible").Length;
        
        Debug.Log("Fase iniciada com " + totalDeColetaveisNaFase + " coletáveis. Gerando pássaros...");

        for (int i = 0; i < totalDeColetaveisNaFase; i++)
        {
            int indiceAleatorio = Random.Range(0, passarinhosPrefabs.Length);
            GameObject prefabEscolhido = passarinhosPrefabs[indiceAleatorio];
            GameObject novoPassarinho = Instantiate(prefabEscolhido, pontoDeSpawn.position, Quaternion.identity);
            novoPassarinho.transform.SetParent(this.transform);
            passarinhosNaGaiola.Add(novoPassarinho);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!jaForamSoltos && other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                int numeroParaSoltar = player.quantidadeColetaveis;
                Debug.Log("Jogador tocou a gaiola com " + numeroParaSoltar + " itens. Soltando pássaros!");
                
                SoltarPassaros(numeroParaSoltar);
                jaForamSoltos = true;

                // --- LÓGICA DE DESTRUIÇÃO DA GAIOLA ---
                // NOVO: Verifica se o número de itens coletados é igual ao total da fase.
                if (numeroParaSoltar >= totalDeColetaveisNaFase)
                {
                    Debug.Log("Todos os " + totalDeColetaveisNaFase + " itens foram coletados! Destruindo a gaiola.");
                    // Destrói o objeto da gaiola.
                    Destroy(this.gameObject); 
                }
            }
        }
    }

    private void SoltarPassaros(int quantidade)
    {
        for (int i = 0; i < quantidade; i++)
        {
            if (passarinhosNaGaiola.Count > 0)
            {
                GameObject passarinhoParaSoltar = passarinhosNaGaiola[passarinhosNaGaiola.Count - 1];
                passarinhosNaGaiola.RemoveAt(passarinhosNaGaiola.Count - 1);
                passarinhoParaSoltar.transform.SetParent(null);
                passarinhoParaSoltar.GetComponent<Passarinho>().Voar();
            }
        }
    }
}