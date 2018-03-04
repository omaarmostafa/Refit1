using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Refit1.Effects;
using Refit1.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]
namespace Refit1.iOS.Effects
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var customButton = e.NewElement as CustomButton;

            var thisButton = Control as UIButton;
            thisButton.TouchDown += delegate
            {
                customButton.OnPressed();
            };
            thisButton.TouchUpInside += delegate
            {
                customButton.OnReleased();
            };
        }
    }
}