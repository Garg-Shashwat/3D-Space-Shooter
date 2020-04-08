using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float speed = 1.5f;
    [SerializeField]
    AudioClip ExplosionClip, LaserClip;
    [SerializeField]
    GameObject Laser;

    Vector3 position;
    UIManager uimanager;
    Animator animator;
    AudioSource audioSource;
    bool hit = false;

    void Awake()
    {
        uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (uimanager == null)
            Debug.LogError("Player Controller script is null");
        
        if (animator == null)
            Debug.LogError("Animator cannot be attached");

        if (audioSource == null)
            Debug.LogError("Audio Source cannot be attached");
        else
            audioSource.clip = ExplosionClip;
        StartCoroutine("Firing");
    }

    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            random();
        }
    }
    void random() 
    {
        position = new Vector3(Random.Range(-9f,9f),6.5f,0);
        transform.position = position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !hit)
        {
            uimanager.UpdateScore(5);
            PlayerController control = other.GetComponent<PlayerController>();
            if (control != null)
                control.ChangeHealth(-1);
            StartCoroutine("Disablecollider");
            StopCoroutine("Firing");
            hit = true;
        }
        if(other.tag == "Laser" && !hit)
        {
            Destroy(other.gameObject);
            uimanager.UpdateScore(10);
            StartCoroutine("Disablecollider");
            StopCoroutine("Firing");
            hit = true;
        }
    }
    IEnumerator Disablecollider()
    {
        animator.SetTrigger("DestroyEnemy");
        audioSource.PlayOneShot(ExplosionClip);
        speed = 0;
        Destroy(this.gameObject, 2.65f);
        yield return new WaitForSeconds(0.4f);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator  Firing()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            GameObject LaserClone = Instantiate(Laser, transform.position + Vector3.down, Quaternion.identity);
            LaserMovement[] Lasers = LaserClone.GetComponentsInChildren<LaserMovement>();
            foreach (LaserMovement Laser in Lasers)
            {
                Laser.SetDirection(-1);
                Laser.tag = "EnemyLaser";
            }
            audioSource.PlayOneShot(LaserClip);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }   
}