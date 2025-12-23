using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnRange;
    [SerializeField] float notSpawnRange;
    [SerializeField] GameObject Warning;
    [SerializeField] Enemy_Base testEnemy;
    [SerializeField] float warningTime;
    [SerializeField] float spawnTime;

    WaitForSeconds _warningTime, _spawnTime;
    void Start()
    {
        _warningTime = new WaitForSeconds(warningTime);
        _spawnTime = new WaitForSeconds(spawnTime);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float x, y;
        Vector3 spawnPos;
        while (true)
        {
            if (Random.value < 0.5f) x = Random.Range(-spawnRange, -notSpawnRange);
            else x = Random.Range(notSpawnRange, spawnRange);
            if (Random.value < 0.5f) y = Random.Range(-spawnRange, -notSpawnRange);
            else y = Random.Range(notSpawnRange, spawnRange);

            spawnPos = Player.Instance.transform.position + new Vector3(x, y);

            ObjectPoolManager.Instance.Spawn(Warning, spawnPos, Quaternion.identity).GetComponent<PoolableObject>().TimeReturn(warningTime);
            yield return _warningTime;

            ObjectPoolManager.Instance.Spawn(testEnemy.gameObject, spawnPos, Quaternion.identity);
            yield return _spawnTime;
        }
    }
}
