using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 3.5f, firerate = 0.5f, speedrate = 7f, immunerate = 0.4f;
    float horizontalInput, verticalInput, speedmultiplier = 1.6f;

    bool isCool = true, isTripleActive = false, isImmune = false, isReady = false;

    [SerializeField]
    int PlayerIndex;
    int[] lives = new int[2];
    int triplecount = 0;
    readonly int MaxLives = 3, MaxTriple = 3;

    Vector3 Position;

    [SerializeField]
    GameObject Laser, TripleShot, Shields, LeftEngineFire, RightEngineFire, Thrusters, LaserContainer, Background;
    [SerializeField]
    AudioClip LaserClip, ExplosionClip, PowerUpClip;

    UIManager uIManager;
    Animator animator;
    AudioSource audioSource;
    GameManager gamemanager;

    void Start()
    {
        lives[0] = lives[1] = 3;
        uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        gamemanager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (uIManager == null)
            Debug.LogError("UIManager Script is null");

        if (animator == null)
            Debug.LogError("Player Animator cannot be attached");

        if (audioSource == null)
            Debug.LogError("Audio Source cannot be attached");
        else
            audioSource.clip = LaserClip;
        if (gamemanager == null)
            Debug.LogError("GameManager Script is null");
        else if (!gamemanager.isCoop)
        {
            transform.position = new Vector3(0, -2f, 0);
            lives[1] = 0;
        }
        StartCoroutine(start());
    }

    void Update()
    {
        if (gamemanager.depth() == 0 && isReady)
        {
            if (PlayerIndex == 0)
            {
                if (lives[PlayerIndex] > 0)
                    CalculateOneMovement();
                if (Input.GetAxis("Fire1") != 0 && isCool)
                    Firing();
            }
            else
            {
                if (lives[PlayerIndex] > 0)
                    CalculateTwoMovement();
                if (Input.GetKeyDown(KeyCode.Space) && isCool)
                    Firing();
            }
            if (isTripleActive && triplecount == MaxTriple)
            {
                isTripleActive = false;
            }
        }
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(1.1f);
        isReady = true;
    }

    void CalculateOneMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", horizontalInput);
        verticalInput = Input.GetAxis("Vertical");
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Direction * speed * Time.deltaTime);
        if (!gamemanager.isCoop)
        {
            Position.x = -transform.position.x * 0.08f;
            Position.y = -(transform.position.y + 2) * 0.2f;
            Background.transform.position = new Vector3(Mathf.Clamp(Position.x, -0.8f, 0.8f), Mathf.Clamp(Position.y, -0.4f, 0.4f), 1);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, -4.8f, 0), 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 0), 0);
            if (Mathf.Abs(transform.position.x) > 10.7f)
            {
                transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
            }
        }
    }

    void CalculateTwoMovement()
    {
        horizontalInput = Input.GetAxis("AltHorizontal");
        animator.SetFloat("Speed", horizontalInput);
        verticalInput = Input.GetAxis("AltVertical");
        Vector3 Direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(Direction * speed * Time.deltaTime);
        if (!gamemanager.isCoop)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -10f, 10f), Mathf.Clamp(transform.position.y, -4.8f, 0), 0);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.8f, 0), 0);
            if (Mathf.Abs(transform.position.x) > 10.7f)
            {
                transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
            }
        }
    }

    void Firing()
    {
        if (isTripleActive)
        {
            Instantiate(TripleShot, transform.position, Quaternion.identity);
            triplecount++;
        }
        else
        {
            GameObject LaserClone = Instantiate(Laser, transform.position + Vector3.up, Quaternion.identity);
            LaserClone.transform.parent = LaserContainer.transform;
        }
        audioSource.Play();
        isCool = false;
        StartCoroutine(GunTimer());
    }
    IEnumerator GunTimer()
    {
        yield return new WaitForSeconds(firerate);
        isCool = true;
    }

    public void ChangeHealth(int points)
    {

        if (!isImmune)
        {
            if (points < 0 || lives[PlayerIndex] < MaxLives)
            {
                lives[PlayerIndex] += points;
                if (lives[PlayerIndex] < 0)
                    lives[PlayerIndex] = 0;
                uIManager.UpdateLives(lives[PlayerIndex], PlayerIndex);
                isImmune = true;
                StartCoroutine(ImmuneTimer());
            }
        }
        else
        {
            isImmune = false;
            Shields.SetActive(false);
        }
        switch (lives[PlayerIndex])
        {
            case 0:
                animator.SetTrigger("DeathTrigger");
                audioSource.PlayOneShot(ExplosionClip);
                Thrusters.SetActive(false);
                RightEngineFire.SetActive(false);
                LeftEngineFire.SetActive(false);
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 2.65f);
                break;
            case 1:
                RightEngineFire.SetActive(true);
                break;
            case 2:
                LeftEngineFire.SetActive(true);
                RightEngineFire.SetActive(false);
                break;
            case 3:
                LeftEngineFire.SetActive(false);
                break;
            default:
                Debug.LogError("Error in Player" + PlayerIndex + 1 + "Lives");
                break;
        }
    }

    public void ActivateTripleShot()
    {
        audioSource.PlayOneShot(PowerUpClip);
        isTripleActive = true;
        triplecount = 0;
    }


    public void ActivateSpeed()
    {
        audioSource.PlayOneShot(PowerUpClip);
        speed *= speedmultiplier;
        StartCoroutine(SpeedTimer());
    }
    IEnumerator SpeedTimer()
    {
        yield return new WaitForSeconds(speedrate);
        speed /= speedmultiplier;
    }

    public void ActivateShield()
    {
        audioSource.PlayOneShot(PowerUpClip);
        isImmune = true;
        Shields.SetActive(true);
    }

    IEnumerator ImmuneTimer()
    {
        yield return new WaitForSeconds(immunerate);
        isImmune = false;
    }
}
