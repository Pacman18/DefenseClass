using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Image를 상속한 예제. 커스텀 에디터는 ImageEditor를 확장합니다.
/// </summary>
public class CustomImage : Image
{
    [SerializeField]
    Color _pulseColor = Color.white;

    [SerializeField]
    [Min(0f)]
    float _pulseSpeed = 1f;

    public Color PulseColor => _pulseColor;
    public float PulseSpeed => _pulseSpeed;
}
