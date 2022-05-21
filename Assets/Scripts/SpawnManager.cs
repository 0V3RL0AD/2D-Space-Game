﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerupPrefab;
    [SerializeField]
    private GameObject _SpeedUpPrefab;
    [SerializeField]
    private GameObject _ShieldUpPrefab;
    [SerializeField]
    private bool _PlayerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawntripleShotRoutine());
        StartCoroutine(SpeedupRoutine());
        StartCoroutine(ShieldupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return null;
        while (_PlayerDead == false)
        {
            //spawn position of the enemies that spawn
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //making the enemy prefab apply to the spawn position as the gameObject newEnemy
            GameObject newEnemy = Instantiate(_enemy, SpawnPosition, Quaternion.identity);
            //connnecting the gameobject of enemies spawning (newEnemy) to the enemy container to make it spawn in the container
            newEnemy.transform.parent = _enemyContainer.transform;
            //making the spawn manager wait 2 seconds
            yield return new WaitForSeconds(2.0f);
        }
    }
    IEnumerator SpawntripleShotRoutine()
    {
        yield return null;
        while (_PlayerDead == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_tripleShotPowerupPrefab, SpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8)); 
        }
    }
    IEnumerator SpeedupRoutine()
    {
        yield return null;
        while (_PlayerDead == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_SpeedUpPrefab, SpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
    IEnumerator ShieldupRoutine()
    {
        yield return null;
        while (_PlayerDead == false)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_ShieldUpPrefab, SpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerDead = true;
    }
}
