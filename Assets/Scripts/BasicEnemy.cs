using UnityEngine;

// Düþman da mermilerimizden hasar alacaðý için IDamageable kullanýyor!
public class BasicEnemy : MonoBehaviour, IDamageable
{
    public Transform targetCore;
    public float speed = 2f;
    public int health = 40;

    void Update()
    {
        if (targetCore != null)
        {
            // Çekirdeðe doðru yürü
            transform.position = Vector3.MoveTowards(transform.position, targetCore.position, speed * Time.deltaTime);

            // Çekirdeðe yeterince yaklaþtýysa patla ve hasar ver
            if (Vector3.Distance(transform.position, targetCore.position) < 1.5f)
            {
                targetCore.GetComponent<IDamageable>()?.TakeDamage(20);
                Destroy(gameObject);
            }
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