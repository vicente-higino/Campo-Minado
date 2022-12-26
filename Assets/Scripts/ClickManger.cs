using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManger : MonoBehaviour
{

    private List<RaycastHit2D> hits = new List<RaycastHit2D>();
    private RaycastHit2D hit2;
    private float time;

    void Update()
    {
        if (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                time = Time.time;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                
                hits.Add(hit);

            }
            if (Input.GetMouseButtonUp(0))
            {
                
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                hits.Add(hit);
                if (hits[hits.Count - 1].collider == hits[hits.Count - 2].collider)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    GameObject.Find("GameControl").GetComponent<GameControl>().firstClick = true;
                    hit.collider.gameObject.GetComponent<RandomSprite>().leftClick();
                    // hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
                hits.Clear();
            }
            if (Input.GetMouseButtonDown(1))
            {
                
                Debug.Log(time);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                hits.Add(hit);

            }
            if (Time.time-time >= 0.5f && time > 0 )
            {
                time = 0f;
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                hits.Add(hit);
                if (hits[hits.Count - 1].collider == hits[hits.Count - 2].collider)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    hit.collider.gameObject.GetComponent<RandomSprite>().rightClick();
                    // hit.collider.attachedRigidbody.AddForce(Vector2.up);
                }
                hits.Clear();
            }

        }
    }
        
}
