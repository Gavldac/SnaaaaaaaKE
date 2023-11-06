using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for Obsitles that the Snake Object can collide with.
/// 
/// author Edwin Casady
/// </summary>
public class ObsticleScript : MonoBehaviour
{
    public BoxCollider2D PlayableArea;

    public void RandomPosition()
    {
        Bounds boundry = this.PlayableArea.bounds;

        float X = Random.Range(boundry.min.x, boundry.max.x);
        float Y = Random.Range(boundry.min.y, boundry.max.y);

        this.transform.position = new Vector3(
            Mathf.Round(X), 
            Mathf.Round(Y), 
            0.0f);
    }

  
    // Start is called before the first frame update
    private void Start()
    {
        RandomPosition();
    }


    
}
