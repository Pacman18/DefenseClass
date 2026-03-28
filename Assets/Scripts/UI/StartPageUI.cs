using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartPageUI : UIBase
{
    [SerializeField]

    Button _startBtn;

    public void SetButtonEvent(UnityAction action)
    {
        _startBtn.onClick.AddListener(action);
        _startBtn.onClick.AddListener(Clear);        
    }   

}
