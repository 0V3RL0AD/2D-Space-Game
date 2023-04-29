using JetBrains.Annotations;
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
    
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawntripleShotRoutine());
        StartCoroutine(SpeedupRoutine());
        StartCoroutine(ShieldupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return null;
        while (_PlayerDead == false)
        {
            //spawn position of the enemies that spawn
            Vector3 SpawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);
            //In order to control the instatiated Enemies (as they cannot be manually controlled due to the laws of instantiate) create a GameObject variable to handle its information.
            //Using the variable, apply the position and rotation of every spawned enemy.
            GameObject newEnemy = Instantiate(_enemy, SpawnPosition, Quaternion.identity);
            //Making the gameobject variable's parent the enemy container so that it spawns within the enemy container.
            //This ensures they are forever in the exact same position
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
