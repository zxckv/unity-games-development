using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCrosshair : MonoBehaviour {
    
    [Range(0, 100)]
    public float value;
    public float speed;
    public float margin;

    public RectTransform Top, Bottom, Left, Right, Centre;

    void Update() {
        float valueTop, valueBottom, valueLeft, valueRight;

        valueTop = Mathf.Lerp(Top.position.y, Centre.position.y + margin + value, speed * Time.deltaTime);
        valueBottom = Mathf.Lerp(Bottom.position.y, Centre.position.y - margin - value, speed * Time.deltaTime);
        valueLeft = Mathf.Lerp(Left.position.x, Centre.position.x - margin - value, speed * Time.deltaTime);
        valueRight = Mathf.Lerp(Right.position.x, Centre.position.x + margin + value, speed * Time.deltaTime);

        Top.position = new Vector2(Top.position.x, valueTop);
        Bottom.position = new Vector2(Bottom.position.x, valueBottom);
        Left.position = new Vector2(valueLeft, Centre.position.y);
        Right.position = new Vector2(valueRight, Centre.position.y);
    }
}