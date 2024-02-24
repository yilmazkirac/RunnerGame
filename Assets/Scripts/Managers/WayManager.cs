using UnityEngine;

public class WayManager : MonoBehaviour
{
    public static WayManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public GameObject[] WayPrefab;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CoreGameSignals.Instance.onRestartGame += OnRestartGame;
    }

    public void OnRestartGame()
    {
        foreach (var item in WayPrefab)
        {
            item.transform.position = Vector3.zero;
            item.SetActive(false);
        }
    }
}
