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
    [SerializeField] float spawnTime;

    void Start()
    {
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

            Destroy(Instantiate(Warning, spawnPos, Quaternion.identity, this.transform), 1);
            yield return new WaitForSeconds(spawnTime);

            var temp = Instantiate(testEnemy, spawnPos, Quaternion.identity, this.transform);
        }
    }
}
