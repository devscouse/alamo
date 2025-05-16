using TMPro;
using UnityEngine;

public class BarComponentUI : MonoBehaviour
{

    public RectTransform barBackground;
    public RectTransform barForeground;

    // Set a proportional value to show on the Bar UI Component. Value should be between 0 and 1
    public void SetDisplayValue(float value)
    {
        float width = value * barBackground.rect.width;
        float pos = (barBackground.rect.width - width) / 2;
        barForeground.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        barForeground.position = new Vector3(barBackground.position.x - pos, barBackground.position.y, barBackground.position.z);
    }

}
