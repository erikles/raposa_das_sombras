using UnityEngine;
using TMPro; // Biblioteca para usar TextMeshPro

public class UIDeathCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoMortes; // Referência para o texto na tela

    void Start()
    {
        // Garante que o textoMortes foi atribuído no Inspector
        if (textoMortes == null)
        {
            Debug.LogError("A referência para o texto de mortes não foi configurada!", this);
            return;
        }

        // Registra este script para "escutar" o evento de mudança de mortes do GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMortesCountChanged.AddListener(AtualizarTextoMortes);
            
            // Atualiza o texto com o valor inicial assim que o jogo começa
            AtualizarTextoMortes(GameManager.Instance.totalMortes);
        }
    }

    // Esta função é chamada pelo evento do GameManager
    void AtualizarTextoMortes(int quantidade)
    {
        // Atualiza o texto na tela. Ex: "Mortes: 5"
        textoMortes.text = " " + quantidade;
    }

    // Boa prática: remove o listener quando o objeto for destruído
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMortesCountChanged.RemoveListener(AtualizarTextoMortes);
        }
    }
}