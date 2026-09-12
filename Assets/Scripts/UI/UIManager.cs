using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Transform _popupTransform;

    [SerializeField]
    private Transform _hudTransform;

    Dictionary<Type, UIBase> _uiContainer = new Dictionary<Type, UIBase>();

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance  = Instantiate(Resources.Load<UIManager>("Prefab/UI/UIManager"));
            }

            return _instance;
        }
    }
    private static UIManager _instance;    

    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private const string UIPATH = "Prefab/UI/";
    public T CreateUI<T>() where T : UIBase
    {
        UIBase resui = null;

        resui = Resources.Load<UIBase>(UIPATH + typeof(T).ToString());

        UIBase comp = Instantiate(resui);
        comp.transform.SetParent(_popupTransform, false);

        comp.SetOnClose(RemoveUI);

        _uiContainer.Add(typeof(T), comp);

        return comp as T;
    }

    public T GetUI<T>() where T : UIBase
    {
        if(_uiContainer.ContainsKey(typeof(T)))
        {
            return _uiContainer[typeof(T)] as T;
        }
        return null;
    }

    public void RemoveUI(UIBase ui)
    {
        if(_uiContainer.ContainsKey(ui.GetType()))
        {
            _uiContainer.Remove(ui.GetType());
            Destroy(ui.gameObject);
        }
    }

    public void RemoveUI<T>() where T : UIBase
    {
        if (_uiContainer.ContainsKey(typeof(T)))
        {
            UIBase ui = _uiContainer[typeof(T)];
            if (ui != null)
            {
                Destroy(ui.gameObject);
                _uiContainer.Remove(typeof(T));
            }
        }

    }



}
