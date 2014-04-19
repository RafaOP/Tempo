using UnityEngine;
using System.Collections;

public class IngameGUI : MonoBehaviour
{
    private Rect prect = new Rect(0, 0, 30, 30);

    public Texture btnTexture;
    public GUIStyle style;

    public static bool mouseOverGUI = false;

    void Update()
    {
        mouseOverGUI = prect.Contains(new Vector3(Input.mousePosition.x, Input.mousePosition.y - Screen.height + prect.yMax, 0));
        //if (mouseOverGUI) Debug.Log("MOUSE OVER");
    }

    void LateUpdate()
    {
        mouseOverGUI = false;
    }

    void OnGUI()
    {
        var btn = GUI.Button(prect, btnTexture, style);

        if (!btnTexture)
        {
            Debug.Log("No texture!");
            return;
        }
  
        if (btn)
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;

        if (Time.timeScale == 0)
        {
            // Game is paused, draw a nice menu
            // OR should I load the other scene?
        }
    }
}
