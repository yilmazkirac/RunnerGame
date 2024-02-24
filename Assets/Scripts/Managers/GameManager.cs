using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    [Header("CHARACTER----------------")]
    [SerializeField] private Transform _spawner;

    public CharacterStats Player;

    [Header("GAME----------------")]
    [SerializeField] private CoinPool _coinPool;

    private int random;
    private int currentRandom;

    public float RunTime;

    [Header("SOUND----------------")]
    public AudioSource GameSound;
    public AudioSource CoinSound;
    public AudioSource FinishSound;
    private void OnEnable()
    {
        Subscribe();
    }
    private void Subscribe()
    {
        CoreGameSignals.Instance.onRestartGame += OnRestartGame;
        CoreGameSignals.Instance.onStartGame += OnStartGame;
    }
    public void OnStartGame()
    {
        GameSound.Play();
    }
    public void OnRestartGame()
    {
        foreach (var item in _coinPool.Coins)
        {
            item.transform.position = Vector3.zero;
            item.transform.rotation = Quaternion.Euler(0,0,0);
            item.gameObject.SetActive(false);
        }
        SetWay();
        Player.transform.position = _spawner.transform.position;
        RunTime = 0;
    }  
    private void SetWay()
    {
        random = Random.Range(0, WayManager.Instance.WayPrefab.Length);
        currentRandom = random;
        WayManager.Instance.WayPrefab[random].transform.position = new Vector3(0, 0, 130);
        WayManager.Instance.WayPrefab[random].SetActive(true);

        while (random == currentRandom)
        {
            random = Random.Range(0, WayManager.Instance.WayPrefab.Length);
        }
        WayManager.Instance.WayPrefab[random].transform.position = new Vector3(0, 0, 280);
        WayManager.Instance.WayPrefab[random].SetActive(true);
    }
    void Start()
    { 
        if (!PlayerPrefs.HasKey("Point"))
        {
            PlayerPrefs.SetInt("Point",0);
        }
        SetWay();
    }
    private void Update()
    {
        if (Player.Speed>0)
        {
            RunTime += Time.deltaTime;
        }
        if (RunTime > 30&& RunTime <= 59)
        {
            Player.Speed = 20;
        }
        else if (RunTime > 60)
        {
            Player.Speed = 25;
        }
    }  
}
