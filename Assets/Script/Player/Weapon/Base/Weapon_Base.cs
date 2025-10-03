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

        if (curDelay <= attackDelay) curDelay += Time.deltaTime;
        else Shot();
    }

    void FindObj()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, suckableLayer);
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
        if (colliders.Length == 0) return;
        dir_gun = new Vector3(outValue.x, outValue.y) - me.transform.position;
        float z = Mathf.Atan2(dir_gun.y, dir_gun.x) * Mathf.Rad2Deg;
        rot = z - 90f;
        float angle = Vector2.SignedAngle(Vector2.right, dir_gun) - 90f;

        //startpos.localPosition = _startpos;
        me.eulerAngles = new Vector3(0, 0, angle);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    
    }

    protected abstract void Shot();
}
