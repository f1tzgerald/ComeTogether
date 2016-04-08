using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ComeTogether.Droid
{
    class DialogAddNewCategory : DialogFragment
    {
        private EditText txtCategoryName;
        private Button btnAddNewCategory;

        public event EventHandler<OnAddCategoryEventArgs> OnAddComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            var view = inflater.Inflate(Resource.Layout.dialog_AddNewCategory, container, false);
            txtCategoryName = view.FindViewById<EditText>(Resource.Id.dlgCategoryName);
            btnAddNewCategory = view.FindViewById<Button>(Resource.Id.dlgBtnAddNewCategory);
            btnAddNewCategory.Click += BtnAddNewCategory_Click;
            return view;
        }

        private void BtnAddNewCategory_Click(object sender, EventArgs e)
        {
            // Touch the add button -> add task and close dialog
            OnAddCategoryEventArgs tasktoAdd = new OnAddCategoryEventArgs(txtCategoryName.Text);

            Console.WriteLine(new string('-', 20));
            Console.WriteLine(tasktoAdd.ToString());
            Console.WriteLine(new string('-', 20));

            OnAddComplete.Invoke(this, tasktoAdd);
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);  // Hide the title bar
            base.OnActivityCreated(savedInstanceState);
			Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_add_category_animation;
        }
    }

    public class OnAddCategoryEventArgs : EventArgs
    {
        private string categoryName;
        private Category categoryToAdd;

        public string CategoryName
        {
            get
            { return categoryName; }
            set
            { categoryName = value; }
        }

        internal Category CategoryToAdd
        {
            set { categoryToAdd = value; }
            get { return categoryToAdd; }
        }

        public OnAddCategoryEventArgs(string _text) : base()
        {
            CategoryName = _text;

            CategoryToAdd = new Category() {  Name = CategoryName };
        }

        public override string ToString()
        {
            return string.Format("[CategoryToAdd: Category Name = {0}]", CategoryName);
        }
    }
}