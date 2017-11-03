using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gen {
	int action;

	public int Action {
		get {
			return action;
		}
	}

	float time;

	public float Time {
		get {
			return time;
		}
	}

	public Gen(int action,float time){
		this.action = action;
		this.time = time;
	}
}
