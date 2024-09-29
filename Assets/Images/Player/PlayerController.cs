using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;

    public string upAnime = "PlayerUp";
    public string downAnime = "PlayerDown";
    public string rightAnime = "PlayerRight";
    public string leftAnime = "PlayerLeft";
    public string deadAnime = "PlayerDead";

    string nowAnimation = "";
    string oldAnimation = "";

    float axisH;
    float axisV;
    public float angleZ = -90.0f;

    Rigidbody2D rbody;
    bool isMoving = false;



    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        oldAnimation = downAnime;
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving == false)
        {
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        Vector2 fromPt = transform.position;
        Vector2 toPt = new Vector2(fromPt.x + axisH, fromPt.y + axisV);
        angleZ = GetAngle(fromPt, toPt);

        if(angleZ >= -45 && angleZ <= 45)
        {
            nowAnimation = upAnime;
        }else if(angleZ >=45 && angleZ <= 135)
        {
            nowAnimation = rightAnime;
        }else if(angleZ >= -135 && angleZ <= -45)
        {
            nowAnimation = leftAnime;
        }
        else
        {
            nowAnimation = downAnime;
        }

        if(nowAnimation != oldAnimation)
        {
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }
    }

    void FixedUpdate()
    {
        rbody.velocity = new Vector2(axisH, axisV) * speed;
    }

    public void setAxis(float h, float v)
    {
        axisH = h;
        axisV = v;

        if(axisH == 0 && axisV == 0)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
    }

    float GetAngle(Vector2 p1, Vector2 p2)
    {
        float angle;

        if (axisH != 0 || axisV != 0)
        {
            float dx =p2.x - p1.x;
            float dy =p2.y - p1.y;

            float rad = Mathf.Atan2(dx, dy);

            angle = rad * Mathf.Rad2Deg;
        }
        else
        {
            {
                angle = angleZ;
            }
        }
        return angle;
    }
}
