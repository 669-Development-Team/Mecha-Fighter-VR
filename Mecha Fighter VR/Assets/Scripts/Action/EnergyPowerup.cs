using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPowerup : Powerup
{
    [SerializeField] private float energyAmount;
    public override void Apply(PlayerStats stats) {
        stats.DepleteEnergy(-energyAmount);
    }
}
