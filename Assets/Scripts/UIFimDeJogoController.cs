using UnityEngine;
using TMPro; // Importante: Adicione esta linha para usar TextMeshPro

public class UIFimDeJogoController : MonoBehaviour
{
    [Header("Componentes da UI")]
    [SerializeField] private TextMeshProUGUI textoMortes;
    [SerializeField] private TextMeshProUGUI textoGemas;

    void Start()
    {
        // Garante que a instância do GameManager existe antes de tentar usá-la
        if (GameManager.Instance != null)
        {
            // Pega os valores finais do GameManager
            int mortesFinais = GameManager.Instance.totalMortes;
            int gemasFinais = GameManager.Instance.totalGemas;

            // Atualiza os textos na tela
            // O "D2" formata o número para ter sempre pelo menos 2 dígitos (ex: 01, 05, 12)
            textoMortes.text = " " + mortesFinais.ToString("D2");
            textoGemas.text = " " + gemasFinais.ToString("D2");
        }
        else
        {
            // Mensagem de erro caso o GameManager não seja encontrado
            Debug.LogError("GameManager não encontrado! As estatísticas não podem ser exibidas.");
            textoMortes.text = "--";
            textoGemas.text = "--";
        }
    }
}