using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isCoop = false;
    public bool isGameOver = false;
    bool isReady = false;

    int menudepth = 0;

    [SerializeField]
    GameObject PauseMenuUI, OptionsMenuUI, QuitMenuUI, RestartImg, Canvas;

    private void Start()
    {
        StartCoroutine(start());
    }
    private void Update()
    {
        if(isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && isReady)
        {
            if (menudepth == 0)
                Pause();
            else if (menudepth == 1)
                Resume();
            else if(OptionsMenuUI.activeInHierarchy == true)
            {
                OptionsMenuUI.SetActive(false);
                PauseMenuUI.SetActive(true);
                menudepth--;
            }
            else if (QuitMenuUI.activeInHierarchy == true)
            {
                QuitMenuUI.SetActive(false);
                PauseMenuUI.SetActive(true);
                menudepth--;
            }
        }
    }
    IEnumerator start()
    {
        yield return new WaitForSeconds(1.1f);
        isReady = true;
    }
    public void GameOver()
    {
        isGameOver = true;
    }
    public void Resume()
    {
        if (isGameOver)
            RestartImg.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        menudepth--;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(SceneManager.GetActiveScene().buildIndex));
    }
    void Pause()
    {
        PauseMenuUI.SetActive(true);
        RestartImg.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
        menudepth++;
    }
    public void Back()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(1));
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void addmenudepth(int i)
    {
        menudepth += i;
    }
    public int depth()
    {
        return menudepth;
    }

    IEnumerator LoadScene(int index)
    {
        Canvas.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }
}
