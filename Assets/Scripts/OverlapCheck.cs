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
        Debug.Log(col.gameObject.name);
        return col;
    }
    
    Collider2D DetermineMostOverlap(Collider2D[] lib)
    {
        Collider2D result = lib[0];
        float currentResult = 0;
        foreach(var col in lib)
        {
            float currentArea = GetOverlappingArea(col);

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
