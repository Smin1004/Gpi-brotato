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
        //이따 오브젝트 풀링 적용좀 하자 / 아직 소환 많이 없어서 조금만 미룰 예정
        var temp = Instantiate(bullet, startpos.transform.position, Quaternion.Euler(0, 0, rot + dir_ran)).GetComponent<Bullet_Base>();
        temp.Init(bulletSpeed, damage);
        curDelay = 0;
        isFire = false;
    }
}
