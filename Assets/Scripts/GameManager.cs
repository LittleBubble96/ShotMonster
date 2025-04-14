using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private static GameManager _instance;

    private UIManager uiManager;
    private AppEventDispatcher appEventDispatcher;
    private GameStateMachine gameStateMachine;

    struct TestInfo
    {
        public int hp ;
        public int mp;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        TestInfo testInfo = new TestInfo();
        testInfo.hp = 100;
        testInfo.mp = 100;
        string json = JsonUtility.ToJson(testInfo);
        Debug.Log("[Json] TestInfo: " + json);
        TestInfo testInfo2 = JsonUtility.FromJson<TestInfo>(json);
        Debug.Log("[Json] TestInfo2: " + testInfo2.hp + " " + testInfo2.mp);
    }
    
    public static GameManager Instance => _instance;
    
    private void Start()
    {
        ClientFactoryRegisterHelper.Register();

    
        uiManager = new UIManager();
        uiManager.Init();
        
        appEventDispatcher = new AppEventDispatcher();
        appEventDispatcher.Init();
        
        gameStateMachine = new GameStateMachine();
        gameStateMachine.Init();

        RoomManager.Instance.Init();
        
    
        GOtPoolManager.Instance.Init();
        EffectManager.Instance.Init();
    }
    

    
    public static UIManager GetUIManager()
    {
        return _instance.uiManager;
    }
    
    public static AppEventDispatcher GetAppEventDispatcher()
    {
        return _instance.appEventDispatcher;
    }
    
    public static GameStateMachine GetGameStateMachine()
    {
        return _instance.gameStateMachine;
    }
    

    
    private void FixedUpdate()
    {
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.DoFixedUpdate(Time.fixedDeltaTime);
        }
    }
    
    private void Update()
    {
        if (uiManager != null)
        {
            uiManager.DoUpdate(Time.deltaTime);
        }
        if (gameStateMachine != null)
        {
            gameStateMachine.DoUpdate(Time.deltaTime);
        }
        if (EffectManager.Instance != null)
        {
            EffectManager.Instance.DoUpdate(Time.deltaTime);
        }
        
        if (RoomManager.Instance != null)
        {
            RoomManager.Instance.DoUpdate(Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        if (appEventDispatcher != null)
        {
            appEventDispatcher.Dispose();
        }
        if (gameStateMachine != null)
        {
            gameStateMachine.Dispose();
        }
    }
}