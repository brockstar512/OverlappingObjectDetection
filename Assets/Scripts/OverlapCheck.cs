using UnityEngine;

public class OverlapCheck : MonoBehaviour
{ 
    Vector2 _areaTopRightCornerAABB,_areaBottomLeftCornerAABB = Vector2.zero;
    public LayerMask detectionLayer;


    // Start is called before the first frame update
    void Start()
    {
        SetOverlappingArea();
    }
    void SetOverlappingArea()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float centerX = sr.bounds.center.x; 
        float centerY = sr.bounds.center.y;
        float extendsX = sr.bounds.extents.x; 
        float extendsY = sr.bounds.extents.y;

        _areaTopRightCornerAABB = new Vector2(centerX+extendsX,centerY+extendsY);
        _areaBottomLeftCornerAABB = new Vector2(centerX-extendsX,centerY-extendsY);
    }
    
    private void FixedUpdate()
    {
        GetMostOverlappedCol();
    }

    Collider2D GetMostOverlappedCol()
    {
        Collider2D[] overlappingCols = Physics2D.OverlapAreaAll(_areaTopRightCornerAABB, _areaBottomLeftCornerAABB,detectionLayer);
        if (overlappingCols.Length == 0)
            return null;

        Collider2D col = DetermineMostOverlap(overlappingCols);
        return col;
    }
    
    Collider2D DetermineMostOverlap(Collider2D[] lib)
    {
        Collider2D result = lib[0];
        float currentResult = 0;
        //float colliderArea = ()*()
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        foreach(var col in lib)
        {
            float currentArea = GetOverlappingArea(col);
            Percent(col.bounds,sr.bounds);
            

            if ( currentArea > currentResult)
            {
                currentResult = currentArea;
                result = col;

            }
        }

        return result;
    }
    
    float GetOverlappingArea(Collider2D overlappingObject)
    { 
        (Vector2 overlappingTopRightCornerAABB,Vector2 overlappingBottomLeftCornerAABB) = GetAABBCorners(overlappingObject);

        float xLength = Mathf.Min(_areaTopRightCornerAABB.x,overlappingTopRightCornerAABB.x)-Mathf.Max(_areaBottomLeftCornerAABB.x,overlappingTopRightCornerAABB.x);
        float yLength = Mathf.Min(_areaTopRightCornerAABB.y,overlappingBottomLeftCornerAABB.y)-Mathf.Max(_areaBottomLeftCornerAABB.y,overlappingBottomLeftCornerAABB.y);
        
        
        return xLength * yLength;
    }
    
    //get percentage that item overlaps
    float Percent(Bounds a,Bounds b)
    {
        // get the bounds of both colliders
        var boundsA = a;
        var boundsB = b;

        // get min and max point of both
        var minA = boundsA.min; //(basically the bottom-left-back corner point)
        var maxA = boundsA.max; //(basically the top-right-front corner point)

        var minB = boundsB.min;
        var maxB = boundsB.max;

        // we want the smaller of the max and the higher of the min points
        var lowerMax = Vector3.Min(maxA, maxB);
        var higherMin = Vector3.Max(minA, minB);
 
        // the delta between those is now your overlapping area
        Vector2 overlappingSqaure = lowerMax - higherMin;
        float overlappingArea = overlappingSqaure.x * overlappingSqaure.y;
        
        return overlappingArea/(a.extents.x * 2 * a.extents.y * 2)*100.0f;
    }
    
    (Vector2, Vector2) GetAABBCorners(Collider2D overlappingObject)
    {   

        Bounds objectsBound = overlappingObject.bounds;
        
        Vector2 topRightCorner = new Vector2(
            objectsBound.center.x + objectsBound.extents.x,
            objectsBound.center.y + objectsBound.extents.y);
        
        Vector2 bottomLeftCorner = new Vector2(
            objectsBound.center.x - objectsBound.extents.x,
            objectsBound.center.y - objectsBound.extents.y);
        
        return (topRightCorner, bottomLeftCorner);
    }
    
    private void OnDrawGizmos()
    {

        CustomDebug.DrawRectange(_areaTopRightCornerAABB, _areaBottomLeftCornerAABB); 

    }
    
    
}
