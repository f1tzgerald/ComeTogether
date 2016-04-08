using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ComeTogether.Droid;
using TaskyAndroid.ApplicationLayer;
using Android.Content.PM;
using System.Linq;

namespace ComeTogether.Droid
{
	/// <summary>
	/// Main ListView screen displays a list of tasks, plus an [Add] button
	/// </summary>
	[Activity (Label = "Tasky",  
		Icon="@drawable/icon",
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
		ScreenOrientation = ScreenOrientation.Portrait)]
	public class TasksScreen : Activity 
	{
		private TodoItemListAdapter taskList;
		private IList<TodoItem> tasks;

		private Button addTaskButton;
		private ListView taskListView;
        private TextView taskNameHeader;
        private TextView statusHeader;

        private bool isTaskAsc;
        private bool isStatusAsc;

        private int categoryId;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            categoryId = Intent.GetIntExtra("CategoryID", 0);

            // set our layout to be the home screen
            SetContentView(Resource.Layout.TasksScreen);

			//Find our controls
			taskListView = FindViewById<ListView> (Resource.Id.TaskList);
			addTaskButton = FindViewById<Button> (Resource.Id.AddButton);
            taskNameHeader = FindViewById<TextView>(Resource.Id.TaskNameHeader);
            statusHeader = FindViewById<TextView>(Resource.Id.StatusHeader);

            taskNameHeader.Click += TaskNameHeader_Click;
            statusHeader.Click += StatusHeader_Click;
			addTaskButton.Click += (sender, e) =>
            {
                var taskDetails = new Intent(this, typeof(TodoItemScreen));
                taskDetails.PutExtra("CategoryId", categoryId);
                StartActivity(taskDetails);
            };			

			taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => 
            {
				var taskDetails = new Intent (this, typeof (TodoItemScreen));
				taskDetails.PutExtra ("TaskID", tasks[e.Position].ID);
                taskDetails.PutExtra ("CategoryId", categoryId);
                StartActivity(taskDetails);
			};

            
		}

        /// <summary>Sort the task list to asc/desc order by status</summary>
        private void StatusHeader_Click(object sender, System.EventArgs e)
        {
            List<TodoItem> sortedTasksStatus;

            if (!isStatusAsc)
            {
                sortedTasksStatus = (from task in tasks
                                     orderby task.Done
                                     select task).ToList<TodoItem>();
            }
            else
            {
                sortedTasksStatus = (from task in tasks
                                     orderby task.Done descending
                                     select task).ToList<TodoItem>();
            }
            isStatusAsc = !isStatusAsc;

            // Refresh view
            taskList = new TodoItemListAdapter(this, sortedTasksStatus);
            taskListView.Adapter = taskList;
        }

        /// <summary>Sort the task list to asc/desc order by task name</summary>
        private void TaskNameHeader_Click(object sender, System.EventArgs e)
        {
            List<TodoItem> sortedTaskName;

            if (!isTaskAsc)
            {
                sortedTaskName = (from task in tasks
                                  orderby task.Name
                                  select task).ToList();
            }
            else
            {
                sortedTaskName = (from task in tasks
                                  orderby task.Name descending
                                  select task).ToList();
            }
            isTaskAsc = !isTaskAsc;

            // Refresh view
            taskList = new TodoItemListAdapter(this, sortedTaskName);
            taskListView.Adapter = taskList;
        }

        protected override void OnResume ()
		{
			base.OnResume ();

			tasks = TodoItemManager.GetTasks(categoryId);
			
			// create our adapter
			taskList = new TodoItemListAdapter(this, tasks);

			//Hook up our adapter to our ListView
			taskListView.Adapter = taskList;
		}
	}
}