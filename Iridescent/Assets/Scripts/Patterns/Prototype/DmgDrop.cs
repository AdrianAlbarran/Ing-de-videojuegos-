using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgDrop : AbstractDrop
{
    
    public DmgDrop() : base ("Damage")
    {
        
    }
    
    public override IPrototypeDrop Clone(Vector3 position)
    {
        DmgDrop a = new DmgDrop();
        a.Create(position);
        return a;
    }
}
