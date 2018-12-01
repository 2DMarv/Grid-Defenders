using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloo : Blob {
    
    /*
    public Bloo() {
        unitName = "Bloo";
        health = 50;
        attack = 3;
        Debug.Log("constructor run");
        Debug.Log(unitName);
    } */

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        unitName = "Bloo";
        health = 50;
        attack = 3;

        Vector2 startCell = transform.position;
        MapPositioning.UpdatePositionENTER(startCell);
    }

    // Update is called once per frame
    void Update() {
        if (!isActive) return;
        if (isMoving || onCooldown) return;

        int horizontal = 0;
        int vertical = 0;

        //To get move directions
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        vertical = (int)(Input.GetAxisRaw("Vertical"));

        //We can't go in both directions at the same time
        if (horizontal != 0)
            vertical = 0;

        //If there's a direction, we are trying to move.
        if (horizontal != 0 || vertical != 0) {
            StartCoroutine(actionCooldown(0.2f));
            Move(horizontal, vertical);
        }
    }
}
