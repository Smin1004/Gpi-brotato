using UnityEngine;

public abstract class Bullet_Base : PoolableObject
{
    float cur_lifeTime;
    //[SerializeField] protected float lifeTime;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;

    [SerializeField] protected bool isStop;
    [SerializeField] bool isHit;

    public void Stop(bool _isStop)
    {
        isStop = _isStop;
    }

    public void Init(float _moveSpeed, float _damage)
    {
        moveSpeed = _moveSpeed;
        damage = _damage;
        TimeReturn(5);
    }

    private void OnCollisionEnter2D(Collision2D hit)
    {
        
        // else if (isHit && hit.collider.TryGetComponent<Player>(out var p_hit))
        // {
        //     Player.Instance.Damage();
        //     Hit_Event();
        // }
        // else if (hit.collider.CompareTag("Wall"))
        // {
        //     Hit_Wall(hit);
        // }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        if (!isHit && hit.TryGetComponent<Enemy_Base>(out var e_hit))
        {
            Debug.Log("bullet HIt");
            e_hit.Enemy_Damage(damage);
            e_hit.KnockBack(Player.Instance.transform);
            Hit_Event();
        }
    }

    protected abstract void Hit_Event();
    protected abstract void Hit_Wall(Collision2D hit);
}
