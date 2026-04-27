using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float surviveTime = 30f;
    private bool gameEnded = false;

    void Update()
    {
        if (gameEnded) return;

        // Geri sayým
        surviveTime -= Time.deltaTime;
        if (surviveTime <= 0)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        if (gameEnded) return;
        gameEnded = true;
        Debug.Log("LOSE! Çekirdek yok oldu, savunma çöktü!");
    }

    private void WinGame()
    {
        gameEnded = true;
        Debug.Log("WIN! 30 Saniye dayandýn, Çekirdek güvende!");
    }
}