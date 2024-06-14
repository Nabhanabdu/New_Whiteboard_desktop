using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    //Access the textre of white board
    public Texture2D texture;
    //whitebprd resolution
    public Vector2 textureSize = new Vector2(x:2048, y:6144);

    void Start()
    {
        //setting new texture
        var r = GetComponent<Renderer>();
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        r.material.mainTexture = texture;
    }

}