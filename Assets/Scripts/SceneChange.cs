using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{

    [SerializeField] private string sceneName; //make sure target scene is in build settings

    [SerializeField] private Button btn;

    private void Start()
    {
        btn = GetComponent<Button>();
        if (btn != null)
            btn.onClick.AddListener(NewGame);
    }

    private void NewGame()
{
    // A linha foi removida. O PermanentUI já reseta sozinho ao carregar a nova cena.
    SceneManager.LoadScene(sceneName);
}   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName); //reloads the current scene on death
        }
    }


    public void ChangeScenes()
    {
        SceneManager.LoadScene(sceneName);
    }
}