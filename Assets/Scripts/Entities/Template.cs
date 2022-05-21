﻿using System;
using UnityEngine;

public class Template : DrawElement, ICombat {

    [SerializeField]
    protected bool _placed;
    [SerializeField]
    protected bool _canPlace;

    protected bool _isBox;
    public bool IsBox() { return _isBox; }

    protected bool CanPlace() { return _canPlace; }
    public bool Placed() { return _placed; }

    protected void load() {
        _camera = Camera.main;
        _position = new Vector2(0.0f, 0.0f);
        _offset = new Vector2(0.0f, 0.0f);

        _isBox = false;

        _placed = false;
        _canPlace = true;

        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<BoxCollider2D>().isTrigger = true;

        Show(0.5f);
    }

    protected void PlaceTemplate() {
        Show();
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        _placed = true;
    }

    public void TryToPlace(){
        if (CanPlace() && !Placed()) PlaceTemplate();
    }

    protected void HighlightGreen() {
        if (Placed()) return;

        _canPlace = true;
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 1.0f, 0.5f, c.a);
    }

    protected void HighlightRed() {
        if (Placed()) return;

        _canPlace = false;
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.5f, c.a);
    }

    protected void HighlightNormal() {
        if (Placed()) return;

        _canPlace = true;
        Color c = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, c.a);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != null) HighlightRed();
    }

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject != null) HighlightRed();
    }

    private void OnTriggerExit2D(Collider2D other) {
        HighlightNormal();
    }

    public void Attack(ICombat target){}

    public void TakeDamage(Attack weapon) {
        GameObject.FindObjectOfType<GameManager>().InstantiateInkPot(transform.position);
        Destroy(this.gameObject);
    }

    public override void MainAction(bool action) {}

}