using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Video.VideoClip clip;
    [SerializeField]
    Image logo;
    Color c;
    bool end;

    void Awake()
    {
        c = logo.color;
        c.a = 0;
        StartCoroutine(VideoEnd((float)clip.length));
    }

    // Update is called once per frame
    void Update()
    {
        if(end)
        {
            c.a += 0.01f;
            logo.color = c;
            Debug.Log(c.a);
            if (c.a >= 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    IEnumerator VideoEnd(float duration)
    {
        yield return new WaitForSeconds(duration + 0.5f);
        end = true;
    }
}
