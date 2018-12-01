using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Blob : MonoBehaviour {

    public Animator anim;
    public Tilemap groundTilemap;

    //stats for Blob
    public string unitName;
    public int health;
    public int attack;
    public bool isActive = false;

    public bool onCooldown = false;
    public bool isMoving = false;
    private float moveTime = 1.2f;

    public Blob() {
        unitName = "Blob";        
        // Debug.Log("Base constructor run");
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
      

    }

    public void Move(int xDir, int yDir) {
        Vector2 startCell = transform.position;
        Vector2 targetCell = startCell + new Vector2(xDir, yDir);
        
        bool isOnGround = getCell(groundTilemap, startCell) != null; //If the player is on the ground
        bool hasGroundTile = getCell(groundTilemap, targetCell) != null; //If target tile has a ground
        bool tileIsEmpty = MapPositioning.CheckPosition(targetCell) == 0;
       
        
        if (isOnGround && tileIsEmpty) {

            //If the front tile is a walkable ground tile, the player moves here.
            if (hasGroundTile) {
                MapPositioning.UpdatePositionLEAVE(startCell);
                MapPositioning.UpdatePositionENTER(targetCell);
                StartCoroutine(SmoothMovement(targetCell));
            }
        } else {
            print("tile occupied");
        }
    }

    private IEnumerator SmoothMovement(Vector3 end) {
        while (isMoving) yield return null;

        isMoving = true;

        /*Play movement sound
        if (walkingSound != null) {
            walkingSound.loop = true;
            walkingSound.Play();
        }*/

        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;
        float inverseMoveTime = 1 / moveTime;

        while (sqrRemainingDistance > float.Epsilon) {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, end, inverseMoveTime * Time.deltaTime);
            transform.position = newPosition;
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            yield return null;
        }

        /*if (walkingSound != null)
            walkingSound.loop = false;*/
        isMoving = false;
    }

    public IEnumerator actionCooldown(float cooldown) {
        onCooldown = true;

        //float cooldown = 0.2f;
        while (cooldown > 0f) {
            cooldown -= Time.deltaTime;
            yield return null;
        }

        onCooldown = false;
    }

    public void Activated() {
        isActive = true;
        anim.enabled = true;
        anim.Play(unitName + "_Idle");
    }

    public void Deactivated() {
        isActive = false;
        anim.enabled = false;
    }

    private TileBase getCell(Tilemap tilemap, Vector2 cellWorldPos) {
        //print(tilemap.WorldToCell(cellWorldPos));
        return tilemap.GetTile(tilemap.WorldToCell(cellWorldPos));
    }
}
