using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Changescenes : MonoBehaviour {

    // Use this for initialization
    GameObject button;
    void Start()
    {
        if (GameObject.Find("XButton"))
        {

            button = GameObject.Find("XButton");
        }
        else if (GameObject.Find("PlusButton"))
        {
            button = GameObject.Find("PlusButton");
        }
        button.GetComponent<Button>().onClick.AddListener(Change_scene);
	}
	
    void Change_scene()
    {
        if(SceneManager.GetActiveScene().name == "TaskCreationScreen")
        {
            SceneManager.LoadScene("TaskHomeScreen");
        }
        if (SceneManager.GetActiveScene().name == "TaskHomeScreen")
        {
            SceneManager.LoadScene("TaskCreationScreen");
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
