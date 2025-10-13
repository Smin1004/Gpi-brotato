using UnityEngine;
using DG.Tweening;

public abstract class Piercing_Weapon : Weapon_Base
{
    public float hoveringTime;
    public float shootingTime;

    protected override void Shot()
    {
        isFire = true;
        hitCollider.enabled = true;
        // me.transform.rotation = new Quaternion(0, 0, rot, 0);

        me.transform.DOLocalMove(new Vector3(0, range), shootingTime).OnComplete(() => Invoke(nameof(Back), hoveringTime));
    }

    void Back()
    {
        me.DOLocalMove(Vector3.zero, shootingTime).OnComplete(() => End());
    }

    void End()
    {
        hitCollider.enabled = false;
        curDelay = 0;
        isFire = false;
    }
}
