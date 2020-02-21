using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool energyLossEnabled = true;

    private float Energy = 50;
    private float lossFactor = 0.01f;
    private int collectedOrbCount = 0;
    
    public void CollectOrb(OrbSettings orb)
    {
        this.Energy += orb.Energy;
        this.collectedOrbCount += 1;
    }
    public float GetEnergy()
    {
        return this.Energy;
    }

    public int GetOrbCount()
    {
        return this.collectedOrbCount;
    }
    private void EnergyLoss()
    {
        if (Energy >= 0)
        {
            Energy -= lossFactor;
        }
    }



    private void Update()
    {
        if(energyLossEnabled) EnergyLoss();
    }
}
