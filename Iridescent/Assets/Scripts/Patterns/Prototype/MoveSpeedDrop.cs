using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpeedDrop : AbstractDrop
{
    // Start is called before the first frame update
    public MoveSpeedDrop() : base("MoveSpeed")
    {

    }

    public override IPrototypeDrop Clone(Vector3 position)
    {
        MoveSpeedDrop a = new MoveSpeedDrop();
        a.Create(position);
        return a;
    }
}
