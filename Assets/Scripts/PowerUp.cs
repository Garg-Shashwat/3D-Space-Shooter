using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    [SerializeField]
    int ID; //0=TripleShot, 1=Speed, 2=Shields

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.x < -6.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController control = collision.GetComponent<PlayerController>();
            if (control != null)
            {
                switch (ID)
                {
                    case 0: control.ActivateTripleShot();
                        break;
                    case 1: control.ActivateSpeed();
                        break;
                    case 2: control.ActivateShield();
                        break;
                    default: Debug.LogError("Unknown PowerUp ID!");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
