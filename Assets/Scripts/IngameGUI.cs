using UnityEngine;
using System.Collections;

public class IngameGUI : MonoBehaviour
{
    public Vector3 menuCamPos = new Vector3(0.0F, 7.5F, -40.0F);
    public static Vector3 playCamPos;
    public static Quaternion playCamRot;

    private Rect prect = new Rect(0, 0, 50, 50);

    public Texture btnTexture;
    public GUIStyle style;

    public static bool mouseOverGUI = false;

    void Start() { Application.LoadLevelAdditive("IngameMenu"); }

    void Update()
    {
        Rect rect = new Rect(prect);
        rect.xMax *= 1.3F;
        rect.yMax *= 1.3F;
        mouseOverGUI = rect.Contains(new Vector3(Input.mousePosition.x, Input.mousePosition.y - Screen.height + rect.yMax, 0));

        //if (Input.GetKey(KeyCode.Escape)) pause();
    }

    void OnGUI()
    {
        if (Time.timeScale != 0)
        {
            if (!btnTexture) { Debug.Log("No texture!"); return; }

            var btn = GUI.Button(prect, btnTexture, style);

            if (btn)
            {
                playCamPos = Camera.main.transform.position;
                playCamRot = Camera.main.transform.localRotation;
                pause();
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        Camera.main.transform.position = menuCamPos;
        Quaternion q = new Quaternion(0.0f, 0.7071068f, 0.0f, -0.7071068f);
        Camera.main.transform.localRotation = q;
    }

    public void unpause()
    {
        Camera.main.transform.position = playCamPos;
        Camera.main.transform.localRotation = playCamRot;
        Time.timeScale = 1;
    }
}
