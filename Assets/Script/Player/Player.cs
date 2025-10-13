using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

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

    // public Gun_Base nowWeapon;
    public float radius;
    public List<Weapon_Base> weaponList = new List<Weapon_Base>();
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
    void Awake()
    {
        _Instance();
        //나중에 순서 맞추는 OBJ 만들것
    }
    private void Start()
    {
        //_Instance();
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //stat = GameManager.Instance.stat;
        //HP = stat.hp;
    }

    void Update()
    {
        pos = transform.position;
        //if (!isActive) return;

        Move();
        WeaponSetting();
    }

    void WeaponSetting()
    {
        float angle;
        float plusValue = 360 / weaponList.Count;
        float rad;
        Vector3 plusVector = Vector3.zero;
        if (weaponList.Count <= 2)
        {
            plusVector = new Vector3(0,0.5f);
            angle = 0;
        }
        else angle = 90;
        for (int i = 0; i < weaponList.Count; i++)
        {
            rad = angle * Mathf.Deg2Rad;
            weaponList[i].transform.position = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)) * radius + plusVector + transform.position;
            weaponList[i].Setting();
            angle += plusValue;
        }
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
