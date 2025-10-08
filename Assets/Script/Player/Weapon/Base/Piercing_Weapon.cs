using DG.Tweening;
using UnityEngine;

public abstract class Piercing_Weapon : Weapon_Base
{
    public float hoveringTime;
    public float shootingTime;

    protected override void Shot()
    {
        isFire = true;
        // me.transform.rotation = new Quaternion(0, 0, rot, 0);

        me.transform.DOLocalMove(new Vector3(0, range), shootingTime);
        Invoke("Back", hoveringTime);
    }

    void Back()
    {
        me.transform.DOLocalMove(new Vector3(0, 0), shootingTime);
        Invoke("End", shootingTime);
    }

    void End()
    {
        curDelay = 0;
        isFire = false;
    }
}
