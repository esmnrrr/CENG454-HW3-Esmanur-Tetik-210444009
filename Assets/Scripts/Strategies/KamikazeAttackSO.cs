using UnityEngine;

[CreateAssetMenu(menuName = "Strategies/Kamikaze Attack")]
public class KamikazeAttackSO : AttackStrategySO
{
    public int damage = 50;

    public override void Attack(Transform attacker, Transform target)
    {
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Debug.Log(attacker.name + " KAMÝKAZE Yaptý! " + damage + " hasar verdi ve patladý!");

            // Kendini imha et
            Destroy(attacker.gameObject);
        }
    }
}