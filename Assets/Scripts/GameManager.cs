using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events; // Necessário para usar UnityEvent

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Variáveis que persistem entre as cenas
    public bool tutorialJaExibido = false;
    public int totalMortes = 0; // <-- ADICIONADO: Contador de mortes

    // <-- ADICIONADO: Evento para notificar a UI quando as mortes mudarem
    public UnityEvent<int> OnMortesCountChanged;

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
        totalMortes++; // Incrementa o contador
        Debug.Log("Total de mortes: " + totalMortes);
        
        Debug.Log("GameManager: Chamada recebida! Registrando morte. Novo total: " + totalMortes);
    

        // Dispara o evento para avisar a UI para se atualizar
        OnMortesCountChanged?.Invoke(totalMortes);
    }

    public void ReiniciarFase()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}