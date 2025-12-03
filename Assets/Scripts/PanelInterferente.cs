using UnityEngine;

public class PanelInterferente : MonoBehaviour
{
    [Header("Timing")]
    public float openTime = 1.5f;   // tiempo que permanece abierto
    public float closedTime = 1f;   // tiempo que permanece cerrado
    public bool startsClosed = true;

    private SpriteRenderer sr;
    private Collider2D col;

    private float timer = 0f;
    private bool isClosed;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        isClosed = startsClosed;
        ApplyState();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isClosed && timer >= closedTime)
        {
            // abrir
            isClosed = false;
            timer = 0f;
            ApplyState();
        }
        else if (!isClosed && timer >= openTime)
        {
            // cerrar
            isClosed = true;
            timer = 0f;
            ApplyState();
        }
    }

    private void ApplyState()
    {
        if (isClosed)
        {
            // cerrado = bloquea rayos
            col.enabled = true;
            sr.color = new Color(1f, 0.4f, 0.4f); // rojo claro
        }
        else
        {
            // abierto = no bloquea rayos
            col.enabled = false;
            sr.color = new Color(0.4f, 1f, 0.4f); // verde claro
        }
    }
}