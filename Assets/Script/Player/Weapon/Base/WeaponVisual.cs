using UnityEngine;

public class WeaponVisual : MonoBehaviour
{
    public Weapon_Base parentWeapon;

    void OnTriggerEnter2D(Collider2D other)
    {
        parentWeapon.OnChildHit(other);
    }
}