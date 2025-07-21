using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Variáveis que persistem entre as cenas
    public bool tutorialJaExibido = false;
    public int totalMortes = 0;
    public int totalGemas = 0; // <-- ADICIONADO: Contador de gemas

    // Eventos para notificar a UI quando os valores mudarem
    public UnityEvent<int> OnMortesCountChanged;
    public UnityEvent<int> OnGemasCountChanged; // <-- ADICIONADO

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Função chamada pelo PlayerController ao morrer
    public void RegistrarMorte()
    {
        totalMortes++;
        Debug.Log("Total de mortes: " + totalMortes);
        OnMortesCountChanged?.Invoke(totalMortes);
    }

    // <-- ADICIONADO: Nova função para ser chamada ao coletar uma gema -->
    public void RegistrarGema(int quantidade = 1)
    {
        totalGemas += quantidade;
        Debug.Log("Total de gemas: " + totalGemas);
        OnGemasCountChanged?.Invoke(totalGemas);
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}