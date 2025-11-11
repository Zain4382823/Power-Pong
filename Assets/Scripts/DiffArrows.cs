using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffArrows : MonoBehaviour
{
    Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Get current mouse position in screen coordinates (bottom-left is (0,0))
        mousePos = Input.mousePosition;

        if (mousePos.y > 175)
            transform.position = new Vector3((float)-5.5, (float)-0.7);  // Normal
        else if (mousePos.y > 100)
            transform.position = new Vector3((float)-5.5, (float)-2.5);  // Hard
        else
            transform.position = new Vector3((float)-5.5, (float)-4.2);  // Nightmare
    }
}
