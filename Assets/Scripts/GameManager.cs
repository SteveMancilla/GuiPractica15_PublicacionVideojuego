using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public PuzzleController activePuzzle;
    public DoorController mainDoor;
    private bool puzzleAlreadySolved = false;
    public ResettableObject[] resettableObjects;

    private void Update()
    {
        // Tecla de reset
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetPuzzle();
        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Este método lo llama el LightReceiver cuando el haz lo golpea
    public void OnReceiverActivated(LightReceiver receiver)
    {
        if (activePuzzle == null) return;

        if (!puzzleAlreadySolved && activePuzzle.IsPuzzleSolved())
        {
            puzzleAlreadySolved = true;

            Debug.Log("🔥 Puzzle resuelto!");
            
            if (mainDoor != null)
            {
                mainDoor.OpenDoor();
            }
        }
    }

    public void ResetPuzzle()
    {
        puzzleAlreadySolved = false;

        // Reset de la puerta
        if (mainDoor != null)
        {
            mainDoor.ResetDoor();
        }

        // Reset de los objetos dinámicos (prismas, etc.)
        if (resettableObjects != null)
        {
            foreach (var obj in resettableObjects)
            {
                if (obj != null)
                    obj.ResetState();
            }
        }

        Debug.Log("Puzzle reseteado.");
    }

    // Más adelante aquí agregaremos:
    // - Reset de nivel
    // - Cambio de puzzles
    // - Estados generales del juego
}
