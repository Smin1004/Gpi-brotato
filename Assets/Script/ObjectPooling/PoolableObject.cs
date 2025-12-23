using UnityEngine;
using System.Collections;

public abstract class PoolableObject : MonoBehaviour
{
    private GameObject myPrefab;

    public virtual void SetPrefab(GameObject prefab)
    {
        myPrefab = prefab;
    }

    public virtual void SpawnInit()
    {
        
    }

    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.Despawn(this.gameObject, myPrefab);
    }
    
    public void TimeReturn(float time)
    {
        StartCoroutine(AutoReturn(time));
    }
    
    IEnumerator AutoReturn(float Time)
    {
        yield return new WaitForSeconds(Time);
        ReturnToPool();
    }
}