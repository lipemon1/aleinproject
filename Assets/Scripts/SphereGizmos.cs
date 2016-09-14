using UnityEngine;
using System.Collections;

public class SphereGizmos : MonoBehaviour {

    public float gizmoSize = 1f;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
