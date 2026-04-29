using UnityEngine;

// 1. Interface sözleþmemiz (Hocanýn istediði gibi)
public interface IAttackStrategy
{
    void Attack(Transform attacker, Transform target);
}

// 2. Unity'de dosya (ScriptableObject) yaratabilmek için aracý sýnýfýmýz
public abstract class AttackStrategySO : ScriptableObject, IAttackStrategy
{
    public float attackRange = 2f; // Saldýrý menzili
    public float attackCooldown = 1f; // Ýki saldýrý arasý bekleme süresi

    // Alt sýnýflar bu metodu doldurmak zorunda
    public abstract void Attack(Transform attacker, Transform target);
}