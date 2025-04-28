using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera; 
    
    [SerializeField] float edgeThickness = 10f;      // How close to the edge before movement starts
    [SerializeField] float moveSpeed = 10f;          // Movement speed
    [SerializeField] float borderPadding = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        Vector3 direction = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        // Horizontal (X-axis)
        if (mousePos.x <= edgeThickness + borderPadding)
            direction.z = 1;
        else if (mousePos.x >= Screen.width - edgeThickness - borderPadding)
            direction.z = -1;

        // Vertical (Z-axis)
        if (mousePos.y <= edgeThickness + borderPadding)
            direction.x = -1;
        else if (mousePos.y >= Screen.height - edgeThickness - borderPadding)
            direction.x = 1;

        // Apply movement
        _camera.transform.Translate(direction.normalized * (moveSpeed * Time.deltaTime), Space.World);
        
        _camera.transform.position = Mathf.Clamp(_camera.transform.position.x, -10f, 10f) * Vector3.right +
                                     Mathf.Clamp(_camera.transform.position.y, -10f, 10f) * Vector3.up +
                                     Mathf.Clamp(_camera.transform.position.z, -10f, 10f) * Vector3.forward;
    }
}
