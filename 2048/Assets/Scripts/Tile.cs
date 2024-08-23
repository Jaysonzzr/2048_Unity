using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public int value;
    public Text valueText;

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.text = value.ToString();
    }
}
