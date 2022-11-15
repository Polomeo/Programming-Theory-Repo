using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelectButton : MonoBehaviour
{
    public GameObject Tower;

    private GameManager gameManager;
    private Button button;
    [SerializeField] private TextMeshProUGUI towerSelectButtonText;
    [SerializeField] private string towerName;
    [SerializeField] private string towerCost;

    // Start is called before the first frame update
    void Start()
    {
        // --- DEPENDENCIES --- //
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        button = GetComponent<Button>();

        Tower towerData = Tower.GetComponent<Tower>();
        // Debug.Log(towerData.GetTowerName());

        // --- FUNCTIONALITY --- //
        button.onClick.AddListener(SelectTower);
        towerSelectButtonText.SetText(towerName + "\n" + towerCost);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectTower()
    {
        gameManager.SelectTower(Tower);
    }
}
