using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Exp : PoolableObject
{
    public void Get()
    {
        StartCoroutine(MoveTo(1));
    }

    IEnumerator MoveTo(float sec)
    {
        float timer = 0f;
        Vector3 start = transform.position;

        while (timer <= sec)
        {
            if(Player.Instance.isNotActive) continue;
            transform.position = Vector3.LerpUnclamped(start, Player.Instance.transform.position, Easing.easeInOutBack(timer / sec));
            timer += Time.deltaTime;
            yield return null;
        }
        Player.Instance.curExp++;
        ReturnToPool();
        yield break;
    }
}
