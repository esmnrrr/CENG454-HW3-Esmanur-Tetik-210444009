using UnityEngine;

public class CoreController : MonoBehaviour, IDamageable
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Hasar alma metodu
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Çekirdek hasar aldý! Kalan Can: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("GAME OVER! Çekirdek Yok Oldu!");
        FindAnyObjectByType<GameManager>().GameOver();
        gameObject.SetActive(false);
    }
}