using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DNA : MonoBehaviour {

	//gene for colour
	public float r;
	public float g;
	public float b;
	public float scale = 0.2f;
	public bool dead = false;
	public float timeToDie = 0;
	SpriteRenderer spriteRenderer;
	Collider2D col2d;

	
	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		col2d = GetComponent<Collider2D>();

		spriteRenderer.color = new Color(r,g,b);
		transform.localScale = new Vector3(scale,scale,scale);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// OnMouseDown is called when the user has pressed the mouse button while
	/// over the GUIElement or Collider.
	/// </summary>
	void OnMouseDown()
	{
		dead = true;
		timeToDie = PopulationManager.elapsedTime;
		spriteRenderer.enabled = false;
		col2d.enabled = false;
	}
	
	
  public void RemovePerson(){ 
    dead = true; 
    timeToDie = PopulationManager.elapsedTime; 
    spriteRenderer.enabled = false; 
    col2d.enabled = false; 
  } 
}
