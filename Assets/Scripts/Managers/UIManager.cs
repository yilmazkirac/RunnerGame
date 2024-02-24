using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CoreGameSignals.Instance.onStartGame += OnStartGame;
    }

    private void OnStartGame()
    {
        ScoreText.CrossFadeColor(new Color(1, 1, 1, 1), 0.5f, true, true);
        InGamePanel.SetActive(true);
        _startPanel.SetActive(false);
    }

    [Header("PANELS----------------")]
    [SerializeField] private GameObject _startPanel;
    public GameObject RestartPanel;
    public GameObject InGamePanel;

    [Header("TEXTS----------------")]
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HealtText;
    public TextMeshProUGUI BestScoreText;
    public TextMeshProUGUI ScoreText2;

    [Header("BUTTONS----------------")]
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;

    private void Start()
    {
        _restartButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _restartButton.GetComponent<Button>().onClick.AddListener(() => { CoreGameSignals.Instance.onRestartGame?.Invoke(); });
        _startButton.GetComponent<Button>().onClick.RemoveAllListeners();
        _startButton.GetComponent<Button>().onClick.AddListener(() => { CoreGameSignals.Instance.onStartGame?.Invoke(); });

        RestartGame();

    }
    private void Update()
    {
        if (GameManager.Instance.Player.Point>PlayerPrefs.GetInt("Point"))
        {
            ScoreTextCangeColor();
        }
    }

    public void RestartGame()
    {
        InGamePanel.SetActive(false);
        RestartPanel.SetActive(false);
        _startPanel.SetActive(true);
    }
    private void ScoreTextCangeColor()
    {
        ScoreText.CrossFadeColor(new Color(0, 1, 0, 1), 0.5f, true, true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
