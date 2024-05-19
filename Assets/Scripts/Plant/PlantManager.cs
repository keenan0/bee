using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour {
    [SerializeField]
    List<Sprite> plantSprites;
    
    List<Color32> averageColorsPlants;

    private void Awake() {
        //Cache all the average colors (Probably should use async)
        //for (int i = 0; i < plantSprites.Count; i++) {
        //    averageColorsPlants.Add(AverageColorOfSprite(plantSprites[i].associatedAlphaSplitTexture));
        //}
    }

    private Color32 GetCachedColorAtIndex(int index) {
        return averageColorsPlants[index];
    }

    public Sprite PickRandomSprite(ref int index) {
        /*
            @return Sprite
        
            Function that returns a random sprite from a list of plant sprites. The list must be initialised manually in the beginning.
        */

        index = Random.Range(0, plantSprites.Count);

        return plantSprites[index];
    }

    private Color32 AverageColorOfSprite(Texture2D sampleSprite) {
        /*
            @return Color32
            
            Function that takes in a texture and returns the average color of the texture.
        */
        Color32[] pixels = sampleSprite.GetPixels32();
        int total = pixels.Length;

        Debug.Log(pixels);
        Debug.Log(pixels.ToString());

        float r = 0.0f;
        float g = 0.0f;
        float b = 0.0f;

        foreach(Color32 pixel in pixels) {
            r += pixel.r;
            g += pixel.g;
            b += pixel.b;
        }

        return new Color32((byte)(r/total), (byte)(g/total), (byte)(b/total), 0);
    }
}
