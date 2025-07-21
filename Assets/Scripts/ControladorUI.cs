using UnityEngine;
using UnityEngine.UI; // Lembre-se de adicionar esta linha para trabalhar com UI

public class ControladorUI : MonoBehaviour
{
    // Crie referências públicas para arrastar seus painéis no Inspector
    public GameObject painelHistoria;
    public GameObject painelTutorial;

    // A função Start é chamada uma vez quando o jogo começa.
    // Vamos garantir que o estado inicial esteja correto.
    void Start()
    {
        // Garante que o painel da história esteja visível e o do tutorial escondido.
        painelHistoria.SetActive(true);
        painelTutorial.SetActive(false);
    }

    // Esta será a função chamada pelo botão "Seguir"
    public void MostrarPainelTutorial()
    {
        // Desativa o painel da história
        painelHistoria.SetActive(false);

        // Ativa o painel do tutorial
        painelTutorial.SetActive(true);
    }

    // Você pode adicionar uma função para o botão "Iniciar" do tutorial também, se precisar
    public void IniciarJogo()
    {
        // Esconde o painel do tutorial
        painelTutorial.SetActive(false);

        // Aqui você colocaria a lógica para iniciar o jogo de fato
        // Ex: Time.timeScale = 1; carregar a cena do jogo, etc.
        Debug.Log("O jogo começou!");
    }
}