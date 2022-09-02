using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _Speed = 3f;
    [SerializeField]
    private Player _player;

    void Start()
    {
        //connects the player script to the enemy through finding the player gameobject and getting it's script
        _player = GameObject.Find("Player").GetComponent<Player>();
        
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
            //adds score to player when laser hits the enemy
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.ScoreUp(10);
            }
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
