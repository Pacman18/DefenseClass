using UnityEngine;

/// <summary>
/// EditorGUILayout 레이아웃·조건부 그리기 교육용. 에디터는 InspectorLayoutExampleEditor 참고.
/// </summary>
public class InspectorLayoutExample : MonoBehaviour
{
    [SerializeField]
    bool _useAdvanced;

    [SerializeField]
    float _minValue;

    [SerializeField]
    float _maxValue = 10f;

    [SerializeField]
    LayerMask _layerMask;

    public bool UseAdvanced => _useAdvanced;
    public float MinValue => _minValue;
    public float MaxValue => _maxValue;
    public LayerMask LayerMask => _layerMask;
}
