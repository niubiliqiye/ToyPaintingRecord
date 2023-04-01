using UnityEngine;
using System.Collections.Generic;

//example
[RequireComponent(typeof(PolyNavAgent))]
public class ClickToMove : MonoBehaviour{
	
	private PolyNavAgent _agent;
	private PolyNavAgent agent{
		get {return _agent != null? _agent : _agent = GetComponent<PolyNavAgent>();}
	}

	void Update() {
		if (Input.GetMouseButton(0)){
			if (gameObject.transform.position.x > Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
			{
				gameObject.GetComponent<SpriteRenderer>().flipX = false;
			}
			if (gameObject.transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x)
			{
				gameObject.GetComponent<SpriteRenderer>().flipX = true;
			}
			agent.SetDestination( Camera.main.ScreenToWorldPoint(Input.mousePosition) );
		}
	}
}