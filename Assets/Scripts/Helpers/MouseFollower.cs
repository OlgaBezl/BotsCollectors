using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _planeTransform;
    
    public bool IsActive {get; private set;}

    private Plane _plane;

    private void Start()
    {
        if (_planeTransform != null)
        {
            _plane = new Plane(Vector3.down, _planeTransform.position.y);
        }
    }

    private void Update()
    {
        if (IsActive)
        {
            UpdatePosition();
        }
    }

    private void OnDisable()
    {
        IsActive = false;
    }

    public void SetParameters(Camera camera, Transform planeTransform)
    {
        _camera = camera;
        _planeTransform = planeTransform;

        Start();
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    public void UpdatePosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (_plane.Raycast(ray, out float distance))
        {
            transform.position = ray.GetPoint(distance);
        }
    }
}