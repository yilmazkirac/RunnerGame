using UnityEngine;

public class CoinTransform : MonoBehaviour
{
    [SerializeField] private CoinPool _coinPool;
    [SerializeField] private Transform[] _coinTransform;
    private void OnEnable()
    {
        SetCoinTransform();
    }
    private void SetCoinTransform()
    {
        foreach (var item in _coinTransform)
        {
            foreach (var item2 in _coinPool.Coins)
            {
                if (!item2.activeInHierarchy)
                {
                    item2.transform.position = item.position;
                    item2.SetActive(true);
                    break;
                }
            }
        }
    }
}
