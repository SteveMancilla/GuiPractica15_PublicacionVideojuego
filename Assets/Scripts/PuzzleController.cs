using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    [Header("Receptores que deben activarse")]
    public LightReceiver[] requiredReceivers;

    public bool IsPuzzleSolved()
    {
        foreach (var receiver in requiredReceivers)
        {
            if (receiver == null || !receiver.IsLit())
                return false;
        }
        return true;
    }
}