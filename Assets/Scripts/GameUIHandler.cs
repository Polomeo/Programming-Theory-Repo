using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject playUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI currencyText;


    private void Start()
    {
        // Game Over text starts hidden
        gameOverUI.gameObject.SetActive(false);

        // Starting currency
        UpdateCurrencyText(GameManager.Instance.PlayerCurrency.ToString());
    }

    public void UpdateCurrencyText(string text)
    {
        currencyText.SetText("Suns: " + text);
    }
    public void ShowGameOverUI()
    {
        playUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        // Re-loads the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        // Loads the Menu Scene
        SceneManager.LoadScene(0);
    }
}
