using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private float decayTime = 5f;
    private int currencyValue = 25;

    // Decays over certain time and disappears
    private void Update()
    {
        Destroy(gameObject, decayTime);
    }

    // Adds 25 to player currency on click and disappears
    private void OnMouseDown()
    {
        GameManager.Instance.AddCurrency(currencyValue);
        Destroy(gameObject);
    }

    

}
