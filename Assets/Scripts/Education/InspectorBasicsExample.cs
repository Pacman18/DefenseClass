using UnityEngine;

/// <summary>
/// 인스펙터 기본 패턴 교육용(런타임 데이터만). 에디터는 InspectorBasicsExampleEditor 참고.
/// </summary>
public class InspectorBasicsExample : MonoBehaviour
{
    [SerializeField]
    string _description;

    [SerializeField]
    int _repeatCount = 3;

    [SerializeField]
    AnimationCurve _curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public string Description => _description;
    public int RepeatCount => _repeatCount;
    public AnimationCurve Curve => _curve;
}
