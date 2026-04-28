using UnityEngine;
using TMPro; // TextMeshPro kullanmak için gerekli

public class UIManager : MonoBehaviour
{
    public TMP_Text healthText;

    // "Can Deðiþti" haberini dinle
    private void OnEnable()
    {
        GameEventManager.OnCoreHealthChanged += UpdateHealthUI;
    }

    // haber dinlemeyi durdur, oyun bittiðinde artýk bu eventi dinlememize gerek yok
    private void OnDisable()
    {
        GameEventManager.OnCoreHealthChanged -= UpdateHealthUI;
    }

    // coreController dan gelen yeni can deðerini al ve UI'ý güncelle
    private void UpdateHealthUI(int newHealth)
    {
        if (healthText != null)
        {
            healthText.text = "Core Health: " + newHealth.ToString();
        }
    }
}