using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Damageable
{
    public void Damage(int amt = 1, Dictionary<string, object> additionalConditions = null);
}
