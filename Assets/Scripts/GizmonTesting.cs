using UnityEngine;

public class GizmonTesting : MonoBehaviour
{
    public void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 2);
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
