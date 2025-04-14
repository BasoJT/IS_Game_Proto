using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : MonoBehaviour
{
    [SerializeField] private float speedValue; // Speed value that can be set in the Inspector

    public float SpeedValue
    {
        get { return speedValue; }
        set { speedValue = value; }
    }
}
