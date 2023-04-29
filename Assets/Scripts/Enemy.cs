using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private Player _player;
    private Animator Enemy_Anim;
    [SerializeField]
    private AudioClip _Explosion_Sound;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        //connects the player script to the enemy through finding the player gameobject and getting it's script
        _player = GameObject.Find("Player").GetComponent<Player>();
        //null check player
        if (_player == null)
        {
            Debug.LogError("Player = null");
        }
        Enemy_Anim = gameObject.GetComponent<Animator>();
        //null check animator
        if (Enemy_Anim == null)
        {
            Debug.LogError("Animator = null");
        }

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
            _audioSource.PlayOneShot(_Explosion_Sound, 1F);
            Enemy_Anim.SetTrigger("OnEnemyDeath");
            _Speed = 0;
            //changed to destory after certain time has passed (2.5 seconds - just longer than the destruction animation)
            Destroy(this.gameObject, 2.5f);
            
            
        }
        if (other.tag == "Player")
        {
            //Player player = other.transform.GetComponent<Player>();
            other.transform.GetComponent<Player>().Damage();

            /*if (player!= null)
            {
                player.Damage();
            }*/
            _audioSource.PlayOneShot(_Explosion_Sound, 1F);
            Enemy_Anim.SetTrigger("OnEnemyDeath");
            _Speed = 0;
            //changed to destroy after certain time has passed (2.5 seconds - just longer than the destruction animation)
            Destroy(this.gameObject, 2.5f);
        }
    }
}
