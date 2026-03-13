using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIContent;
using UnityEngine.Events;
using System;
using System.Reflection;

namespace Santa
{
    public enum SCENENAME
    {
        Load,
        LobbyScene,
        GameScene,
    }



    public class SantaSceneManager : MonoSingleton<SantaSceneManager>
    {    
        public bool InputEnable
        {
            get { return _inputEnable; }
            set { _inputEnable = value; }
        }

        private bool _isStoryInputLock = false;
        public bool IsStoryInputLock
        {
            get { return _isStoryInputLock; }
        }

        static public SantaSceneManager Instance
        {
            get { return i; }
        }

        private bool _inputEnable = true;

        private UnityAction<KeyCode> _onInpuKeyUpdate = null;

        #region  Loading 

        private IEnumerator EnterLoad()
        {
            var data = new LoadingPopupData();
            data.Number = 10;
            UIManager.i.CreatePopupUI<LoadingPopup>(data);
            
            Debug.Log("EnterLoad");

            yield return null;
        }

        private IEnumerator ExitLoad()
        {
            
            Debug.Log("ExitLoad");

            UIManager.i.RemovePopup<LoadingPopup>();            
            yield return null;
        }
        #endregion    


        private IEnumerator EnterLobbyScene()
        {

            Debug.Log("EnterLobbyScene");
            yield return null;

        }

        private void UpdateLobbyScene()
        {
            
        }

        private IEnumerator ExitLobbyScene()
        {
            Debug.Log("ExitLobbyScene");            
            yield return null;
        }

        // Enter은 Component의 Start보다 늦게 불린다. 
        private IEnumerator EnterGameScene()
        {          
            Debug.Log("EnterGameScene");
            yield break;
        }

