using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;


    private Collider2D[] results; 

    private void Start()
    {
        results = new Collider2D[6]; 
    }

    private void Update()
    {

        int num_colliders = Physics2D.OverlapAreaNonAlloc(top_right_corner, bottom_left_corner, results);

        for (int i = 0; i < num_colliders; i++)
        {   
            Debug.Log(results[i].gameObject.name); 
        }

    }



    private void OnDrawGizmos()
    {

        CustomDebug.DrawRectange(top_right_corner, bottom_left_corner); 

    }


}
