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
using Android.Views.Animations;

namespace ComeTogether.Droid
{
    class SearchAnimation : Animation
    {
        private View viewToAnim;
        private int originalHeight;
        private int targetHeight;
        private int growBy;

        public SearchAnimation(View _view, int _targetHeight)
        {
            viewToAnim = _view;
            originalHeight = _view.Height;
            targetHeight = _targetHeight;
            growBy = targetHeight - originalHeight;
        }

        protected override void ApplyTransformation(float interpolatedTime, Transformation t)
        {
            viewToAnim.LayoutParameters.Height = (int)(originalHeight + growBy * interpolatedTime);
            viewToAnim.RequestLayout();
        }

        /// <summary>
        /// Indicates whether or not this animation will affect the bounds of the animated view. 
        /// </summary>
        public override bool WillChangeBounds()
        {
            return true;
        }
    }
}