using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Read player input through the old input system.
/// </summary>
public class Player : MonoBehaviour
{
    [SerializeField]
    private UnityEvent Speak;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Speak.Invoke();
        }
    }
}
