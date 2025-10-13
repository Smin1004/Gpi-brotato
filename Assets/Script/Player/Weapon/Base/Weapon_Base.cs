using UnityEngine;
using DG.Tweening;

public abstract class Weapon_Base : MonoBehaviour
{
    public Transform me;
    [SerializeField]protected Collider2D hitCollider;
    [Header("Stat")]
    public float range;
    public float curDelay;
    public float attackDelay;
    public float damage;
    public LayerMask suckableLayer;

    protected bool isFire = false;

    protected Vector3 dir_gun;
    protected Vector3 target;
    protected float rot;
    protected float angle;

    public virtual void Setting()
    {
        me.TryGetComponent(out hitCollider);
    }

    protected virtual void Update()
    {
        FindObj();

        if (curDelay <= attackDelay && !isFire) curDelay += Time.deltaTime;
    }

    protected void FindObj()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, suckableLayer);
        if (colliders.Length == 0) return;
        float dis = range + 10;
        Vector3 outValue = Vector3.zero;
        foreach (Collider2D col in colliders)
        {
            //Vector2 toTarget = (col.transform.position - transform.position).normalized;
            float temp = Vector2.Distance(col.transform.position, transform.position);
            if (temp < dis)
            {
                dis = temp;
                outValue = col.transform.position;
            }
        }
        dir_gun = new Vector3(outValue.x, outValue.y) - me.transform.position;
        float z = Mathf.Atan2(dir_gun.y, dir_gun.x) * Mathf.Rad2Deg;
        rot = z - 90f;
        angle = Vector2.SignedAngle(Vector2.right, dir_gun) - 90f;

        //startpos.localPosition = _startpos;
        if (!isFire)
        {
            if (curDelay <= attackDelay) transform.eulerAngles = new Vector3(0, 0, angle);
            else { transform.eulerAngles = new Vector3(0, 0, angle); Shot();}
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);

    }

    public virtual void OnChildHit(Collider2D other)
    {
        if(other.TryGetComponent<Enemy_Base>(out var enemy))
        {
            enemy.Enemy_Damage(damage);
            enemy.KnockBack(Player.Instance.transform);
        }
    }

    protected abstract void Shot();
}
