﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public abstract void Apply(PlayerStats stats);
}
