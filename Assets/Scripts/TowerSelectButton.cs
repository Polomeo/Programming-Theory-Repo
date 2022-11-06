using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerSelectButton : MonoBehaviour
{
    public GameObject Tower;

    private Button button;
    [SerializeField] private TextMeshProUGUI towerSelectButtonText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        button = GetComponent<Button>();

        button.onClick.AddListener(SelectTower);

        towerSelectButtonText.SetText(Tower.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectTower()
    {
        gameManager.SelectTower(Tower);
        Debug.Log(Tower.gameObject.name + " selected.");
    }
}
