using UnityEngine;

// Düþman da mermilerimizden hasar alacaðý için IDamageable kullanýyor!
public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Transform targetCore;
    public float speed = 2f;
    public int health = 40;

    // STRATEJÝ DESENÝ! Düþmana dýþarýdan strateji baðlýyoruz.
    public AttackStrategySO attackStrategy;

    private float lastAttackTime;

    void Start()
    {
        // Eðer targetCore boþsa, sahnede "Core" etiketli objeyi otomatik bulsun
        if (targetCore == null)
        {
            GameObject core = GameObject.FindGameObjectWithTag("Core");
            if (core != null) targetCore = core.transform;
        }
    }

    void Update()
    {
        if (targetCore == null || attackStrategy == null) return;

        float distance = Vector3.Distance(transform.position, targetCore.position);

        // Menzilde deðilse hedefe yürü
        if (distance > attackStrategy.attackRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCore.position, speed * Time.deltaTime);
        }
        // Menzildeyse ve bekleme süresi dolduysa STRATEJÝYÝ UYGULA!
        else if (Time.time >= lastAttackTime + attackStrategy.attackCooldown)
        {
            attackStrategy.Attack(transform, targetCore);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Düþman öldürüldü!");
            Destroy(gameObject);
        }
    }
}

    
