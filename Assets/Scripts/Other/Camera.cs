using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _distance;

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x + _distance.x, transform.position.y, _target.position.z + _distance.z);    
    }
}
