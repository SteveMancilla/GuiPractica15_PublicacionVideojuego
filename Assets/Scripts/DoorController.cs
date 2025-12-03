using UnityEngine;

/// <summary>
/// Controla una puerta simple que se desplaza hacia arriba al abrirse.
/// </summary>
public class DoorController : MonoBehaviour
{
    [Header("Movimiento")]
    public Vector3 openOffset = new Vector3(0f, 2f, 0f); // cuánto sube al abrir
    public float openSpeed = 3f;

    private Vector3 closedPosition;
    private Vector3 targetPosition;
    private bool isOpening = false;

    private void Awake()
    {
        closedPosition = transform.position;
        targetPosition = closedPosition;
    }

    private void Update()
    {
        if (!isOpening) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            openSpeed * Time.deltaTime
        );
    }

    public void OpenDoor()
    {
        // Solo configuramos una vez
        targetPosition = closedPosition + openOffset;
        isOpening = true;
    }

    public void ResetDoor()
    {
        isOpening = false;
        transform.position = closedPosition;
    }
}