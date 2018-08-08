using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TaskData : MonoBehaviour {


    public int Num_of_Tasks;
    public bool Scene_change;
    // Use this for initialization
    public class Task{
        string name;
        string remTime;

        public Task(string n, string t)
        {
            name = n;
            remTime = t;
        }


        public void SetName(string n)
        {
            name = n;
        }

        public void SetTime(string t)
        {
            remTime  = t;
        }

        public string GetName()
        {
            return name;
        }

        public string GetTime()
        {
            return remTime;
        }
    }
    GameObject cur_task;
    Task addTask;
    public static List<Task> task_array = new List<Task>();
    public static TaskData Instance;

    //Makes sure the master data stays present through both scenes
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start () {
        Scene_change = false;
        //This assigns the variable cur_task to one task at a time to get the name and time and add to the array
        for(int i = 0; i < Num_of_Tasks; i++)
        {
            if (i > 0)
            {
                cur_task = GameObject.Find("Task (" + i + ")");
            }
            else
            {
                cur_task = GameObject.Find("Task");
            }
            addTask = new Task("NULL", "12:00 AM");
            task_array.Add(addTask);
            
        }
	}
	
	// Update is called once per frame
	void Update () {
        //this checks the scene before running to prevent errors
        if (SceneManager.GetActiveScene().name == "TaskCreationScreen")
        {
            Scene_change = false;
            //Now I go through each task one by one and check if the name or time has been changed, and if either has, then it is updated in the list
            for (int a = 0; a < Num_of_Tasks; a++)
            {
                if (a > 0)
                {
                    cur_task = GameObject.Find("Task (" + a + ")");
                }
                else
                {
                    cur_task = GameObject.Find("Task");
                }
                string n = cur_task.GetComponent<TaskClassScript>().taskname;
                string t = cur_task.GetComponent<TaskClassScript>().remTime;
                if (n != task_array[a].GetName() && n != "NULL")
                {
                    task_array[a].SetName(n);
                }
                if (t != task_array[a].GetTime() && (t != "12:00 AM" && t != null))
                {
                    task_array[a].SetTime(t);
                                 
                }
            }
        }
        if((SceneManager.GetActiveScene().name == "TaskHomeScreen") && Scene_change == false)
        {
            Scene_change = true;
            //If the scene is on the home screen, then I will check the tasks and for each task that has something written,
            //I instantiate the habitat, changing the model depending on the streak count
            for(int b = 0; b < Num_of_Tasks; b++)
            {
                if(task_array[b].GetName() != "NULL")
                {
                    
                    GameObject test = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    //test.transform.localScale = new Vector3(Screen.width / 10, Screen.width / 10, 1);
                }
                
            }
        }
	}
}
