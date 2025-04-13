using Data;
using GameState;
using Service;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Bootstrap : MonoBehaviour, ICoroutineHandler
{
    [field: SerializeField] public AssetData AssetData { get; private set; }
    [field: SerializeField] public GameData GameData { get; private set; }

    private static Bootstrap _instance;

    public Services Services { get; private set; }
    public GameStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            
            RegisterServices();
            StateMachine = new GameStateMachine(Services);
            StateMachine.Entry<InitialGameState>();

            DontDestroyOnLoad(gameObject);
        }
    }

    private void RegisterServices()
    {
        Services = new Services();
        Services.Register<IGameData>(GameData);
        Services.Register<IAssetData>(AssetData);
        Services.Register<IWallet>(new Wallet(GameData.Wallet.coins, int.MaxValue));
        Services.Register<IGameFactory>(new GameFactory(AssetData));
        Services.Register<IInput>(new StandaloneInput());
        Services.Register<ISceneLoader>(new SceneLoader(this));
    }
}