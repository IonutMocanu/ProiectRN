using UnityEngine;

public class ChildScript : MonoBehaviour
{
    [SerializeField,Tooltip("Tatal robot")] RobotAgent agent;

    private void OnTriggerEnter(Collider other)
    {
        agent.HandleChildCollision(false);
    }
}
