using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Melee_Weapon : Weapon_Base
{
    [SerializeField] float curShootingTime;
    [SerializeField] private float shootingTime;
    [SerializeField] private float weaponRadius;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    [SerializeField] private List<Vector3> plusPos;

    public override void Setting()
    {
        base.Setting();
        weaponRadius = Player.Instance.radius;
        startPos.localPosition = new Vector3(-1, -weaponRadius);
        endPos.localPosition = new Vector3(1, -weaponRadius);

        Debug.Log(endPos);
    }

    protected override void Update()
    {
        base.Update();
        if (isFire)
        {
            me.transform.localPosition = Bezier(startPos.localPosition, endPos.localPosition, plusPos, curShootingTime);
            curShootingTime += Time.deltaTime;
        }
    }

    protected override void Shot()
    {
        isFire = true;
        curShootingTime = 0;
        plusPos.Clear();
        plusPos.Add(GetCirclePos(Vector3.zero, range));
        // me.transform.rotation = new Quaternion(0, 0, rot, 0);
        Invoke("End", shootingTime + 0.1f);
    }

    void End()
    {
        me.transform.localPosition = Vector3.zero;
        curDelay = 0;
        isFire = false;
    }

    Vector3 GetCirclePos(Vector3 center, float radius){
        return center + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * radius, Mathf.Sign(angle * Mathf.Rad2Deg) * radius);
    }
    
    Vector3 Bezier(Vector3 startPos, Vector3 endPos, List<Vector3> plusPos, float value)
    {
        List<Vector3> posList = new List<Vector3>();

        posList.Add(startPos);
        foreach (var n in plusPos)
        {
            posList.Add(n);
        }
        posList.Add(endPos);

        while (posList.Count > 1)
        {
            List<Vector3> curPos = new List<Vector3>();
            for (int i = 0; i < posList.Count - 1; i++)
            {
                curPos.Add(Vector3.LerpUnclamped(posList[i], posList[i + 1], value));
            }
            posList = curPos;
        }

        return posList[0];
    }
}
