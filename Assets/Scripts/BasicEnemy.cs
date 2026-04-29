using UnityEngine;
using UnityEngine.Pool; // Havuz kütüphanesi!

// Düþman da mermilerimizden hasar alacaðý için IDamageable kullanýyor!
public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Transform targetCore;
    public float speed = 2f;

    public int maxHealth = 40; 
    private int currentHealth;

    public AttackStrategySO attackStrategy;
    private float lastAttackTime;

    // Düþmanýn ait olduðu havuzun referansý
    private IObjectPool<BasicEnemy> pool;

    public void SetPool(IObjectPool<BasicEnemy> pool)
    {
        this.pool = pool;
    }

    // HOCANIN ZORUNLU KURALI: Havuzdan her çýktýðýnda canýný SIFIRLA!
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
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
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Düþman düþtü, havuza geri gönderiliyor!");

        // Obje silinmiyor, havuza iade ediliyor (Release)
        if (pool != null)
        {
            pool.Release(this);
        }
        else
        {
            gameObject.SetActive(false); // Havuz yoksa sadece gizle
        }
    }
}