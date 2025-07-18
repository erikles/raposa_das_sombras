using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManeger : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDejogo;

    public void Jogar (){
        SceneManager.LoadScene(nomeDoLevelDejogo);
    }

    public void SairJogo(){
        Debug.Log("SAIR DO JOGO");
        Application.Quit();
    }
}
