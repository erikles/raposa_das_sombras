using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [Header("Componentes da UI")]
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private Button botaoComecar;

    [Header("Eventos")]
    public UnityEvent OnGameStarted;

    void Start()
    {
        // <-- LÓGICA PRINCIPAL ALTERADA AQUI -->
        // Verifica no GameManager se o tutorial já foi exibido.
        if (GameManager.Instance != null && GameManager.Instance.tutorialJaExibido)
        {
            // Se já foi exibido, simplesmente inicia o jogo sem mostrar o painel.
            painelTutorial.SetActive(false);
            Time.timeScale = 1f; // Garante que o tempo está normal
            OnGameStarted?.Invoke();
        }
        else
        {
            // Se for a primeira vez, mostra o painel e pausa o jogo.
            painelTutorial.SetActive(true);
            Time.timeScale = 0f;
            if (botaoComecar != null)
            {
                botaoComecar.onClick.AddListener(StartGame);
            }
        }
    }

     public void StartGame()
    {
        painelTutorial.SetActive(false);
        Time.timeScale = 1f;
        OnGameStarted?.Invoke();

        // <-- LINHA CRÍTICA ADICIONADA -->
        // Avisa ao GameManager que o tutorial agora foi concluído.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.tutorialJaExibido = true;
        }
    }

    private void OnDestroy()
    {
        if (botaoComecar != null)
        {
            botaoComecar.onClick.RemoveListener(StartGame);
        }
    }
}