using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private float _speedMultiplier = 2.0f;
    [SerializeField]
    private GameObject _LaserPrefab;
    [SerializeField]
    private float _FireRate = 0.5f;
    [SerializeField]
    private float _CanFire = -1f;
    [SerializeField]
    private int _Lives = 3;
    [SerializeField]
    public int _Score;
    [SerializeField]
    private SpawnManager _SpawnManager;
    [SerializeField]
    private GameObject _Triple_ShotPrefab;
    [SerializeField]
    private bool _TripleShotActive = false;
    [SerializeField]
    private bool _SpeedUpActive = false;
    [SerializeField]
    private bool _ShieldActive = false;
    [SerializeField]
    private GameObject _ShieldVisualiserPrefab;
    [SerializeField]
    private UIManager _uiManager;

    void Start()
    {
        transform.position = new Vector3(-0.06f, -2.82f, 0);
        _SpawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        //connects the UI script to the enemy through finding the player gameobject and getting it's script
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _CanFire)
        {
            FireLaser();
        }
    }
    public void Damage()
    {
        if (_ShieldActive == true)
        {
            _ShieldActive = false;
            _ShieldVisualiserPrefab.SetActive(false);
            return;
        }
        else
        {
            _Lives = _Lives - 1;
            _uiManager.UpdateLives(_Lives);

            if (_Lives < 1)
            {
                _SpawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }
    }
    public void ScoreUp(int points)
    {
        _Score += points;
        _uiManager.ScoreUpdate(_Score);
    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (_SpeedUpActive == false)
        {
            transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * horizontalInput * (_speed * _speedMultiplier) * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * (_speed * _speedMultiplier) * Time.deltaTime);
        }
        
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        
        if (transform.position.x > 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x < -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    public void FireLaser()
    {
        _CanFire = Time.time + _FireRate;

            if (_TripleShotActive == true)
            {
                Instantiate(_Triple_ShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_LaserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
    }
   
    public void SpeedUpActive()
    {
        _SpeedUpActive = true;
        StartCoroutine(SpeedUpPowerDownRoutine());
    }
    IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return null;
        while (_SpeedUpActive == true)
        {
            yield return new WaitForSeconds(7.0f);
            _SpeedUpActive = false;
        }
    }
    public void TripleShotActive()
    {
        _TripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return null;
        while (_TripleShotActive == true)
        {
            yield return new WaitForSeconds(7.0f);
            _TripleShotActive = false;
        }
    }
    public void ShieldActive()
    {
        _ShieldActive = true;
        _ShieldVisualiserPrefab.SetActive(true);
    }

    /*IEnumerator ShieldDownRoutine()
    {
        yield return null;
        while (_ShieldActive == true)
        {
            yield return new WaitForSeconds(5.0f);
            _ShieldActive = false;
        }
    }*/
}
