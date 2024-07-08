using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DynamicDigits : MonoBehaviour
{
    public TextMeshProUGUI dynamicDigits;
    private float currentValue = 0;

    void Start()
    {
        dynamicDigits.text = "0";
    }

    void Update()
    {
        currentValue += Input.GetAxis("Mouse X");
        currentValue = Mathf.Clamp(currentValue, 0, 360); // limit the value between 0 and 360
        dynamicDigits.text = ((int)currentValue).ToString(); // update the text with the current value
    }
}