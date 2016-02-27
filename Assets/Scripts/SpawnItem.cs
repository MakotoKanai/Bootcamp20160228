using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour {
	float time = 5;
	public GameObject Item;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time <= 0) {
			time = 5;
			float x = Random.Range(-20,20);
			float y = Random.Range(-20,20);
			Instantiate (Item, new Vector3 (x, y, 0), Quaternion.identity);
		}
	}
}

//u,n,k,d,v,w,Q,b,s,t,l,a,g,x,i,r,o,h,f,m,c,p,e,j