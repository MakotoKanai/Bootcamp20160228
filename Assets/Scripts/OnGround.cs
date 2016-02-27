using UnityEngine;
using System.Collections;

public class OnGround : MonoBehaviour {
	public bool Flag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		{
			if (col.CompareTag ("Block")) {
				Flag = true;
			}
		}
	}
}