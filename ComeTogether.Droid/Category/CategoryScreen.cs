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
using Android.Content.PM;
using Android.Views.InputMethods;

namespace ComeTogether.Droid
{
    [Activity(Label = "ToDoList",
        Icon = "@drawable/icon",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class CategoryScreen : Activity
    {
        Button addCategoryBtn;
        ListView categoryListView;
        CategoryAdapter categoryList;
        IList<Category> categories;
        EditText searchCategoryET;
        RelativeLayout lvContainer;

        bool animatedDown;
        bool isAnimating;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.CategoryScreen);
            
            categoryListView = FindViewById<ListView>(Resource.Id.CategoryList);
            addCategoryBtn = FindViewById<Button>(Resource.Id.AddCategoryButton);
            searchCategoryET = FindViewById<EditText>(Resource.Id.searchCategoryET);
			lvContainer = FindViewById<RelativeLayout>(Resource.Id.lvContainer);
            lvContainer.BringToFront();

            searchCategoryET.Alpha = 0;
            searchCategoryET.TextChanged += SearchCategoryET_TextChanged;

            // Wire up add task button handler
            if (addCategoryBtn != null)
            {
                addCategoryBtn.Click += (sender, e) =>
                {
                    // Open dialog for add new category
                    FragmentTransaction ftransaction = FragmentManager.BeginTransaction();
                    DialogAddNewCategory dialogCategory = new DialogAddNewCategory();
                    dialogCategory.Show(ftransaction, "Category_dialog");

                    dialogCategory.OnAddComplete += DialogTask_OnAddComplete;
                };
            }

            // wire up task click handler
            if (categoryListView != null)
            {
                categoryListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
                {
                    var categoryDetails = new Intent(this, typeof(Screens.TasksScreen));
                    categoryDetails.PutExtra("CategoryID", categories[e.Position].Id);
                    StartActivity(categoryDetails);
                };
            }
        }

        private void SearchCategoryET_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            List<Category> searchedCategory = (from categ in categories
                                               where categ.Name.Contains(searchCategoryET.Text, StringComparison.OrdinalIgnoreCase)
                                               select categ).ToList<Category>();

            // Change refresh view method
            categoryList = new CategoryAdapter(this, searchedCategory);
            categoryListView.Adapter = categoryList;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            // If some menu item has been clicked
            switch (item.ItemId)
            {
                case Resource.Id.search:
                    if (isAnimating) return true;
                    if (!animatedDown)
                    {
                        // Animate list to move down
						SearchAnimation sanim = new SearchAnimation(lvContainer, lvContainer.Height - searchCategoryET.Height);
                        sanim.Duration = 500;
                        categoryListView.StartAnimation(sanim);
                        sanim.AnimationStart += (sender, e) => 
                        {
                            isAnimating = true;
                            searchCategoryET.Animate().AlphaBy(1.0f).SetDuration(500).Start();
                        };
                        sanim.AnimationEnd += (sender, e) =>
                        {
                            isAnimating = false;
                        };
                        lvContainer.Animate().TranslationYBy(searchCategoryET.Height).SetDuration(500).Start();
                    }
                    else
                    {
                        // Animate list to move up
						SearchAnimation sanim = new SearchAnimation(lvContainer, lvContainer.Height + searchCategoryET.Height);
                        sanim.Duration = 500;
                        categoryListView.StartAnimation(sanim);
                        sanim.AnimationStart += (sender, e) =>
                        {
                            isAnimating = true;
                            searchCategoryET.Animate().AlphaBy(-1.0f).SetDuration(100).Start();
                        };
                        sanim.AnimationEnd += Anim_AnimationEnd;
                        lvContainer.Animate().TranslationYBy(-searchCategoryET.Height).SetDuration(500).Start();
                    }
                    animatedDown = !animatedDown;
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void Anim_AnimationEnd(object sender, Android.Views.Animations.Animation.AnimationEndEventArgs e)
        {
            isAnimating = false;
            searchCategoryET.ClearFocus();
            InputMethodManager inputManager = (InputMethodManager) this.GetSystemService(Context.InputMethodService);
            inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
        }

        protected override void OnResume()
        {
            base.OnResume();
            RefreshView();
        }

        private void RefreshView()
        {
            categories = TodoItemManager.GetCategories();

            // create our adapter
            categoryList = new CategoryAdapter(this, categories);

            //Hook up our adapter to our ListView
            categoryListView.Adapter = categoryList;
            RegisterForContextMenu(categoryListView);
        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.CategoryList)
            {
                var info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                menu.SetHeaderTitle(categories[info.Position].Name);
                var menuItems = Resources.GetStringArray(Resource.Array.context_menu);
                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;

            switch (item.ItemId)
            {
                case 0: 
                    // Edit - Rename category
                    break;
                case 1: //Delete
                    // Are you sure
                    TodoItemManager.DeleteCategory(categories[info.Position].Id);
                    break;
                default:
                    Toast.MakeText(this, "Some problem", ToastLength.Short).Show();
                    break;
            }
            return true;
        }

        private void DialogTask_OnAddComplete(object sender, OnAddCategoryEventArgs e)
        {
            TodoItemManager.SaveCategory(e.CategoryToAdd);
            RefreshView();
        }
    }
}