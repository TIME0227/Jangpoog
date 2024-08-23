using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Destination : Destination
{
    private Collider2D interactCollider;

    private void Awake()
    {
        interactCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (GameObject.FindWithTag("Monster") != null)
        {
            interactCollider.enabled = false;
        }
        else
        {
            interactCollider.enabled = true;
        }
    }
}
