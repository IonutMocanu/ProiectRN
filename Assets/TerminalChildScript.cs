using UnityEngine;

public class TerminalChildScript : MonoBehaviour
{
    [SerializeField, Tooltip("Tatal robot")] RobotAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        agent.HandleChildCollision(true);
    }
}
