using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SceneView : MonoBehaviour
{
    // Use this for initialization
    protected virtual void Start()
    {
        SceneUIInitialize();
    }

    public abstract void SceneUIInitialize();
}