        private void UpdateGameScene()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameManager.Instance.StartGame();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                Debug.Log("F2 Key Pressed");
            }           
        }

        private IEnumerator ExitGameScene()
        {
            Debug.Log("ExitGameScene");            
            yield break;
        }

        private delegate void SceneChangeClearEvent();     

        // 유한상태기계 본체 
        public class SceneState
        {
            public Action DoUpdate = DoNothing;
            public Action DoFixedUpdate = DoNothing;
            public Func<IEnumerator> preSceneLoad = DoNothingCoroutine; 
            public Func<IEnumerator> EnterScene = DoNothingCoroutine; 
            public Func<IEnumerator> exitScene = DoNothingCoroutine;
            public Enum currentState;
        }    

        protected LoadingPopup _loadUI;

        private bool _testMode = false;

        [HideInInspector]
        public SceneState State = new SceneState();        

        [HideInInspector]
        public Enum LastState; // 이전 상태                

        private float timeEnteredState; // 스테이트 입장시간 

        // 현재 상태의 지난시간 알기 
        public float TimeInCurrentState
        {
            get
            {
                return Time.time - timeEnteredState;
            }
        }

        private const string LoadSceneName = "Load";

        /// <summary>
        /// StartScene에서 사용되는 변수  ( 항상 Scene이 새로 로드되면 true로 변환된다 )
        /// StartScene을 선언하지 않으면 사용할 필요가 없다 . 
        /// 이펙트 로드 혹은 네트워크를 기다려야할때 사용할것 
        /// </summary>
        public bool Waiting = false;

        public void ReadyComplete()
        {
            Waiting = false;
        }

        // 같은 씬으로 전환되도 다른 임의 식별자가 필요함으로 만든 변수
        protected int SpecIndex;

        public Enum CurrentState
        {
            get
            {
                return State.currentState;
            }                   
        }


        private SceneChangeClearEvent _clearEvent;

        public void AddClearListener(Action listener)
        {
            _clearEvent += delegate { listener.Invoke(); };   
        }

        /// <summary>    
        /// 씬을 로드할때 쓰이는 함수 
        /// </summary>
        /// <param name="loadScene"></param>
        public void SceneLoad(Enum loadScene , int index = 0)
        {
            // 현재 FSM이 같고 상태도 같으면 return  
            if (State.currentState == loadScene)
                return;

            // Exit Scene부터는 이 루프를 알수 있게된다 
            SpecIndex = index;

            StartCoroutine(SceneNowLoadiong(loadScene));        

            //Debug.Log("Scene Loaded Complete");
        }

        private IEnumerator SceneNowLoadiong(Enum loadScene)
        {   
            // 업데이트에서 아무것도 안하게 바꿔놓고 
            State.DoUpdate = DoNothing;

            // 이전 Exit  실행 
            if (State.exitScene != null)
            {
                Debug.Log("ExitCoroutine Scene Load + " + State.currentState);
                yield return StartCoroutine(State.exitScene());
            }


            ChangingState(SCENENAME.Load);
            // 로딩 씬으로 바꿈 
            yield return StartCoroutine(EmptyLoadScene()); 
            

            // 로딩씬 끝나고 현재 씬으로 바꿔줌 
            ChangingState(loadScene);        


            yield return StartCoroutine(LoadChangeScene(loadScene));        

            //Debug.Log("loading Done");
        }

        /// <summary>
        /// 이전 스테이트 저장 및 현재스테이트 바꿔줌
        /// </summary>
        void ChangingState(Enum loadScene)
        {
            Debug.Log("Change Scene State : " + loadScene + ", PrevState : " + State.currentState);

            LastState = State.currentState;
            State.currentState = loadScene;
            timeEnteredState = Time.time;        
        }

        protected void Awake()
        {
            print("SCENE FSM Awake");        

            DontDestroyOnLoad(gameObject);
        }

        static IEnumerator DoNothingCoroutine()
        {
            yield break;
        }

        static void DoNothing()
        {
        }

        private Dictionary<Enum, Dictionary<string, Delegate>> _cache = new Dictionary<Enum, Dictionary<string, Delegate>>();


        private AsyncOperation _progress;

        // 원하는 씬 로드 
        private IEnumerator LoadChangeScene(Enum loadScene)
        {
            // 그럼 다음 씬을 로드한다
            var loadProgress = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadScene.ToString());

            while (!loadProgress.isDone)
            {
                if (_loadUI != null)
                    _loadUI.SetUIData((int)loadProgress.progress * 100);

                yield return null;
            }        

            // PapareLoad에서는 모든 이펙트 혹은 리소스를 로드한다 
            State.preSceneLoad = ConfigureDelegate<Func<IEnumerator>>("Prepare", DoNothingCoroutine);

            if (State.preSceneLoad != null)
            {
                // 로딩 팝업 만들어진다
                yield return StartCoroutine(State.preSceneLoad());
            }

                    
            State.EnterScene = ConfigureDelegate<Func<IEnumerator>>("Enter", DoNothingCoroutine);        
            State.DoUpdate = ConfigureDelegate<Action>("Update", DoNothing); 
            State.DoFixedUpdate = ConfigureDelegate<Action>("FixedUpdate", DoNothing);
            State.exitScene = ConfigureDelegate<Func<IEnumerator>>("Exit", DoNothingCoroutine);            

            if (State.EnterScene != null)
            {
                // Enter 시작
                yield return StartCoroutine(State.EnterScene());
            }
        }

        // 로딩부분 고치는 것 
        private IEnumerator EmptyLoadScene()
        {
            // EnterLoad 시작 
            State.EnterScene = ConfigureDelegate<Func<IEnumerator>>("Enter", DoNothingCoroutine);            

            if (State.EnterScene != null)
            {
                yield return StartCoroutine(State.EnterScene());
            }

            UIManager.i.ClearPopupList();
            UIManager.i.RemoveAllPagePopup();

            // 로딩 씬 로드
            AsyncOperation loading = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LoadSceneName);

            _loadUI = UIManager.i.GetPopComponent<LoadingPopup>();


            // 로딩 페이지 테스트 
            if(_testMode)
            {
                float waitngTime = 1;
                float accTime = 0;

                while (accTime < waitngTime)
                {
                    if (_loadUI != null)
                    {
                        float progress = accTime / waitngTime;
                        _loadUI.SetUIData((int)(progress * 100));
                    }

                    accTime += Time.deltaTime;

                    yield return null;
                }

            }
            else
            {
                while (!loading.isDone)
                {
                    if (_loadUI != null)
                        _loadUI.SetUIData((int)loading.progress * 100);

                    yield return null;
                }
            }

            State.exitScene = ConfigureDelegate<Func<IEnumerator>>("Exit", DoNothingCoroutine);            

            if (State.exitScene != null)
            {
                // ExitLoad 시작
                yield return StartCoroutine(State.exitScene());                
            }            
        }

        T ConfigureDelegate<T>(string methodRoot, T Default) where T : class
        {   

            Dictionary<string, Delegate> lookup;

            //Debug.Log("Prev : " + State.currentState + ", method : " + methodRoot);

            // not Exist  new 
            if (!_cache.TryGetValue(State.currentState, out lookup))
            {
                _cache[State.currentState] = lookup = new Dictionary<string, Delegate>();
            }

            Delegate returnValue;            

            if (!lookup.TryGetValue(methodRoot, out returnValue))
            {
                GlobalUtil.AddString(methodRoot, State.currentState.ToString());

                //Debug.Log(State.currentState + ", method : " + methodRoot);

                MethodInfo mtd = GetType().GetMethod(GlobalUtil.GetTextString, System.Reflection.BindingFlags.Instance | 
                                                System.Reflection.BindingFlags.Public |
                                                System.Reflection.BindingFlags.NonPublic |
                                                System.Reflection.BindingFlags.InvokeMethod);

                if (mtd != null)
                {
                    returnValue = System.Delegate.CreateDelegate(typeof(T), this, mtd);
                }
                else
                {
                    returnValue = Default as Delegate;
                }

                lookup[methodRoot] = returnValue;
            }

            return returnValue as T;
        }

        #region Pass On Methods

        void Update()
        {
            State.DoUpdate();

            //Debug.Log(CurrentState);
        }

        void FixedUpdate()
        {
            State.DoFixedUpdate();
        }
        #endregion


        #region UIScene

        private IEnumerator EnterUIScene()
        {
            Debug.Log("EnterUIScene");
            yield break;
        }

        private IEnumerator ExitUIScene()
        {
            Debug.Log("ExitUIScene");
            yield break;
        }
        
        #endregion

    }
}