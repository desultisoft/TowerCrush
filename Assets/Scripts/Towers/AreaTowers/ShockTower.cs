using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockTower : AreaTower
{
    public ParticleSystem activeSystem;
    public override void visualEffect()
    {
        activeSystem.Play();
    }
}
