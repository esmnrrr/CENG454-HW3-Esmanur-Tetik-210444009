using UnityEngine;

[CreateAssetMenu(menuName = "Strategies/Melee Attack")]
public class MeleeAttackSO : AttackStrategySO
{
    public int damage = 10;

    public override void Attack(Transform attacker, Transform target)
    {
        // 1. Zombinin Animator'ünü bul ve Vurma animasyonunu tetikle!
        Animator anim = attacker.GetComponentInChildren<Animator>();
        if (anim != null) anim.SetTrigger("Attack");

        // 2. Normal hasar verme iþlemleri...
        IDamageable damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Debug.Log(attacker.name + " Yakýn Dövüþ ile " + damage + " hasar verdi!");
        }
    }
}


