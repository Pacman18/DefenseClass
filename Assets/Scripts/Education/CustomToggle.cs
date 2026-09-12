using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Toggle을 상속한 예제. 커스텀 에디터는 ToggleEditor를 확장합니다.
/// </summary>
public class CustomToggle : Toggle
{
    [SerializeField]
    Color _onTint = Color.white;

    [SerializeField]
    [TextArea(2, 4)]
    string _onStateTooltip;

    public Color OnTint => _onTint;
    public string OnStateTooltip => _onStateTooltip;
}
