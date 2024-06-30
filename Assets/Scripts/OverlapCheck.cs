using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{   
    Vector2 top_right_cornerAABB,bottom_left_cornerAABB;


    // Start is called before the first frame update
    void Start()
    {

        // Debug.Log($"sides to right {top_right_corner} sides bottom left {bottom_left_corner}");
        // Debug.Log($" min  { sr.bounds.min} max {sr.bounds.max}");
        // Debug.Log($"sprite x min  { sr.sprite.rect.xMin} sprite x max {sr.sprite.rect.xMax} sprite y min  { sr.sprite.rect.yMin} sprite y max {sr.sprite.rect.yMax}");
        // top_right_corner = new Vector2(centerX+extendsX,centerY+extendsY);
        // bottom_left_corner = new Vector2(centerX-extendsX,centerY-extendsY);
        // Debug.Log($"Based off sprite {top_right_corner} and {bottom_left_corner}");
        // (top_right_corner, bottom_left_corner) = GetAABBCorners(this.GetComponent<Collider2D>());
        // Debug.Log($"Based off function {top_right_corner} and {bottom_left_corner}");
        
    }
    
    private void SetOverlappingArea()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float centerX = sr.bounds.center.x; 
        float centerY = sr.bounds.center.y;
        float extendsX = sr.bounds.extents.x; 
        float extendsY = sr.bounds.extents.y;
        top_right_corner = new Vector2(centerX+extendsX,centerY+extendsY);
        bottom_left_corner = new Vector2(centerX-extendsX,centerY-extendsY);
    }
    private void FixedUpdate()
    {
        //OverlapAreaNonAlloc
        //OverlapAreaAll
        // int num_colliders = Physics2D.OverlapAreaNonAlloc(top_right_corner, bottom_left_corner, results);
        Collider2D[] result = Physics2D.OverlapAreaAll(top_right_corner, bottom_left_corner);
        //Debug.Log(num_Colliders); 

    }


    string DetectMostOverlap(Collider2D[] lib)
    {
        string currentResult = String.Empty;
        float currentAreaMax = 0f;
        if (lib.Length == 1)
        {
            return lib[0].gameObject.name;
        }
        

        return currentResult;
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

        CustomDebug.DrawRectange(top_right_corner, bottom_left_corner); 

    }
    
    /*public Vector3 OverlapArea(BoxCollider a, BoxCollider b)
    {
        // get the bounds of both colliders
        var boundsA = a.bounds;
        var boundsB = b.bounds;

        // first heck whether the two objects are even overlapping at all
        if(!boundsA.Intersects(boundsB))
        {
            Vector3.zero;
        }

        // now that we know they at least overlap somehow we can calculate

        // get the bounds of both colliders
        var boundsA = a.bounds;
        var boundsB = b.bounds;

        // get min and max point of both
        var minA = boundsA.min; //(basically the bottom-left-back corner point)
        var maxA = boundsA.max; //(basically the top-right-front corner point)

        var minB = boundsB.min;
        var maxB = boundsB.max;

        // we want the smaller of the max and the higher of the min points
        var lowerMax = new Vector3.Min(maxA, maxB);
        var higherMin = new Vector3.Max(minA, minB);

        // the delta between those is now your overlapping area
        return lowerMax - higherMin;
    }*/
}
