using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    public int value;
    public TextMeshProUGUI valueText;
    public Image tileImage;

    void Start()
    {
        valueText = GetComponentInChildren<TextMeshProUGUI>();
        SetValue(value);  // 初始化时设置数字
    }

    public void SetValue(int newValue)
    {
        value = newValue;

        if (value == 0)
        {
            // 如果值为0，隐藏文本并设置透明度为0
            valueText.text = "";
            SetTileTransparency(0);
        }
        else
        {
            // 设置文本内容并显示Tile
            valueText.text = value.ToString();
            SetTileTransparency(1);

            // 根据Tile的值调整字体大小
            AdjustFontSize();

            // 根据Tile的值，可以在这里更改Tile的颜色或其他属性
            tileImage.color = GetColorBasedOnValue(value);
        }
    }

    private void SetTileTransparency(float alpha)
    {
        // 修改Image的透明度
        Color color = tileImage.color;
        color.a = alpha;
        tileImage.color = color;
    }

    private void AdjustFontSize()
    {
        if (value < 100)
        {
            valueText.fontSize = 72; // 一/两位数
        }
        else if (value < 1000)
        {
            valueText.fontSize = 52; // 三位数
        }
        else
        {
            valueText.fontSize = 40; // 四位数及以上
        }
    }

    private Color GetColorBasedOnValue(int value)
    {
        // 根据Tile的数值返回不同的颜色
        switch (value)
        {
            case 2: return new Color(0.93f, 0.89f, 0.85f, 1);
            case 4: return new Color(0.93f, 0.88f, 0.78f, 1);
            case 8: return new Color(0.95f, 0.69f, 0.47f, 1);
            case 16: return new Color(0.96f, 0.58f, 0.39f, 1);
            case 32: return new Color(0.96f, 0.48f, 0.37f, 1);
            case 64: return new Color(0.96f, 0.37f, 0.23f, 1);
            case 128: return new Color(0.93f, 0.81f, 0.45f, 1);
            case 256: return new Color(0.93f, 0.80f, 0.38f, 1);
            case 512: return new Color(0.93f, 0.78f, 0.31f, 1);
            case 1024: return new Color(0.93f, 0.76f, 0.24f, 1);
            case 2048: return new Color(0.93f, 0.75f, 0.16f, 1);
            default: return Color.white;
        }
    }
}
