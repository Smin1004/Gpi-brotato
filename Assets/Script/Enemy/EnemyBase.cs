using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("Enemy_Base")]
    protected Transform target;
    [SerializeField] protected GameObject die_effect;

    [SerializeField] float HP;
    [SerializeField] float maxHP;
    [SerializeField] protected float fireRange;
    [SerializeField] protected float speed;
    [SerializeField] float knockbackTime;
    [SerializeField] bool stopMove;

    public float knockbackForce;
    protected Vector2 direction;
    protected Rigidbody2D rb;
    bool isKnockBack;
    bool isDie;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (stopMove) return;

        target = Player.Instance.transform;
        direction = (target.position - transform.position).normalized;
        if (!isKnockBack) rb.linearVelocity = direction * speed;

        //var hits = Physics2D.RaycastAll(transform.position, direction, fireRange);
    }
    void KnockOff()
    {
        isKnockBack = false;
    }

    public void KnockBack(Transform other)
    {
        if (isKnockBack) return;
        isKnockBack = true;
        Vector2 difference = (transform.position - other.position).normalized;
        Vector2 force = difference * knockbackForce;
        rb.AddForce(force, ForceMode2D.Impulse); //if you don't want to take into consideration enemy's mass then use ForceMode.VelocityChange
        Invoke("KnockOff", knockbackTime);
    }
}
