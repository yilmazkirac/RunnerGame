using UnityEngine;

public class CoinAnim : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("False", 45f);
    }
    private void Update()
    {
        transform.Rotate(0f, 100f * Time.deltaTime, 0f);
    }
    private void False()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
