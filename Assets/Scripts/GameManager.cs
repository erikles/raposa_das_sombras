using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // "Instância" é uma referência estática a si mesmo, permitindo que outros scripts o acessem facilmente.
    public static GameManager Instance { get; private set; }

    // Esta variável vai guardar o estado do tutorial.
    public bool tutorialJaExibido = false;

    private void Awake()
    {
        // Este é o padrão Singleton. Garante que apenas UMA instância do GameManager exista.
        if (Instance == null)
        {
            Instance = this;
            // Esta é a linha mágica: ela impede que este GameObject seja destruído ao carregar uma nova cena.
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            // Se outra instância já existir, esta é destruída para evitar duplicatas.
            Destroy(gameObject);
        }
    }

    // Função para reiniciar a cena atual.
    public void ReiniciarFase()
    {
        // Recarrega a cena que está ativa no momento.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}