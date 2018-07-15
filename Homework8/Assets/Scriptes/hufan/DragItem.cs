using UnityEngine;
using System.Collections;

public class DragItem : MonoBehaviour {


	// Use this for initialization
	GameObject a;
	public Camera uiCamera;

	void Start () {
		a = transform.parent.gameObject;
		//a.AddComponent<WindowDragTilt> ();
		/*if (a.GetComponent<UISprite>() != null) {
			transform.GetChild(0).GetComponent<BoxCollider>().size = new Vector3(
				a.GetComponent<UISprite>().width,
				a.GetComponent<UISprite>().height, 0);
		}*/
		if (transform.GetChild(0).gameObject.GetComponent<UIEventListener>() == null)
			transform.GetChild(0).gameObject.AddComponent<UIEventListener>();
		UIEventListener.Get (transform.GetChild (0).gameObject).onDrag = ondrag;
		if (uiCamera == null)
			uiCamera = NGUITools.FindCameraForLayer(gameObject.layer);
	}

	void ondrag(GameObject g, Vector2 delta) {

		//Debug.Log (g.transform.position);
		Vector3 pos = Input.mousePosition;
		if (uiCamera != null) {
			// Since the screen can be of different than expected size, we want to convert
			// mouse coordinates to view space, then convert that to world position.
			pos.x = Mathf.Clamp01 (pos.x / Screen.width);
			pos.y = Mathf.Clamp01 (pos.y / Screen.height);
			a.transform.Translate(uiCamera.ViewportToWorldPoint (pos) - g.transform.position);
		}
		//a.transform.Translate(new Vector3(delta.x, delta.y, 0) * Time.deltaTime);
		//Debug.Log (delta);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
