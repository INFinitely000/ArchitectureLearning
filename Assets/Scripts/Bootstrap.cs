using GameState;
using Service;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour, ICoroutineHandler
{
    [field: SerializeField] public AssetData AssetData { get; private set; }

    private static Bootstrap _instance;

    private Services _services;
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        if (_instance)
            Destroy(gameObject);

        _instance = this;

        RegisterServices();
        _stateMachine = new GameStateMachine(_services);
        _stateMachine.Entry<InitialGameState>();

        DontDestroyOnLoad(this);
    }

    private void RegisterServices()
    {
        _services = new Services();
        _services.Register<IAssetData>(AssetData);
        _services.Register<IFactory>(new Factory(AssetData));
        _services.Register<IInputService>(new StandaloneInputService());
        _services.Register<ISceneLoader>(new SceneLoader(this));
    }
}