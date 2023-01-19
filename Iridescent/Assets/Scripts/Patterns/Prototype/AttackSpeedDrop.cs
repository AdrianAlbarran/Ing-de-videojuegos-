using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedDrop : AbstractDrop
{
    // Start is called before the first frame update
    public AttackSpeedDrop() : base("AttackSpeed")
    {

    }

    public override IPrototypeDrop Clone(Vector3 position)
    {
        AttackSpeedDrop a = new AttackSpeedDrop();
        a.Create(position);
        return a;
    }
}
