using UnityEngine;

public interface IAttackStrategy
{
    // Saldýrýyý yapacak kiþi ve hedefini parametre olarak alýyoruz
    void Attack(Transform attacker, Transform target);
}