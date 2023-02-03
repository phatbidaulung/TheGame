using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _taget;
    [SerializeField] private float _smoothTime;
    private Vector3 _velocity = Vector3.zero;
    private void Update()
    {
        MoveCameraToTaget();
    }
    private void MoveCameraToTaget()
    {
        Vector3 vctTaget= Vector3.SmoothDamp(transform.position, _taget.position, ref _velocity, _smoothTime);
        this.transform.position = new Vector3(vctTaget.x, this.transform.position.y, vctTaget.z);
    }
}
