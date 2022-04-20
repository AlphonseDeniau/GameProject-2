using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyRoom : ARoom
{

    public EmptyRoom(int depth, int index) : base(depth, index)
    {

    }

    public override void InitRoom()
    {
        throw new System.NotImplementedException();
    }

    public override void EnterInRoom()
    {
        throw new System.NotImplementedException();
    }
}
