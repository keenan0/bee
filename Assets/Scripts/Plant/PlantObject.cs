using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantObject : MonoBehaviour {
    [SerializeField]
    PlantManager manager;
    [SerializeField]
    PlantTimer timer;

    Sprite plantTexture;

    Transform plantRenderer;

    int spriteIndex;

    private void Awake() {
        //Get the PlantManager object
        manager = FindObjectOfType<PlantManager>();

        //Find the plant_sprite component of the plant object
        plantRenderer = transform.Find("plant_sprite");

        //Set the plant to a random one
        plantTexture = manager.PickRandomSprite(ref spriteIndex);

        //Set the sprite of that plant sprite to the randomly picked texture
        plantRenderer.GetComponent<SpriteRenderer>().sprite = plantTexture;
        ;
    }
}
