using UnityEngine;

public class EndWay : MonoBehaviour
{
    public GameObject Way;
    [SerializeField] private Car[] _cars;
    public void Reset()
    {
        if (_cars.Length > 0)
        {
            foreach (var item in _cars)
            {
                item.Reset();
            }
        }
        Way.SetActive(false);
    }
}
