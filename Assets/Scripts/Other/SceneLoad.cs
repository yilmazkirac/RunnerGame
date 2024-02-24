using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject _dontDestroy;

    private void Start()
    {
        DontDestroyOnLoad(_dontDestroy);
        SceneManager.LoadScene(1);
    }
}
