using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject pauseButton;

    public GameObject panel;

    public GameObject playButton;

    public GameObject replayButton;

    public GameObject returnToMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pausa()
    {
        Time.timeScale = 0;

        pauseButton.SetActive(false);

        playButton.SetActive(true);

        replayButton.SetActive(true);

        returnToMenu.SetActive(true);

        panel.SetActive(true);
    }

    public void Jugar()
    {
        Time.timeScale = 1;

        pauseButton.SetActive(true);

        playButton.SetActive(false);

        replayButton.SetActive(false);

        returnToMenu.SetActive(false);

        panel.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1;

        panel.SetActive(false);

        pauseButton.SetActive(true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Volver()
    {
        Time.timeScale = 1;

        //panel.SetActive(false);

        //pauseButton.SetActive(true);

        //playButton.SetActive(true);

        //replayButton.SetActive(false);

        //returnToMenu.SetActive(false);

        SceneManager.LoadScene("Menu");
    }
}
