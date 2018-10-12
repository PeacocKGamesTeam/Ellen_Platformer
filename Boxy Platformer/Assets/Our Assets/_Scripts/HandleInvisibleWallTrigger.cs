using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleInvisibleWallTrigger : MonoBehaviour {

    public string BoundaryPosition;
    public ParticleSystem wallpuff;
    

        public void OnCollisionEnter2D(Collision2D collision)
    {
        
        float x = collision.gameObject.transform.position.x;
        float y = collision.gameObject.transform.position.y;
        Vector2 position = new Vector2(x, y);
        Vector2 PuffPosition = new Vector2(x, y);

        if (BoundaryPosition == "Left")
        {
           position = new Vector2(x + 2, y);
           PuffPosition = new Vector2(x - 0.5f, y);
        }
        else if (BoundaryPosition == "Right")
        {
           position = new Vector2(x - 2, y);
           PuffPosition = new Vector2(x + 0.5f, y);
        }

        collision.gameObject.transform.position = position;
        wallpuff.transform.position = PuffPosition;
        wallpuff.Play();

    }

    public void OnCollisionExit2D(Collision2D collision)
    {
       
        wallpuff.Stop();
    }
}
