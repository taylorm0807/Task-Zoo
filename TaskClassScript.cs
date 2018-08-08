using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TaskClassScript : MonoBehaviour {


    // Use this for initialization
    public string taskname;
    public int streak;
    public bool status;
    public string remTime;
    bool loaded;
    public int id;
	void Start () {
        //loaded is a variable that checks if the scene was changed, and if it was it reloads all the saved data(name and rem_time)
        loaded = false;

        streak = 1;
        //This sets the taskname variable to the current text
        if (this.transform.Find("Text").GetComponent<Text>().text == "")
        {
            taskname = "NULL";
        }
        else
        {
            taskname = this.transform.Find("Text").GetComponent<Text>().text;
        }
        remTime = this.GetComponent<AddTask>().Get_ReminderTime();
	}
	
	// Update is called once per frame
	void Update () {

        //if this scene has just been reloaded, I want to be able to update the tasks with the correct names and times
        if(loaded == false)
        {
            //this makes sure that it isn't updating the task screen with NULL
            if (TaskData.task_array[id].GetName() != "NULL")
            {
                this.transform.Find("Text").GetComponent<Text>().text = TaskData.task_array[id].GetName();
                Destroy(this.transform.Find("TaskNameInput(Clone)").GetComponent<InputField>().placeholder);
            }
            loaded = true;

            //If there was a time other than midnight the user wanted, when the creation scene is reloaded, this function changes the ui back to that time
            if(TaskData.task_array[id].GetTime() != "12:00 AM" && TaskData.task_array[id].GetTime() != null)
            {
                this.GetComponent<AddTask>().Change_time(TaskData.task_array[id].GetTime());
            }
        }
        //If the text is blank, set the array text to null so that it doesn't break things later
        if (this.transform.Find("Text").GetComponent<Text>().text == "")
        {
            taskname = "NULL";
        }
        else {
            taskname = this.transform.Find("Text").GetComponent<Text>().text;
        }
        //The reminder time is taken every frame from the other script that checks all the dropdowns to update when the user wants it to
        remTime = this.GetComponent<AddTask>().Get_ReminderTime();
        
	}
}
