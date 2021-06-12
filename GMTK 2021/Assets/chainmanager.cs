using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chainmanager : MonoBehaviour
{
    public GameObject linkObject;
    List<GameObject> chain = new List<GameObject>();
    List<Rigidbody2D> rigidBodies = new List<Rigidbody2D>();
    GameObject head;
    List<Vector2> positions = new List<Vector2>();
    Vector2 lastPos;
    Vector2 direction;

    public float speed;
    public float drag = 0.75f;

    public float dist = 2; 
    // Start is called before the first frame update
    void Start()
    {
        summonDudes(5);
        //positions.Add(head.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        // move the head
        direction = new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction.Normalize();
        rigidBodies[0].velocity = direction * speed;
        if (direction.magnitude == 0) {
            rigidBodies[0].velocity = rigidBodies[0].velocity * drag;
        }

        // get the current position and potentially push the new position if it's far enough away from the last
        Vector2 pos = head.transform.position;
        if (lastPos == null || (lastPos - pos).magnitude > dist) {
            positions.Add(pos);
            lastPos = pos;
        }

        for (int i = 1; i < chain.Count; i++) {
            if (positions.Count - i >= 0) {
                if ((chain[i-1].transform.position - chain[i].transform.position).magnitude > dist) {
                    direction = positions[positions.Count - i] - (Vector2) chain[i].transform.position;
                    direction.Normalize();
                    rigidBodies[i].velocity = direction * speed;
                } else {
                    rigidBodies[i].velocity = rigidBodies[i].velocity * drag;
                }

                if (i == chain.Count-1 && positions.Count > i) {
                    positions.RemoveAt(0);
                }
            }
        }
    }

    void summonDude() {
        chain.Add(Instantiate(linkObject, gameObject.transform));
        rigidBodies.Add(chain[chain.Count - 1].GetComponent<Rigidbody2D>());
        if (chain.Count > 1)
            chain[chain.Count - 1].transform.position = chain[chain.Count - 2].transform.position - new Vector3(0, dist, 0);
        if (chain.Count == 1) {
            head = chain[0];
        }
    }

    void summonDudes(int dudes) {
        for (int i = 0; i < dudes; i++) {
            summonDude();
        }
    }
}
