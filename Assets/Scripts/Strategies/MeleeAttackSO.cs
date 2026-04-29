using UnityEngine;

[CreateAssetMenu(menuName = "Strategies/Melee Attack")]
public class MeleeAttackSO : AttackStrategySO
{
    public int damage = 10;

    public override void Attack(Transform attacker, Transform target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Debug.Log(attacker.name + " Yakýn Dövüþ ile " + damage + " hasar verdi!");
        }
    }
}