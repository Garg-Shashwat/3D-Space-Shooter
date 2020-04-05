using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text[] highscores = new TMP_Text[2];

    int newscore, oldscore, activeprofile;
    string newname, oldname;

   public void AddScore(int score, int gamemode)
    {
        string scorekey = "HScore" + gamemode;
        string namekey = "HScore_Name" + gamemode;
        newscore = score;
        Debug.Log(newscore);
        activeprofile = PlayerPrefs.GetInt("Active_Profile");
        Debug.Log(activeprofile);
        newname = PlayerPrefs.GetString("Profile" + activeprofile);
        Debug.Log(newname);
        for (int i = 0; i < 5; i++)
        {
            if (PlayerPrefs.HasKey(scorekey + i))
            {
                Debug.Log("Has Key " + i);
                if (PlayerPrefs.GetInt(scorekey + i) < newscore)
                {
                    Debug.Log("Score At Pos " + i);
                    oldscore = PlayerPrefs.GetInt(scorekey + i);
                    oldname = PlayerPrefs.GetString(namekey + i);
                    PlayerPrefs.SetInt(scorekey + i, newscore);
                    PlayerPrefs.SetString(namekey + i, newname);
                    newname = oldname;
                    newscore = oldscore;
                }
            }
            else
            {
                Debug.Log("Score Added" + i);
                PlayerPrefs.SetInt(scorekey + i, newscore);
                PlayerPrefs.SetString(namekey + i, newname);
                newscore = 0;
                newname = "";
            }
        }
    }
    public void ShowScore(int gamemode)
    {
        string scorekey = "HScore" + gamemode;
        string namekey = "HScore_Name" + gamemode;
        highscores[gamemode].text = "";
        for (int i = 0; i < 5; i++)
        {
            highscores[gamemode].text += "\t" + (i + 1) + ". " + PlayerPrefs.GetInt(scorekey + i) + " - ";
            highscores[gamemode].text += "\t" + PlayerPrefs.GetString(namekey + i) + " \n";
        }
    }
}
