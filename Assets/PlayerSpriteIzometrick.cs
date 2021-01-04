using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteIzometrick : MonoBehaviour
{
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private float rotation;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
     

    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.flipX = false; 
        rotation = player.transform.eulerAngles.y;
        //    
        if ((rotation<45) || (rotation > 315))
        {
            spriteRenderer.sprite = sprites[2];
        }
        else
        if ((rotation > 45) && (rotation < 135))
        {
            spriteRenderer.sprite = sprites[1];
            spriteRenderer.flipX = true;
        }
        else
        if ((rotation > 135) && (rotation < 225))
            spriteRenderer.sprite = sprites[0];
        else
        {
            spriteRenderer.sprite = sprites[1];

        }


    }
}
