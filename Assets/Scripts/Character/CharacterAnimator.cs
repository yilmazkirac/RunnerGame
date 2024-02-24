using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        Subscribe();
    }

    private void Subscribe()
    {
        CoreGameSignals.Instance.onRestartGame += OnRestartGame;
        CoreGameSignals.Instance.onStartGame += OnStartGame;
    }

    public void OnRestartGame()
    {
        _animator.SetBool("IsStart",false);    
    }
    public void OnStartGame()
    {
        _animator.SetBool("IsStart", true);
    }

    public void CrossFade(string animName, float duration)
    {
        _animator.CrossFade(animName, duration);
    }
}
