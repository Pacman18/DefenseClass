using UnityEngine;
using UnityEngine.Events;


public class UIBase : MonoBehaviour
{
    // 닫힐때 실행할 함수
    private UnityAction<UIBase> _onClose;

    public void SetOnClose(UnityAction<UIBase> onClose)
    {
        _onClose = onClose;
    }


    public void Clear()
    {
        if(_onClose != null)
        {
            _onClose(this);
        }
    }
}
