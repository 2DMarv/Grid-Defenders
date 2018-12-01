using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DM : MonoBehaviour {

    Blob activeUnit;
    Blob prevUnit;
    public Button moveButton;
    public Button attackButton;

    // Text Fields for UI to display unit info
    public Text unitName;
    public Text unitHealth;

	// Use this for initialization
	void Start () {
        moveButton.onClick.AddListener(delegate { ActiveUnitMove(activeUnit); });
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

            if (hit.collider != null) {
                if (hit.collider.tag == "Blob") {
                    activeUnit = hit.collider.GetComponent<Blob>();                    
                    ActivateUnit(activeUnit);
                } 

            } 
        }

    }

    // displays the info and activates the blob
    void ActivateUnit(Blob unit) {
        activeUnit.Activated();
        unitName.text = unit.unitName;

        if (prevUnit != null && prevUnit != activeUnit) {
            prevUnit.Deactivated();
        }

        prevUnit = activeUnit;
        
    }

    void ActiveUnitMove(Blob unit) {
        unit.transform.Translate(5, 0, 0);
    }

    void ActiveUnitAttack(Blob unit) {

    }
}
