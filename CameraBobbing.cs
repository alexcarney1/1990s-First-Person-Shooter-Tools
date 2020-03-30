using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Author: Alex R. Carney
* Adds vertical camera bobbing in the over-the-top 1990s fashion. Apply to player controller
* camera. Camera bobbing becomes MORE dramatic in both speed and travel distance 
* as movement velocity increases. Camera bobbing becomes LESS dramatic in both speed
* and travel distance as movement velocity decreases. 
* 
* Altering bobbingDistDivisor will change the feel quiet a bit. Smaller is MORE dramatic
* bobbing at peak velocity. Higher number is LESS dramatic bobbing at peak velocity.
* 
* Designed to work in conjuction with my OldSchoolFPSController or general rigidbody 
* controllers. Can be hacked to function with character controllers.
* 
*/
public class CameraBobbing : MonoBehaviour
{
    public Rigidbody rb;
    private float origCamYPos;
    private float timer = 0;
    //smaller is More dramatic bobbing at peak velocity. Higher number is less dramatic bobbing at peak velocity.
    public float bobbingDistDivisor = 70f;
    void Start()
    {
        origCamYPos = transform.localPosition.y;
    }

    void FixedUpdate()
    {
        //player moving.The faster you move, the more dramatic the bobbing in speed and distance.
        if (Mathf.Abs(rb.velocity.magnitude) > 0f) 
        {
            //print(rb.velocity.magnitude);
            timer += Time.deltaTime * rb.velocity.magnitude;
            transform.localPosition = new Vector3(transform.localPosition.x, origCamYPos + Mathf.Sin(timer)
                * (rb.velocity.magnitude/bobbingDistDivisor), transform.localPosition.z);
        }
        //player idle. Return camera to default position.
        else
        {
            timer = 0;
            //place camera back to its original position. Lerping this will look smoother, but I prefer instant return for this.
            transform.localPosition = new Vector3(transform.localPosition.x, origCamYPos, transform.localPosition.z);
        }

    }
}
