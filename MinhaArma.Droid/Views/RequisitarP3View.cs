using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using MinhaArma.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;
using Xamarin.Essentials;

namespace MinhaArma.Droid.Views
{
    [Activity(Label = "Minha Arma")]
    public class RequisitarP3View : MvxActivity<RequisitarP3ViewModel>
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RequisitarP3View);

            Button button = FindViewById<Button>(Resource.Id.buttonLink);

            //Assign The Event To Button
            button.Click += delegate
            {
                OpenBrowserAsync ();
            };
        }
        public async Task OpenBrowserAsync ()
        {
            try
            {
                Uri uri = new Uri("http://www.pf.gov.br/servicos-pf/armas/psicologos/psicologos-crediciados");
                await Browser.OpenAsync (uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}