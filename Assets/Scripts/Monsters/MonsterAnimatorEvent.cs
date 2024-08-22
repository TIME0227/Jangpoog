using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimatorEvent : MonoBehaviour
{
    private MonsterWeaponCollider weaponCollider;
    // Start is called before the first frame update
    void Start()
    {
        weaponCollider = Util.FindChild<MonsterWeaponCollider>(transform.parent.gameObject);
    }

    public void WeaponAttack()
    {
        weaponCollider.AttackPlayerByWeapon();
    }
}
