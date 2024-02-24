using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("STATS----------------")]
    public int Speed;
    public int JumpForce;
    public int Health;
    public float Point;

    [Header("OTHER----------------")]
    public bool MayHit;
    [SerializeField] private Material _material;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CoreGameSignals.Instance.onStartGame += OnStartGame;
        CoreGameSignals.Instance.onRestartGame += OnRestartGame;
    }

    private void OnStartGame()
    {
        Speed = 15;
        Health = 3;
        UIManager.Instance.HealtText.text = "Health: " + Health;
    }
    private void OnRestartGame()
    {
        Speed = 0;
        Point = 0;
        MayHit = false;
        _material.color = Color.white;
    }
    private void Update()
    {
        Point+= Speed * Time.deltaTime; 
        UIManager.Instance.ScoreText.text = "Score: " + (int)Point;
    }

    public void SetPoint()
    {
        if (Point>PlayerPrefs.GetInt("Point"))
        {
            PlayerPrefs.SetInt("Point", (int)Point);
        }   
    }
    public void MayHits()
    {
        MayHit = false;
    }
}
