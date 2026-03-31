using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public ContainerManager containerManager;
    public Button verticalViewButton;
    public Button horizontalViewButton;
    public float orbitSpeed = 750f;
    public float zoomSpeed = 5f;
    public float panSpeed = 0.3f;
    public float minZoom = 1f;
    public float maxZoom = 30f;

    private Vector3 _target;
    private float _distance;
    private float _yaw;
    private float _pitch;
    private bool _skipOrbitFrame;
    private Vector3 _containerCenter;
    private float _initialDistance;

    void Start()
    {
        var cd = containerManager != null ? containerManager.containerData : null;
        _containerCenter = cd != null
            ? new Vector3(cd.width / 2f, cd.height / 2f, cd.depth / 2f)
            : Vector3.zero;
        _target = _containerCenter;
        _initialDistance = (transform.position - _target).magnitude;

        if (verticalViewButton != null) verticalViewButton.onClick.AddListener(SetVerticalView);
        if (horizontalViewButton != null) horizontalViewButton.onClick.AddListener(SetHorizontalView);

        Vector3 offset = transform.position - _target;
        _distance = offset.magnitude;
        _yaw = Mathf.Atan2(offset.x, offset.z) * Mathf.Rad2Deg;
        _pitch = Mathf.Asin(offset.y / (_distance > 0f ? _distance : 1f)) * Mathf.Rad2Deg;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (_skipOrbitFrame)
                _skipOrbitFrame = false;
            else
            {
                _yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
                _pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
                _pitch = Mathf.Clamp(_pitch, -89f, 89f);
            }
        }

        if (Input.GetMouseButton(2))
        {
            float dx = Input.GetAxis("Mouse X") * panSpeed;
            float dy = Input.GetAxis("Mouse Y") * panSpeed;
            _target -= transform.right * dx;
            _target -= transform.up * dy;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0f)
            _distance = Mathf.Clamp(_distance - scroll * zoomSpeed, minZoom, maxZoom);

        Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0f);
        transform.position = _target - rot * Vector3.forward * _distance;
        transform.rotation = rot;
    }

    void SetVerticalView()
    {
        _pitch = 89f;
        _yaw = 0f;
        _target = _containerCenter;
        _distance = _initialDistance;
        _skipOrbitFrame = true;
    }

    void SetHorizontalView()
    {
        _pitch = 0f;
        _yaw = 90f;
        _target = _containerCenter;
        _distance = _initialDistance;
        _skipOrbitFrame = true;
    }
}
