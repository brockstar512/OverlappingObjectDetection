using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverlapCheck : MonoBehaviour
{   
    public Vector2 top_right_corner;
    public Vector2 bottom_left_corner;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //Debug.Log(this.transform.position);
        float centerX = sr.bounds.center.x; 
        float centerY = sr.bounds.center.y;
        float extendsX = sr.bounds.extents.x; 
        float extendsY = sr.bounds.extents.y;
        top_right_corner = new Vector2(centerX+extendsX,centerY+extendsY);
        bottom_left_corner = new Vector2(centerX-extendsX,centerY-extendsY);
        // Debug.Log($"sides to right {top_right_corner} sides bottom left {bottom_left_corner}");
        // Debug.Log($" min  { sr.bounds.min} max {sr.bounds.max}");
        // Debug.Log($"sprite x min  { sr.sprite.rect.xMin} sprite x max {sr.sprite.rect.xMax} sprite y min  { sr.sprite.rect.yMin} sprite y max {sr.sprite.rect.yMax}");
        top_right_corner = new Vector2(centerX+extendsX,centerY+extendsY);
        bottom_left_corner = new Vector2(centerX-extendsX,centerY-extendsY);
        Debug.Log($"Based off sprite {top_right_corner} and {bottom_left_corner}");
        (top_right_corner, bottom_left_corner) = GetAABBCorners(this.GetComponent<Collider2D>());
        Debug.Log($"Based off function {top_right_corner} and {bottom_left_corner}");



    }
    private void FixedUpdate()
    {
        //OverlapAreaNonAlloc
        //OverlapAreaAll
        // int num_colliders = Physics2D.OverlapAreaNonAlloc(top_right_corner, bottom_left_corner, results);
        Collider2D[] result = Physics2D.OverlapAreaAll(top_right_corner, bottom_left_corner);
        int num_Colliders = result.Length;
        //Debug.Log(num_Colliders); 
/*
        for (int i = 0; i < result.Length; i++)
        {           
            //Debug.Log(i); //i think its a null reference because the collider of the area does not actually have a game object... ts the line that are being drawn

            Debug.Log(result[i].gameObject.name);
            Debug.Log($" Bounds  { result[i].transform.GetComponent<BoxCollider2D>().bounds}");


            //Physics.ComputePenetration();
            
            //Total Area = Area of Rectangle1 + Area of Rectangle2 – Intersecting area of both the rectangles

        }
*/
    }

/*
    string DetectMostOverlap(Collider2D[] lib)
    {
        string currentResult = String.Empty;
        float currentAreaMax = 0f;
        if (lib.Length == 1)
        {
            return lib[0].gameObject.name;
        }
        
        SpriteRenderer areaShape = GetComponent<SpriteRenderer>();
        Vector2 areaLengths = new Vector2(areaShape.bounds.extents.x*2,areaShape.bounds.extents.y*2);
        float areaRegion = areaLengths.x * areaLengths.y; 
        
        foreach(Collider2D item in result)
        {
            //total area of current shape
            SpriteRenderer currentAreaShape = item.GetComponent<SpriteRenderer>();
            Vector2 currentAreaLengths = new Vector2(currentAreaShape.bounds.extents.x*2,currentAreaShape.bounds.extents.y*2);
            float currentAreaRegion = currentAreaLengths.x * currentAreaLengths.y; 
            ////total area of both
            Vector2 combinedDistance = new Vector2();
            //Mathf.Min(areaLengths.x,currentAreaLengths.x)-Mathf.Max(areaLengths.x,currentAreaLengths.x),
//todo these are the corner points the l1 and the r1
        }
        

        return currentResult;
    }
*/
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
