using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealing
{
    public void HealPlayer(GameObject player);

    public int HealingAmount
    {
        get;
        set;
    }
}
