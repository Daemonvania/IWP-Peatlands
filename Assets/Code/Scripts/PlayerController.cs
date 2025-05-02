using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera; 
    
    [SerializeField] float edgeThickness = 10f;      // How close to the edge before movement starts
    [SerializeField] float moveSpeed = 10f;          // Movement speed
    [SerializeField] float borderPadding = 0f;
    [SerializeField] float zoomSpeed = 15f;           // Speed of zooming
    [SerializeField] float minZoom = 5f;             // Minimum zoom distance
    [SerializeField] float maxZoom = 50f;            // Maximum zoom distance
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        CameraMovement();
        CameraZoom();
    }

    private void CameraMovement()
    {
        Vector3 direction = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        // Edge Scrolling
        if (mousePos.x <= edgeThickness + borderPadding)
            direction.z = 1;
        else if (mousePos.x >= Screen.width - edgeThickness - borderPadding)
            direction.z = -1;

        if (mousePos.y <= edgeThickness + borderPadding)
            direction.x = -1;
        else if (mousePos.y >= Screen.height - edgeThickness - borderPadding)
            direction.x = 1;

        // Apply movement
        _camera.transform.Translate(direction.normalized * (moveSpeed * Time.deltaTime), Space.World);
    
        _camera.transform.position = Mathf.Clamp(_camera.transform.position.x, -10f, 10f) * Vector3.right +
                                     Mathf.Clamp(_camera.transform.position.y, -10f, 10f) * Vector3.up +
                                     Mathf.Clamp(_camera.transform.position.z, -10f, 6) * Vector3.forward;

      
    }
    
    private void CameraZoom()
    {
        //todo fix moving after reaching max (clamp zoom in all directions.?)
        float scroll = Mouse.current.scroll.ReadValue().y;
        Vector3 zoom = _camera.transform.position + _camera.transform.forward * (scroll * zoomSpeed * Time.deltaTime);
        zoom.y = Mathf.Clamp(zoom.y, minZoom, maxZoom);
        _camera.transform.position = zoom;
    }

}