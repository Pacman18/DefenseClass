using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Slider를 상속한 예제. 커스텀 에디터는 SliderEditor를 확장합니다.
/// </summary>
public class CustomSlider : Slider
{
    [SerializeField]
    bool _showValueLabel = true;

    [SerializeField]
    string _valueFormat = "F1";

    public bool ShowValueLabel => _showValueLabel;
    public string ValueFormat => _valueFormat;
}
