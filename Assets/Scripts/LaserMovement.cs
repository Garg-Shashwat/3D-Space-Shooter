using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 8f, range = 7f, direction = 1f;
    void Awake()
    {
        direction = 1f;
    }
    void Update()
    {
        CalculateMovement();
    }
    void CalculateMovement()
    {
        transform.Translate((Vector3.up*direction)*speed*Time.deltaTime);
        if(Mathf.Abs(transform.position.y) > range)
        {
            if (transform.parent.tag == "DoubleShot" || transform.parent.tag == "TripleShot")
                Destroy(this.transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

    public void SetDirection(float i)
    {
        direction = i;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && transform.tag == "EnemyLaser")
        {
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            other.GetComponent<PlayerController>().ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}
