using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; // 1. IMPORTANTE: Adicione esta linha!

public class PermanentUI : MonoBehaviour
{
    // --- Suas Variáveis ---
    public int gems = 0;
    public TextMeshProUGUI gemText;
    public static PermanentUI perm;

    // --- Nova Variável ---
    private int totalGemsInLevel;

    // Awake é chamado antes de Start. É o melhor lugar para o padrão Singleton.
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

    // 2. DETECTAR MUDANÇA DE CENA
    // OnEnable é chamado quando o objeto se torna ativo
    void OnEnable()
    {
        // "Inscreve" a função OnSceneLoaded para ser chamada sempre que uma cena carregar
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // OnDisable é chamado quando o objeto é desativado/destruído
    void OnDisable()
    {
        // "Cancela a inscrição" para evitar erros
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 3. FUNÇÃO CHAMADA NO CARREGAMENTO DA CENA
    // Esta função será executada toda vez que uma nova cena for carregada.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Conta todos os coletáveis na NOVA cena
        totalGemsInLevel = GameObject.FindGameObjectsWithTag("Collectible").Length;

        // Zera a contagem de gemas para a nova fase e atualiza o texto
        ResetForNewLevel();
    }
    
    // 4. FUNÇÃO PARA ADICIONAR GEMA
    public void AddGem()
    {
        gems++;
        UpdateGemText();
    }
    
    // 5. ATUALIZA O TEXTO NO FORMATO CORRETO
    private void UpdateGemText()
    {
        if(gemText != null)
        {
            gemText.text = $"{gems} / {totalGemsInLevel}";
        }
    }
    
    // Modificamos sua função Reset para se adaptar ao novo sistema
    // Esta função zera a contagem da fase atual
    public void ResetForNewLevel()
    {
        gems = 0;
        UpdateGemText();
    }
}