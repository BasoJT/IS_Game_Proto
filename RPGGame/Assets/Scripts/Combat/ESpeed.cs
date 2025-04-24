using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpeed : MonoBehaviour
{
    [SerializeField] private float speedValue; // Speed value for the enemy
    [SerializeField] private float rollSpeed;  // Roll speed for the enemy
    [SerializeField] private float roundSpeed; // Round speed for the enemy (calculated)

    public float SpeedValue
    {
        get { return speedValue; }
        set
        {
            speedValue = value;
            UpdateRoundSpeed(); // Recalculate roundSpeed whenever speedValue changes
        }
    }

    public float RollSpeed
    {
        get { return rollSpeed; }
        set
        {
            rollSpeed = value;
            UpdateRoundSpeed(); // Recalculate roundSpeed whenever rollSpeed changes
        }
    }

    public float RoundSpeed
    {
        get { return roundSpeed; }
        private set { roundSpeed = value; } // Make roundSpeed read-only from outside
    }

    private void UpdateRoundSpeed()
    {
        roundSpeed = speedValue + rollSpeed; // Calculate roundSpeed as the sum of speedValue and rollSpeed
    }
}

