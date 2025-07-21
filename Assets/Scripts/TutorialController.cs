using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialController : MonoBehaviour
{
    [Header("Componentes da UI")]
    // Adicionamos as referências para o painel da história e seu botão
    [SerializeField] private GameObject painelHistoria; 
    [SerializeField] private Button botaoSeguir; 
    
    [SerializeField] private GameObject painelTutorial;
    [SerializeField] private Button botaoComecar;

    [Header("Eventos")]
    public UnityEvent OnGameStarted;

    void Start()
    {
        // A lógica principal de verificação continua a mesma
        if (GameManager.Instance != null && GameManager.Instance.tutorialJaExibido)
        {
            // Se já viu o tutorial, esconde tudo e começa o jogo. Perfeito, sem alterações aqui.
            painelHistoria.SetActive(false);
            painelTutorial.SetActive(false);
            Time.timeScale = 1f;
            OnGameStarted?.Invoke();
        }
        else
        {
            // Se for a primeira vez, a lógica agora começa pela HISTÓRIA.
            
            // 1. Mostra o painel da história e garante que o do tutorial está escondido.
            painelHistoria.SetActive(true);
            painelTutorial.SetActive(false);
            
            // 2. Pausa o jogo.
            Time.timeScale = 0f;

            // 3. Adiciona os listeners (ouvintes) para os botões.
            if (botaoSeguir != null)
            {
                // O botão "Seguir" vai chamar a função para mostrar o tutorial.
                botaoSeguir.onClick.AddListener(MostrarTutorial);
            }
            if (botaoComecar != null)
            {
                // O botão "Começar" continua chamando a função para iniciar o jogo.
                botaoComecar.onClick.AddListener(StartGame);
            }
        }
    }

    // NOVA FUNÇÃO: Chamada pelo "botaoSeguir"
    public void MostrarTutorial()
    {
        // Esconde a história e mostra o tutorial. O jogo continua pausado.
        painelHistoria.SetActive(false);
        painelTutorial.SetActive(true);
    }

    // FUNÇÃO EXISTENTE: Chamada pelo "botaoComecar"
    public void StartGame()
    {
        painelTutorial.SetActive(false); // Esconde o painel do tutorial
        Time.timeScale = 1f; // Retoma o tempo do jogo
        OnGameStarted?.Invoke();

        // Avisa ao GameManager que o tutorial foi concluído.
        if (GameManager.Instance != null)
        {
            GameManager.Instance.tutorialJaExibido = true;
        }
    }

    // É uma boa prática remover os listeners quando o objeto for destruído.
    private void OnDestroy()
    {
        if (botaoSeguir != null)
        {
            botaoSeguir.onClick.RemoveListener(MostrarTutorial);
        }
        if (botaoComecar != null)
        {
            botaoComecar.onClick.RemoveListener(StartGame);
        }
    }
}