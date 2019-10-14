
using Android.App;
using Android.OS;
using Android.Widget;
using MinhaArma.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace MinhaArma.Droid.Views
{
    [Activity(Label = "Minha Arma", MainLauncher = true)]
    public class UserView : MvxActivity<UserViewModel>
    {
        protected async override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.UserView);
        }
    }
}