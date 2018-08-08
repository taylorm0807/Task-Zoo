using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AddTask : MonoBehaviour {
    public GameObject minutes;
    public GameObject hours;
    public GameObject AM;
    public GameObject button;
    public GameObject Task_Name;
    public GameObject task_text;
    int rem_hour;
    string rem_minute;
    string am_pm;
    string reminder_time;
    string current_time;
    string cur_hours_and_minutes;
    bool time_check;
    // Use this for initialization
    void Start() {
        //Create a list to store all of the possible minutes to add to the minute dropdown list
        List<string> options = new List<string>();
        //time check is a variable to see if a notifcaiton has already been pushed or not
        time_check = false;
        string minute;
        for (int a = 0; a < 60; a++)
        {
            if (a < 10)
            {
                minute = "0" + a;
                options.Add(minute);
            }
            if (a >= 10)
            {
                minute = "" + a;
                options.Add(minute);
            }
        }


        //This block is basically just instantiating all of the ui elements to be in the correct position and assign scripts for user input
        minutes.GetComponent<Dropdown>().AddOptions(options);
        Task_Name = (GameObject)Instantiate(Resources.Load("TaskNameInput"));
        Task_Name.transform.SetParent(this.transform);
        Task_Name.GetComponent<InputField>().transform.localPosition = new Vector3(-130.3f, -17.5f, 0);
        Task_Name.GetComponent<InputField>().GetComponentInChildren<Text>().text = "Enter Task Here...";
        Task_Name.GetComponent<InputField>().onEndEdit.AddListener(convert_to_text);
        button.GetComponent<Button>().onClick.AddListener(EditText);
        rem_hour = 12;
        rem_minute = "00";
        am_pm = "AM";
        hours.GetComponent<Dropdown>().onValueChanged.AddListener(Change_hours);
        minutes.GetComponent<Dropdown>().onValueChanged.AddListener(Change_minutes);
        AM.GetComponent<Dropdown>().onValueChanged.AddListener(Change_ampm);

    }

    //This is called after enter is hit in the text box. It deletes the input box for the task name and creates solid text instead
    void convert_to_text(string endstring)
    {
        task_text.GetComponent<Text>().text = endstring;
        Destroy(Task_Name);
    }
    //This is run if the little pencil button is hit to change the task name
    void EditText()
    {
        //checks if there is currently an input field or if it has been deleted. If there is still an input field, it doesnt do anything so you tupe in there instead of making a new box
        if (Task_Name == null)
        {
            //Creates a new textbox object and resets the text to null
            GameObject newTaskName = (GameObject)Instantiate(Resources.Load("TaskNameInput"));
            Task_Name = newTaskName;
            newTaskName.transform.SetParent(this.transform);
            newTaskName.GetComponent<InputField>().transform.localPosition = new Vector3(-130.3f, -17.5f, 0);
            newTaskName.GetComponent<InputField>().onEndEdit.AddListener(convert_to_text);
            task_text.GetComponent<Text>().text = "";
        }
    }

    //If you want to change the minutes,hours, or AM/PM. Pretty standard stuff
    void Change_hours(int hour)
    {
        rem_hour = hour + 1;
    }
    void Change_minutes(int min)
    {
        if(min < 11)
        {
            rem_minute = "0" + min; 
        }
        else
        {
            rem_minute = min + "";
        }
    }
    void Change_ampm(int time)
    {
        if(time == 0)
        {
            am_pm = "AM";
        }
        else
        {
            am_pm = "PM";
        }
    }

    //used to get the reminder time, used so the array of names and times can get the current reminder time
    public string Get_ReminderTime()
    {
        return reminder_time;
    }

    //Used from another script. If the scene is changed then comes back to the edit screen, this function will run if a task had a time other than midnight, and it will update the dropdown with the altered time
    public void Change_time(string newtime)
    {
        string h;
        string m;
        string timeofday;
        //this if statement runs if the reminder time set is a single digit hour 
        if(newtime.Substring(1,1) == ":")
        {
            h = newtime.Substring(0, 1);
            m = newtime.Substring(2, 2);
            timeofday = newtime.Substring(5, 2);
        }
        //if the hour is two digits
        else
        {
            h = newtime.Substring(0, 2);
            m = newtime.Substring(3, 2);
            timeofday = newtime.Substring(6, 2);
        }
        hours.GetComponent<Dropdown>().value = System.Int32.Parse(h) - 1;
        minutes.GetComponent<Dropdown>().value = System.Int32.Parse(m);
        if(timeofday == "AM")
        {
            AM.GetComponent<Dropdown>().value = 0;
        }
        else if(timeofday == "PM")
        {
            AM.GetComponent<Dropdown>().value = 1;
        }

    }
	// Update is called once per frame
	void Update () {
        //this is only executed if the scene was changed and there was a custom task name, because I can't delete the input field so i just deleted the placeholder text, so we need to delete the whole object
        if(Task_Name != null && Task_Name.GetComponent<InputField>().placeholder == null)
        {
            Destroy(Task_Name);
        }
        current_time = "" + System.DateTime.Now;
        //if the month is a single digit
        if (current_time.Substring(1, 1) == "/")
        {   
            //if the day is a single digit
            if (current_time.Substring(3, 1) == "/")
            {
                //if the time is currently m/d/yyyy h:mm:ss am
                if (current_time.Substring(10, 1) == ":")
                {
                    cur_hours_and_minutes = current_time.Substring(9, 4) + current_time.Substring(16, 3);
                }
                //if the time is currently m/d/yyyy hh:mm:ss am
                else
                {
                    cur_hours_and_minutes = current_time.Substring(9, 5) + current_time.Substring(17, 3);
                }
            }
            //if the day is double digits
            else
            {
                //if the time is currently m/dd/yyyy h:mm:ss am
                if(current_time.Substring(11,1) == ":")
                {
                    cur_hours_and_minutes = current_time.Substring(10, 4) + current_time.Substring(17, 3);
                }
                //if time is currently m/dd/yyyy hh:mm:ss am
                else
                {
                    cur_hours_and_minutes = current_time.Substring(10, 5) + current_time.Substring(18, 3);
                }
            }
        }
        //if month is double digits
        else
        {
            //if the day is a single digit
            if (current_time.Substring(4, 1) == "/")
            {
                //if the time is currently mm/d/yyyy h:mm:ss am
                if (current_time.Substring(11, 1) == ":")
                {
                    cur_hours_and_minutes = current_time.Substring(10, 4) + current_time.Substring(17, 3);
                }
                //if the time is currently mm/d/yyyy hh:mm:ss am
                else
                {
                    cur_hours_and_minutes = current_time.Substring(10, 5) + current_time.Substring(18, 3);
                }
            }
            //if the day is double digits
            else
            {
                //if the time is currently mm/dd/yyyy h:mm:ss am
                if (current_time.Substring(12, 1) == ":")
                {
                    cur_hours_and_minutes = current_time.Substring(11, 4) + current_time.Substring(18, 3);
                }
                //if time is currently mm/dd/yyyy hh:mm:ss am
                else
                {
                    cur_hours_and_minutes = current_time.Substring(11, 5) + current_time.Substring(19, 3);
                }
            }
        }
        reminder_time = rem_hour + ":" + rem_minute + " " + am_pm;

        //these statements check the time and see if the cur time is equal to the reminder time, and once a day it'll push a reminder
        if((cur_hours_and_minutes == reminder_time) && time_check == false)
        {
            print("Don't forget to " + task_text.GetComponent<Text>().text + " before midnight!");
            time_check = true;
        }
        if((cur_hours_and_minutes != reminder_time) && time_check == true)
        {
            time_check = false;
        }
	}
}
