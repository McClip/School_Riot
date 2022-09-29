using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject backGround;

    public GameObject playButton;

    public GameObject webButton;

    public GameObject title;

    // Start is called before the first frame update
    void Start()
    {
        backGround.SetActive(false);

        title.SetActive(false);

        playButton.SetActive(false);

        webButton.SetActive(false);

        BackGround();

        Invoke(nameof(MainTitle), 2.5f);

        Invoke(nameof(ActiveGame), 5f);

        Invoke(nameof(ActiveWeb), 5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackGround()
    {
        backGround.SetActive(true);
    }

    public void MainTitle()
    {
        title.SetActive(true);
    }

    public void ActiveGame()
    {
        playButton.SetActive(true);
    }

    public void ActiveWeb()
    {
        webButton.SetActive(true);
    }

    public void PlayeGame()
    {
        SceneManager.LoadScene("Test");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void VisitOurPage()
    {
        Application.OpenURL("http://www.schoolriot.ga/");
    }
}
