using UnityEngine;

/// <summary>
/// Permite arrastrar el prisma con el mouse,
/// hacer snap a una grilla y rotarlo con una tecla.
/// Requiere un collider 2D para detectar el clic.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DraggablePrism : MonoBehaviour
{
    [Header("Snapping")]
    public float gridSize = 0.5f;

    [Header("Rotación")]
    public KeyCode rotateKey = KeyCode.R;
    public float rotationStep = 45f;

    private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 dragOffset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Rotación mientras el objeto está seleccionado o en cualquier momento
        if (Input.GetKeyDown(rotateKey))
        {
            transform.Rotate(0f, 0f, rotationStep);
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera == null) return;

        isDragging = true;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        dragOffset = transform.position - mouseWorldPos;
    }

    private void OnMouseDrag()
    {
        if (!isDragging || mainCamera == null) return;

        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        transform.position = mouseWorldPos + dragOffset;
    }

    private void OnMouseUp()
    {
        if (!isDragging) return;

        isDragging = false;

        // Snap a grilla
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x / gridSize) * gridSize;
        pos.y = Mathf.Round(pos.y / gridSize) * gridSize;
        transform.position = pos;
    }
}