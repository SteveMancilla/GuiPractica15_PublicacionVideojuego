using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class LightReceiver : MonoBehaviour
{
    [Header("Visual")]
    public Color idleColor = Color.gray;
    public Color litColor = Color.green;

    private SpriteRenderer sr;

    // flags para este frame
    private bool wasHitThisFrame = false;
    private bool isLit = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = idleColor;
    }

    private void LateUpdate()
    {
        // Al final del frame, actualizamos el estado real
        isLit = wasHitThisFrame;
        sr.color = isLit ? litColor : idleColor;

        // reset para el próximo frame
        wasHitThisFrame = false;
    }

    /// <summary>
    /// Llamado por el sistema de rayos cuando el haz toca el receptor.
    /// </summary>
    public void RegisterHit()
    {
        wasHitThisFrame = true;

        // Aquí podríamos notificar al GameManager cuando se activa
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnReceiverActivated(this);
        }
    }

    public bool IsLit()
    {
        return isLit;
    }
}