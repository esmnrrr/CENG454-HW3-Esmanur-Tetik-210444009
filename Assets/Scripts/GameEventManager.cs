using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    // Dinleyicilerin abone olabileceði yayýn kanallarýmýz (Events)
    public static event Action<int> OnCoreHealthChanged;
    public static event Action OnCoreDestroyed;

    // Çekirdek hasar alýnca bu metot çaðrýlacak ve tüm abonelere haber gidecek
    public static void CoreHealthChanged(int currentHealth)
    {
        OnCoreHealthChanged?.Invoke(currentHealth);
    }

    // Çekirdek yok olunca bu metot çaðrýlacak
    public static void CoreDestroyed()
    {
        OnCoreDestroyed?.Invoke();
    }
}