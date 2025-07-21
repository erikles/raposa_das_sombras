using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PermanentUI : MonoBehaviour
{
    public static PermanentUI perm;

    [Header("Contadores")]
    public int gems = 0;
    public int deaths = 0;

    [Header("Referências da UI")]
    [SerializeField] private TextMeshProUGUI gemText;
    [SerializeField] private TextMeshProUGUI deathText;

    private int totalGemsInLevel;

    private void Awake()
    {
        if (perm == null)
        {
            perm = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("--- NOVA CENA CARREGADA: " + scene.name + " ---");
        
        // Procura e se reconecta aos elementos da UI
        FindAndConnectUI();
        
        // Reseta as gemas e atualiza todos os contadores
        CountGemsInScene();
        UpdateDeathText();
    }
    
    // NOVO: Função central para encontrar a UI.
    private void FindAndConnectUI()
    {
        GameObject gemTextObject = GameObject.FindWithTag("GemCounterText");
        if (gemTextObject != null)
        {
            gemText = gemTextObject.GetComponent<TextMeshProUGUI>();
            Debug.Log("SUCESSO: Texto de Gemas reconectado!");
        }
        else
        {
            Debug.LogWarning("FALHA: Não foi possível encontrar o objeto com a tag 'GemCounterText'. Verifique a tag e se o objeto está ativo.");
        }

        GameObject deathTextObject = GameObject.FindWithTag("DeathCounterText");
        if (deathTextObject != null)
        {
            deathText = deathTextObject.GetComponent<TextMeshProUGUI>();
            Debug.Log("SUCESSO: Texto de Mortes reconectado!");
        }
        else
        {
            Debug.LogWarning("FALHA: Não foi possível encontrar o objeto com a tag 'DeathCounterText'. Verifique a tag e se o objeto está ativo.");
        }
    }

    public void AddGem()
    {
        gems++;
        UpdateGemText();
    }
    
    public void AddDeath()
    {
        deaths++;
        Debug.Log("Morte adicionada! Total de mortes agora é: " + deaths);
        UpdateDeathText();
    }

    public void CountGemsInScene()
    {
        totalGemsInLevel = GameObject.FindGameObjectsWithTag("Collectible").Length;
        gems = 0;
        UpdateGemText();
    }
    
    private void UpdateGemText()
    {
        if(gemText != null)
        {
            gemText.text = $"{gems} / {totalGemsInLevel}";
        }
    }
    
    private void UpdateDeathText()
    {
        if(deathText != null)
        {
            deathText.text = deaths.ToString(); 
            Debug.Log("Atualizando texto de mortes na tela para: " + deaths);
        }
    }
}