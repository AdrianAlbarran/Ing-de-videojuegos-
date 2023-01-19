using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : AbstractDrop
{
    // Start is called before the first frame update
    public HealthDrop() : base("Health")
    {

    }

    public override IPrototypeDrop Clone(Vector3 position)
    {
        HealthDrop a = new HealthDrop();
        a.Create(position);
        return a;
    }
}
