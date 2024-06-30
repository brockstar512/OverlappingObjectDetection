using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 10.0f;
    public float rotation_speed = 4.0f;


    private GameObject target; 
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target"); 
    }

    private void Update()
    {

        Vector2 direction = (target.transform.position - this.transform.position);

        if (direction.sqrMagnitude >= 0.5f)
        {
            float sign = Mathf.Sign(Vector3.Dot(Vector3.Cross(transform.up, direction), Vector3.forward));
            float angle = Vector2.Angle(direction, transform.up) * sign * rotation_speed * Time.deltaTime;
            transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);

            transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
        }
    }
	

}
