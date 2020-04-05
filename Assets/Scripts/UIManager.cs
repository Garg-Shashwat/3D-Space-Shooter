using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    int[] lives = new int[2];
    int score = 0;
    
    [SerializeField]
    Text Score, GameOver, RestartImg;
    [SerializeField]
    Sprite[] LivesSprites;
    [SerializeField]
    Image[] ActiveLife;

    GameManager gameManager;
    HighscoreManager highscoreManager;
    void Start()
    {
        lives[0] = lives[1] = 3;
        Score.text = "Score : " + 0;
        RestartImg.gameObject.SetActive(false);
        gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        highscoreManager = GameObject.Find("Canvas").GetComponent<HighscoreManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager cannot be attached");
        }
        else if (!gameManager.isCoop)
            lives[1] = 0;
        if (highscoreManager == null)
        {
            Debug.LogError("HighScoreManager cannot be attached");
        }
    }

    public void UpdateScore(int add)
    {
        if (lives[0] != 0 || lives[1] != 0)
        {
            score += add;
            Score.text = "Score : " + score.ToString();
        }
    }

    public void UpdateLives(int activelives, int player)
    {
        lives[player] = activelives;
        ActiveLife[player].sprite = LivesSprites[activelives];
        if (lives[0] == 0 && lives[1] == 0)
        {
            StartCoroutine(GameOverScreen());
            RestartImg.gameObject.SetActive(true);
            gameManager.GameOver();
            Debug.Log(score);
            if (gameManager.isCoop)
                highscoreManager.AddScore(score, 1);
            else
                highscoreManager.AddScore(score, 0);
        }
    }

    IEnumerator GameOverScreen()
    {
        while (true)
        {
            GameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            GameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.6f);
        }
    }
}