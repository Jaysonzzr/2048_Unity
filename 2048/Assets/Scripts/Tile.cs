using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;

    void Start()
    {
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        SetValue(value);  // 初始化时设置数字
    }

    public void SetValue(int newValue)
    {
        value = newValue;
        valueText.text = value.ToString();

        // 根据Tile的值，可以在这里更改Tile的颜色或其他属性
        GetComponent<UnityEngine.UI.Image>().color = GetColorBasedOnValue(value);
    }

    private Color GetColorBasedOnValue(int value)
    {
        // 根据Tile的数值返回不同的颜色
        switch (value)
        {
            case 2: return Color.yellow;
            case 4: return Color.red;
            case 8: return Color.green;
            // 继续添加更多颜色映射
            default: return Color.white;
        }
    }
}
