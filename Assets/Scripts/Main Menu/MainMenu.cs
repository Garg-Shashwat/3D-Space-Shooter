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
        if (PlayerPrefs.HasKey("Active_Profile"))
            activeprofile = PlayerPrefs.GetInt("Active_Profile");
        else
            activeprofile = 0;
        if (PlayerPrefs.HasKey("Profile_Count"))
            profilecount = PlayerPrefs.GetInt("Profile_Count");
        else
            profilecount = 0;
        RefreshProfiles();
    }
    public void LoadSinglePlayer()
    {
        StartCoroutine(LoadScenes(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadCoOp()
    {
        StartCoroutine(LoadScenes(SceneManager.GetActiveScene().buildIndex + 2));
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
        if (PlayerPrefs.GetInt("Profile_Count") > 0)
        { 
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
            newprofilepanel.SetActive(true);
            cancelprofile.SetActive(false);
        }
    }

    public void OnValueChange()
    {
        activeprofile = profiledropdown.value;
        PlayerPrefs.SetInt("Active_Profile", activeprofile);
    }
    public void AddProfile()
    {
        if (newprofilename.text != "")
        {
            PlayerPrefs.SetString("Profile" + profilecount, newprofilename.text);
            newprofilename.text = "";
            profilecount++;
            PlayerPrefs.SetInt("Profile_Count", profilecount);
            RefreshProfiles();
            newprofilepanel.SetActive(false);
            mainmenupanel.SetActive(true);
            cancelprofile.SetActive(true);
        }
        else
            StartCoroutine(Nullname());
    }

    public void DeleteProfile()
    {
        for (int i = activeprofile; i < profilecount - 1; i++)
        {
            PlayerPrefs.SetString("Profile" + i, PlayerPrefs.GetString("Profile" + (i + 1)));
        }
        profilecount--;
        PlayerPrefs.SetInt("Profile_Count", profilecount);
        RefreshProfiles();
    }

    IEnumerator Nullname()
    {
        nullnametext.SetActive(true);
        yield return new WaitForSeconds(2f);
        nullnametext.SetActive(false);
    }
}
