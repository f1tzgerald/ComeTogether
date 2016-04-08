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
    class CategoryAdapter : BaseAdapter<Category>
    {
        IList<Category> mCategoryItems;
        private Context context;

        public CategoryAdapter(Context _context, IList<Category> _mCategoryList)
        {
            mCategoryItems = _mCategoryList;
            context = _context;
        }

        public override Category this[int position]
        {
            get { return mCategoryItems[position]; }
        }

        public override int Count
        {
            get { return mCategoryItems.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.CategoryListItem, null, false);
            }

            TextView text = row.FindViewById<TextView>(Resource.Id.CategoryName);
            text.Text = mCategoryItems[position].Name;

            return row;
        }
    }
}