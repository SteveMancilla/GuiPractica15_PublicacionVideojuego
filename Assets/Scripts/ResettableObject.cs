using UnityEngine;

/// <summary>
/// Guarda la posición y rotación inicial del objeto
/// y permite restaurarlas cuando se llama a ResetState().
/// </summary>
public class ResettableObject : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    public void ResetState()
    {
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}