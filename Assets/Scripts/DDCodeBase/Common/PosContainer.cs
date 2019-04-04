using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Pos/PosContainer")]
public class PosContainer : MonoBehaviour
{
    public List<Transform> Points = new List<Transform>();

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (Points.Count == 0)
            return;
    
        for (int i = 0; i < Points.Count; i++)
        {
            Gizmos.color = new Color(0.0f, 1.0f, 1.0f, 0.8f);
            Gizmos.DrawSphere(Points[i].transform.position, 1);
            Gizmos.DrawWireSphere(Points[i].transform.position, 5f);          
        }
    
    }
#endif

}
