using UnityEngine;

public abstract class Weapon_Base : MonoBehaviour
{
    public Transform me;
    [Header("Stat")]
    public float range;
    public float curDelay;
    public float attackDelay;
    public LayerMask suckableLayer;
    
    protected Vector3 dir_gun;
    protected Vector3 target;
    protected float rot;

    public virtual void Setting()
    {

    }

    protected virtual void Update()
    {
        FindObj();
        Spin();

        if (curDelay <= attackDelay) curDelay += Time.deltaTime;
        else Shot();
    }

    void Spin()
    {
        dir_gun = new Vector3(target.x, target.y - 0.5f) - me.transform.position;
        float z = Mathf.Atan2(dir_gun.y, dir_gun.x) * Mathf.Rad2Deg;
        rot = z - 90f;
        float angle = Vector2.SignedAngle(Vector2.right, dir_gun);

        //startpos.localPosition = _startpos;
        me.eulerAngles = new Vector3(0, 0, angle);
    }

    void FindObj()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, suckableLayer);

        foreach (Collider2D col in colliders)
        {
            Vector2 toTarget = (col.transform.position - transform.position).normalized;
        }
    }

    void OnDrawGizmosSelected()
    {
        // if (suckPoint != null)
        // {
        //     Vector3 dir = suckDirection.normalized;
        //     float halfFOV = fieldOfView / 2f;

        //     Vector3 leftBoundary = Quaternion.Euler(0, 0, -halfFOV) * dir;
        //     Vector3 rightBoundary = Quaternion.Euler(0, 0, halfFOV) * dir;

        //     Gizmos.color = Color.yellow;
        //     Gizmos.DrawLine(suckPoint.position, suckPoint.position + leftBoundary * suckRange);
        //     Gizmos.DrawLine(suckPoint.position, suckPoint.position + rightBoundary * suckRange);

        //     Gizmos.color = Color.red;
        //     Gizmos.DrawWireSphere(suckPoint.position, 0.3f);
        // }
    }

    protected abstract void Shot();
}
