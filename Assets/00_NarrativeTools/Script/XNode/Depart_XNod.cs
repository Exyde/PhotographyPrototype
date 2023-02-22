using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Depart_XNod : Node {

	[Output] public bool True;
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		return true; // Replace this
	}
}