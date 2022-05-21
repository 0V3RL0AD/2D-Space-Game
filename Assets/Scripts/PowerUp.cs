using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 2f;
    // create an ID for powerups
    // 0 = triple shot
    // 1 = speed
    // 2 = shields
    [SerializeField]
    private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Random.Range(10, -10), 6, 0);
    }

    // Update is called once per frame
    void Update()
    {
        PowerUpMovement();
    }
    void PowerUpMovement()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //other.transform.GetComponent<Player>().TripleShotActive();
            if (other.tag != null)
            {
                if (powerupID == 0)
                {
                    
                }
                else if (powerupID == 1)
                {
                    
                }
                else if (powerupID == 2)
                {
                    
                }

                switch(powerupID)
                {
                    case 0:
                        other.transform.GetComponent<Player>().TripleShotActive();
                        break;
                    case 1:
                        other.transform.GetComponent<Player>().SpeedUpActive();
                        break;
                    case 2:
                        other.transform.GetComponent<Player>().ShieldActive();
                        break;
                    default:
                        Debug.Log("default value");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }
}
