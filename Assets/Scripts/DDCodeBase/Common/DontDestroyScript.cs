using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}
