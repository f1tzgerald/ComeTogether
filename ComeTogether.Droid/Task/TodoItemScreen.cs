using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using ComeTogether.Droid;

namespace ComeTogether.Droid
{
	/// <summary>
	/// View/edit a Task
	/// </summary>
	[Activity (Label = "Come Together")]			
	public class TodoItemScreen : Activity 
	{
		TodoItem task = new TodoItem();
		Button cancelDeleteButton;
        EditText taskTextEdit;
        TextView dateAddedTextEdit, dateFinish;
        Button setDateFinishButton, saveButton;
		CheckBox doneCheckbox;
        int categoryId;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
			int taskID = Intent.GetIntExtra("TaskID", 0);
            if (taskID > 0)
            {
				task = TodoItemManager.GetTask(taskID);
			}

            categoryId = Intent.GetIntExtra("CategoryId", 0);

            #region Initialize views
            // set our layout to be the home screen
            SetContentView(Resource.Layout.TaskDetails);

			taskTextEdit = FindViewById<EditText>(Resource.Id.TaskText);
			dateAddedTextEdit = FindViewById<TextView>(Resource.Id.DateAddedText);
            dateFinish = FindViewById<TextView>(Resource.Id.DateFinishText);

            setDateFinishButton = FindViewById<Button>(Resource.Id.ChangeDateFinishButton);
            saveButton = FindViewById<Button>(Resource.Id.SaveButton);
            
			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);

			// find all our controls
			cancelDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

            doneCheckbox.Checked = task.Done;
            // set the cancel delete based on whether or not it's an existing task
            cancelDeleteButton.Text = (task.ID == 0 ? "Cancel" : "Delete");
            #endregion

            taskTextEdit.Text = task.Name; 
			dateAddedTextEdit.Text = task.DateAdded;
            dateFinish.Text = task.DateFinish;

            // button clicks
            setDateFinishButton.Click += DateSelect_OnClick;
            saveButton.Click += (sender, e) => { Save(); };
            cancelDeleteButton.Click += (sender, e) => { CancelDelete(); };			
		}

		void Save()
		{
			task.Name = taskTextEdit.Text;
			task.DateFinish = dateFinish.Text;
			task.Done = doneCheckbox.Checked;
            task.CategoryId = categoryId;

			TodoItemManager.SaveTask(task);
			Finish();
		}
		
		void CancelDelete()
		{
			if (task.ID != 0)
            {
				TodoItemManager.DeleteTask(task.ID);
			}
			Finish();
		}

        void DateSelect_OnClick(object sender, System.EventArgs eventArgs)
        {
            DatePickerFragment frag = DatePickerFragment.NewInstance(delegate (System.DateTime time)
            { dateFinish.Text = time.ToShortDateString(); });
            frag.Show(FragmentManager, DatePickerFragment.TAG);
        }
    }
}