using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public float speed = 30;
    Vector2 originalPos;

    // Use this for initialization
    void Start() {
        // Initial velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        /*if (Input.GetKeyDown(KeyCode.UpArrow)) {
            
        }*/
        originalPos = new Vector2(transform.position.x, transform.position.y);
	}

    void OnCollisionEnter2D(Collision2D col) {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // Hit left racket?
        if (col.gameObject.name == "RacketLeft") {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y); //CHECK

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set velocity with direction * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        // Hit right racket?
        if (col.gameObject.name == "RacketRight")
        {
            float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y); //CHECK

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set velocity with direction * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }

        //Hit left wall?
        if (col.gameObject.name == "WallLeft") {
            transform.position = originalPos;
        }

        // Hit right wall?
        if(col.gameObject.name == "WallRight") {
            transform.position = -originalPos;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket

        return (ballPos.y - racketPos.y) / racketHeight;
    }
}
