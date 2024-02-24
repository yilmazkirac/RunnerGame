using UnityEngine;

public class Car : MonoBehaviour
{
    public bool IsStart = false;
    [SerializeField] private Transform _startPos;

    private void Update()
    {
        if (IsStart)
        {
            transform.Translate(0, 0, 20*Time.deltaTime);
            Invoke("Reset", 10f);
        }
    }

    public void Reset()
    {
        IsStart = false;
        transform.position = _startPos.position;
    }    
}
