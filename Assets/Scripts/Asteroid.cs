using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    float speed = 20f;
    [SerializeField]
    AudioClip ExplosionClip;
    [SerializeField]
    TMP_Text wave_text;

    Animator animator;
    AudioSource audiosource;
    SpawnManager spawnManager;
    void Start()
    {
        animator = GetComponent<Animator>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audiosource = GetComponent<AudioSource>();

        if (animator == null)
            Debug.LogError("Animator cannot be attached");
        
        if (spawnManager == null)
            Debug.LogError("Spawn Manager script is null");

        if (audiosource == null)
            Debug.LogError("Audio Source cannnot be attached");
        else
            audiosource.clip = ExplosionClip;
        wave_text.text = "Wave: " + spawnManager.GetWave();
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Laser")
        {
            Destroy(collider.gameObject);
            audiosource.Play();
            animator.SetTrigger("AsteroidDestroyed");
            spawnManager.StartWave();
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 2f);
        }
    }
}
