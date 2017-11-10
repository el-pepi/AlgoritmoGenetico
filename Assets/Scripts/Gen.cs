using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Gen {
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
		set {
			time = value;
		}
	}

	public Gen(int action,float time){
		this.action = action;
		this.time = time;
	}
}
