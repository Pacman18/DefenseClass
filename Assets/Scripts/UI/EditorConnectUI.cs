using UnityEngine;
using TMPro;

public class EditorConnectUI : UIBase
{

    [SerializeField]
    private TextMeshProUGUI _text;

    public void SetUIData(string text)
    {
        _text.text = text;
    }    
}
