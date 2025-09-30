using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private static Player _instance = null;
    public static Player Instance => _instance;
    
    Rigidbody2D rigid;
    Animator anim;
    Vector3 pos;
    Stat stat;

    [Header("Status")]
    public int HP;
    public float moveSpeed;

    [Header("Weapon")]
    public Transform gunPos;
    // public Gun_Base nowWeapon;
    // public List<Gun_Base> weaponList = new List<Gun_Base>();
    // public List<Gun_Base> prefabList = new List<Gun_Base>();

    [Header("Values")]
    [SerializeField] Vector2 checkbox_Size;
    [SerializeField] float cur_ui_delay;
    float x, y;

    bool isDamage;
    bool isSlow;
    public bool isActive = false;

    [Header("Pos")]
    [SerializeField] Vector3 localPosition;
    [SerializeField] Vector2 dir;

    public void _Instance()
    {
        _instance = this;
    }

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        stat = GameManager.Instance.stat;
        HP = stat.hp;
    }

    void Update()
    {
        pos = transform.position;
        //if (!isActive) return;

        Move();
    }

    void Move()
    {
        // if (isSlow) return;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        //dir = target.position - pos;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Vector3 nor = new Vector3(x, y, 0f).normalized;
        rigid.linearVelocity = new Vector2(nor.x * moveSpeed, nor.y * moveSpeed);

    }
    
    public void Damage()
    {
        StartCoroutine(damage());

        IEnumerator damage()
        {

            if (isDamage) yield break;

            isDamage = true;

            // if(isActive) pui.Damage();
            // SoundManager.Instance.Sound(hit, false, 0.5f);

            // HP--;
            // pui.SettingHP(HP);
            // if (HP < 1) AllStop(true);
            // yield return new WaitForSeconds(invincible_Time);
            // isDamage = false;
        }
    }

    public void AllStop(bool isDie)
    {

    }
}
