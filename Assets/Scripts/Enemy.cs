using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _Speed = 3f;
    void Start()
    {
        transform.position = new Vector3(Random.Range(10, -10), 6, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.Range(10, -10), 6, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();
            other.transform.GetComponent<Player>().Damage();
            
            /*if (player!= null)
            {
                player.Damage();
            }*/
            
            Destroy(this.gameObject);
        }
    }
}
