using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class BeamEmitter : MonoBehaviour
{
    [Header("Beam Settings")]
    public float maxDistance = 20f;
    public int maxReflections = 5;
    public LayerMask obstacleMask;

    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawBeam();
    }

    void DrawBeam()
    {
        List<Vector3> beamPoints = new List<Vector3>();

        Vector2 origin = transform.position;
        Vector2 direction = transform.right;

        beamPoints.Add(origin);

        for (int i = 0; i < maxReflections; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, maxDistance, obstacleMask);

            if (hit.collider)
            {
                beamPoints.Add(hit.point);

                // ¿Es un receptor?
                LightReceiver receiver = hit.collider.GetComponent<LightReceiver>();
                if (receiver != null)
                {
                    receiver.RegisterHit();
                    // El rayo termina aquí
                    break;
                }

                // ¿Es reflectivo?
                ReflectiveSurface reflector = hit.collider.GetComponent<ReflectiveSurface>();
                if (reflector != null)
                {
                    // Calcular dirección reflejada
                    direction = Vector2.Reflect(direction, hit.normal);
                    origin = hit.point + direction * 0.01f; // pequeño offset para evitar que se quede pegado
                    continue;
                }
                else
                {
                    // No reflectivo → termina aquí
                    break;
                }
            }
            else
            {
                // No golpeó nada → rayo en línea recta
                beamPoints.Add(origin + direction * maxDistance);
                break;
            }
        }

        lr.positionCount = beamPoints.Count;
        for (int p = 0; p < beamPoints.Count; p++)
        {
            lr.SetPosition(p, beamPoints[p]);
        }
    }
}