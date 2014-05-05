using UnityEngine;
using System.Collections;

// How do I make it support many resolutions?

public class IngameMenu : MonoBehaviour
{
    public IngameGUI igui;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {	
	}

    void OnMouseDown()
    {
        Debug.Log(name + " clicked =D");
        if (name == "Continue")
        {
            Time.timeScale = 1;
            Camera.main.transform.localRotation = IngameGUI.playCamRot;
            Camera.main.transform.position = IngameGUI.playCamPos;
        }

        if (name == "Exit")
        {
            Debug.Log("Quitei");
            Application.Quit();
        }

        if (name == "RetCheckpoint")
        {
            Player p = GameObject.Find("Player").GetComponent<Player>();
            ((IngameGUI)Camera.main.GetComponent(typeof(IngameGUI))).unpause();
            p.returnToCheckpoint();
        }
    }
}
