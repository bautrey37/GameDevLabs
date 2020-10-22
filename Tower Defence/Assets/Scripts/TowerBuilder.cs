using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerBuilder : MonoBehaviour
{
    public Color AllowColor;
    public Color BlockColor;

    private TowerData CurrentTowerData;

    private void Awake()
    {
        Events.OnTowerSelected += TowerSelected;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Events.OnTowerSelected -= TowerSelected;
    }

    void Update()
    {
        Vector3 mousePos = transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(
            Mathf.Round(mousePos.x - 0.5f) + 0.5f,
            Mathf.Round(mousePos.y - 0.5f) + 0.5f,
            0);
        mousePos.z = 0;
        transform.position = mousePos;
 
        bool free = IsFree(transform.position);

        if (free)
        {
            TintSprite(AllowColor);
        }
        else
        {
            TintSprite(BlockColor);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Build();
        }
        if (Input.GetMouseButtonDown(1))
        {
            gameObject.SetActive(false);
        }
    }

    bool IsFree(Vector3 pos)
    {
        if (Events.RequestGold() < CurrentTowerData.Cost)
        {
            return false;
        }

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

    private void TowerSelected(TowerData data)
    {
        CurrentTowerData = data;

        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
        renderers[0].sprite = CurrentTowerData.Icon;
        renderers[1].sprite = null;

        gameObject.SetActive(true);
    }

    void Build()
    {
        if (!IsFree(transform.position)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Events.SetGold(Events.RequestGold() - CurrentTowerData.Cost);

        GameObject.Instantiate(CurrentTowerData.TowerPrefab, transform.position, Quaternion.identity, null);
        gameObject.SetActive(false);
    }
}
