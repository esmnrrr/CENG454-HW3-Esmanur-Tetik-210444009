using UnityEngine;

public class CoreController : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        // baskar baslamaz cani ui a bildiriyoruz
        GameEventManager.CoreHealthChanged(currentHealth);
    }

    // Hasar alma metodu
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        //Debug.Log("Çekirdek hasar aldý! Kalan Can: " + currentHealth);

        // ortaliga haber saliyoruz, can degisti ilgilenenler duysun diye
        GameEventManager.CoreHealthChanged(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("GAME OVER! Çekirdek Yok Oldu!");
        //FindAnyObjectByType<GameManager>().GameOver();

        // ortaliga haber saliyoruz, ben yok oldum ilgilenenler duysun diye
        GameEventManager.CoreDestroyed();
        gameObject.SetActive(false);
    }
}
