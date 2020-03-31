using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* Author: Alex R. Carney
* Adds gun sway commonly found in 1990s FPS games. Moves centered gun sprites in a U pattern.
* SwayAmountOverdrive controls how dramatic the sway is. When set to 1, the sway matches 
* current rigidbody velocity. This effect is meant to be used on centered gun sprites
* such as those typically found in the 2.5D FPS games from the 1990s.
* 
* Designed to work in conjuction with my OldSchoolFPSController or general rigidbody 
* controllers. Can be hacked to function with character controllers.
* 
*/
public class GunSway : MonoBehaviour
{
    public Rigidbody rb;
    private float timer = 0;
    public float swayAmountOverdrive = 2f; //Extra overdrive on size of arc. 1 is no extra overdrive.
    private float defPosY;
    private void Start()
    {
        defPosY = transform.localPosition.y;
    }
    void FixedUpdate()
    {
        //player moving
        if (Mathf.Abs(rb.velocity.magnitude) > 0f)
        {
            timer += Time.deltaTime * rb.velocity.magnitude;
            float scale = 2 / (3 - Mathf.Cos(2 * timer)) * swayAmountOverdrive;
            float x = scale * Mathf.Cos(timer);
            float y = scale * Mathf.Cos(2 * timer) / 2;

            x *= 1 * rb.velocity.magnitude;
            y *= 1 * rb.velocity.magnitude;

            transform.localPosition = new Vector3(x, defPosY + y, transform.localPosition.z);
        }
        //idle
        else
        { ResetToCenter(); }
    }

    //Resets on idle. Can also be called to reset when player shoots in a companion script if desired.
    public void ResetToCenter()
    {
        timer = 0;
        transform.localPosition = new Vector3(0, defPosY, 0);
    }

}
