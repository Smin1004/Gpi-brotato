using UnityEngine;
using DG.Tweening;

public class Ranged_Weapon : Weapon_Base
{
    [SerializeField] protected Transform startpos;
    [SerializeField] protected Bullet_Base bullet;

    public float randomDir;
    public float bulletSpeed;

    protected override void Shot()
    {
        isFire = true;
        float dir_ran = Random.Range(-randomDir, randomDir + 1);
        var temp = ObjectPoolManager.Instance.Spawn(bullet.gameObject, startpos.transform.position, Quaternion.Euler(0, 0, rot + dir_ran)).GetComponent<Bullet_Base>();
        temp.Init(bulletSpeed, damage);
        curDelay = 0;
        isFire = false;
    }
}
