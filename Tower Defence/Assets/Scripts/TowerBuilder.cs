using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public Tower TowerPrefab;

    public Color AllowColor;
    public Color BlockColor;

    void Update()
    {
        //Reposition the gameobject to mouse coordinates.
        //Round the coordinates to make it snap to a grid.
        Vector3 mousePos = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x - 0.5f) + 0.5f,
            Mathf.Round(mousePos.y - 0.5f) + 0.5f,
            0);
        mousePos.z = 0;
        transform.position = mousePos;

        //Verify that building area is free of other towers. 
        //By using a static overlap method from Physics2D class we can make this work without collider and a 2d rigidbody.
        bool free = IsFree(transform.position);

        //Tint the sprite to green or red accordingly.
        if (free)
        {
            TintSprite(AllowColor);
        }
        else
        {
            TintSprite(BlockColor);
        }


        //Call the build method when the player presses left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Build();
        }
    }

    bool IsFree(Vector3 pos)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(pos, 0.45f);
        foreach (Collider2D overlap in overlaps)
        {
            if (!overlap.isTrigger) return false;
        }
        return true;
    }

    void TintSprite(Color col)
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer rend in renderers)
        {
            rend.color = col;
        }
    }

    void Build()
    {
        //Verify that building area is free of other towers. (Turn this into a method)
        //Make a note to remove gold from player later when gold is implemented
        //Instantiate a tower prefab at the current position
        //Disable the Tower Builder gameobject

        if (!IsFree(transform.position)) return;
        // TODO remove gold from player
        GameObject.Instantiate(TowerPrefab, transform.position, Quaternion.identity, null);
    }
}
