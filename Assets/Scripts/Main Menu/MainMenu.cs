using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject newprofilepanel, mainmenupanel, cancelprofile, nullnametext;

    [SerializeField]
    TMP_Dropdown profiledropdown;

    [SerializeField]
    TMP_InputField newprofilename;

    int profilecount, activeprofile;

    private void Start()
    {
        RefreshProfiles();
    }
    public void LoadSinglePlayer()
    {
        StartCoroutine(LoadScenes(1));
    }

    public void LoadCoOp()
    {
        StartCoroutine(LoadScenes(2));
    }

    IEnumerator LoadScenes(int index)
    {
        GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void RefreshProfiles()
    {
        if (PlayerPrefs.HasKey("Profile_Count"))
            if (PlayerPrefs.GetInt("Profile_Count") > 0)
            {
                profilecount = PlayerPrefs.GetInt("Profile_Count");
                List<string> profiles = new List<string>();
    
                for (int i = 0; i < profilecount; i++)
                {
                    string profile = PlayerPrefs.GetString("Profile" + i);
                    profiles.Add(profile);
                }
                profiledropdown.ClearOptions();
                profiledropdown.AddOptions(profiles);
                profiledropdown.value = activeprofile;
                profiledropdown.RefreshShownValue();
            }
        else
        {
            profilecount = 0;
            mainmenupanel.SetActive(false);
            newprofilepanel.SetActive(true);
            cancelprofile.SetActive(false);
        }
    }

    public void AddProfile()
    {
        if (newprofilename.text != "")
        {
            PlayerPrefs.SetString("Profile" + profilecount, newprofilename.text);
            profilecount++;
            PlayerPrefs.SetInt("Profile_Count", profilecount);
            RefreshProfiles();
            newprofilepanel.SetActive(false);
            mainmenupanel.SetActive(true);
        }
        else
            StartCoroutine(Nullname());
    }

    IEnumerator Nullname()
    {
        nullnametext.SetActive(true);
        yield return new WaitForSeconds(2f);
        nullnametext.SetActive(false);
    }
}
