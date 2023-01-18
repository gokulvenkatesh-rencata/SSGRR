using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using DigiFamily.Droid.Renderers;
using DigiFamily.Views.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StandardEntry), typeof(StandardEntryRenderer))]

namespace DigiFamily.Droid.Renderers
{
    public class StandardEntryRenderer : EntryRenderer
    {
        public StandardEntryRenderer(Context context) : base(context)
        {
        }

        public StandardEntry ElementV2 => Element as StandardEntry;
        protected override FormsEditText CreateNativeControl()
        {
            FormsEditText  control = base.CreateNativeControl();
            UpdateBackground(control);
            return control;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == StandardEntry.CornerRadiusProperty.PropertyName)
            {
                UpdateBackground();
            }else if (e.PropertyName == StandardEntry.BorderThicknessProperty.PropertyName)
            {
                UpdateBackground();
            }else if (e.PropertyName == StandardEntry.BorderColorProperty.PropertyName)
            {
                UpdateBackground();
            }

            base.OnElementPropertyChanged(sender, e);
        }

        protected override void UpdateBackgroundColor()
        {
            UpdateBackground();
        }
        protected void UpdateBackground(FormsEditText control)
        {
            if (control == null) return;

            GradientDrawable gd = new GradientDrawable();
            gd.SetColor(Element.BackgroundColor.ToAndroid());
            gd.SetCornerRadius(Context.ToPixels(ElementV2.CornerRadius));
            gd.SetStroke((int)Context.ToPixels(ElementV2.BorderThickness), ElementV2.BorderColor.ToAndroid());
            control.SetBackground(gd);

            int padTop = (int)Context.ToPixels(ElementV2.Padding.Top);
            int padBottom = (int)Context.ToPixels(ElementV2.Padding.Bottom);
            int padLeft = (int)Context.ToPixels(ElementV2.Padding.Left);
            int padRight = (int)Context.ToPixels(ElementV2.Padding.Right);

            control.SetPadding(padLeft, padTop, padRight, padBottom);
        }
        protected override void UpdateBackground()
        {
            UpdateBackground(Control);
        }
    }
}