using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{ 
    Vector2 area_top_right_cornerAABB,area_bottom_left_cornerAABB = Vector2.zero;
    


    // Start is called before the first frame update
    void Start()
    {
        SetOverlappingArea();
    }
    
    private void SetOverlappingArea()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float centerX = sr.bounds.center.x; 
        float centerY = sr.bounds.center.y;
        float extendsX = sr.bounds.extents.x; 
        float extendsY = sr.bounds.extents.y;
        area_top_right_cornerAABB = new Vector2(centerX+extendsX,centerY+extendsY);
        area_bottom_left_cornerAABB = new Vector2(centerX-extendsX,centerY-extendsY);
    }
    private void FixedUpdate()
    {
        //OverlapAreaNonAlloc
        //OverlapAreaAll
        // int num_colliders = Physics2D.OverlapAreaNonAlloc(top_right_corner, bottom_left_corner, results);
        Collider2D[] result = Physics2D.OverlapAreaAll(area_top_right_cornerAABB, area_bottom_left_cornerAABB);
        //Debug.Log(num_Colliders); 
        if (result.Length > 0)
        {
            DetectMostOverlap(result);
        }

    }


    string DetectMostOverlap(Collider2D[] lib)
    {
        string result = String.Empty;
        // if (lib.Length == 1)
        // {
        //     return lib[0].gameObject.name;
        // }
        foreach(var i in lib)
        {
            result = GetOverlappingArea(i).ToString();
        }

        return result;
    }
    

    float GetOverlappingArea(Collider2D OverlappingObject)
    { 
        (Vector2 overlapping_top_right_cornerAABB,Vector2 overlapping_bottom_left_cornerAABB) = GetAABBCorners(OverlappingObject);

        float xLength = Mathf.Min(area_top_right_cornerAABB.x,overlapping_top_right_cornerAABB.x)-Mathf.Max(area_bottom_left_cornerAABB.x,overlapping_bottom_left_cornerAABB.x);
        float yLength = Mathf.Min(area_top_right_cornerAABB.y,overlapping_top_right_cornerAABB.y)-Mathf.Max(area_bottom_left_cornerAABB.y,overlapping_bottom_left_cornerAABB.y);

        return xLength * yLength;
    }
    
    (Vector2, Vector2) GetAABBCorners(Collider2D overlappingObject)
    {   

        Bounds objectsBound = overlappingObject.bounds;
        
        Vector2 top_right_corner = new Vector2(
            objectsBound.center.x + objectsBound.extents.x,
            objectsBound.center.y + objectsBound.extents.y);
        
        Vector2 bottom_left_corner = new Vector2(
            objectsBound.center.x - objectsBound.extents.x,
            objectsBound.center.y - objectsBound.extents.y);
        
        return (top_right_corner, bottom_left_corner);
    }
    
    private void OnDrawGizmos()
    {

        CustomDebug.DrawRectange(area_top_right_cornerAABB, area_bottom_left_cornerAABB); 

    }
    
    
}
