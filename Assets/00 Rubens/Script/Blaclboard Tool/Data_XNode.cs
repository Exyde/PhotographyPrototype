using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using Core.GameEvents;

[NodeWidth(600)]
[CreateNodeMenu("Data/Data Node")]

public class Data_XNode : Node
{
    public string User;

    public List<BlackBoard> ListBlackboard;

    protected override void Init()
    {
        base.Init();
    } 
}
