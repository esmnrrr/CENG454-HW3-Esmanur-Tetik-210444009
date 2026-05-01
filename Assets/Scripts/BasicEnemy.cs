using UnityEngine;
using UnityEngine.Pool; // Havuz kütüphanesi!

// Düþman da mermilerimizden hasar alacaðý için IDamageable kullanýyor!
public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Animator animator;

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
        // 1. ZOMBÝ DOÐAR DOÐMAZ KENDÝ ANÝMATOR'ÜNÜ KENDÝ BULSUN! (Artýk sürükle-býraka gerek yok)
        animator = GetComponent<Animator>();

        // 2. ÇEKÝRDEÐÝ BULMA KODU (Bu zaten vardý)
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

        if (distance > attackStrategy.attackRange)
        {
            // 1. UÇMAYI ENGELLE: Hedefin sadece yatay konumunu al, yüksekliði zombiyle ayný kalsýn!
            Vector3 flatTargetPos = new Vector3(targetCore.position.x, transform.position.y, targetCore.position.z);

            // 2. YAN KAYMAYI ENGELLE: Zombinin yüzünü hedefe döndür!
            transform.LookAt(flatTargetPos);

            // 3. ÝLERLE: Þimdi dümdüz o noktaya yürü
            transform.position = Vector3.MoveTowards(transform.position, flatTargetPos, speed * Time.deltaTime);

            // YÜRÜYORSA ANÝMASYONU BAÞLAT
            if (animator != null) animator.SetBool("isWalking", true);
        }
        else
        {
            // MENZÝLE GÝRDÝYSE YÜRÜMEYÝ DURDUR
            if (animator != null) animator.SetBool("isWalking", false);

            if (Time.time >= lastAttackTime + attackStrategy.attackCooldown)
            {
                attackStrategy.Attack(transform, targetCore);
                lastAttackTime = Time.time;
            }
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