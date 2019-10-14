using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MinhaArma.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;
using Xamarin.Essentials;

namespace MinhaArma.Droid.Views
{
    [Activity(Label = "Minha Arma")]
    public class RequisitarP4View : MvxActivity<RequisitarP4ViewModel>
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RequisitarP4View);

            Button button = FindViewById<Button>(Resource.Id.buttonLink);

            //Assign The Event To Button
            button.Click += delegate
            {
                OpenBrowserAsync();
            };
        }
        public async Task OpenBrowserAsync()
        {
            try
            {
                Uri uri = new Uri("http://www.pf.gov.br/servicos-pf/armas/instrutores-de-armamento-e-tiro/orientacao-para-credenciamento/listagem-de-instrutores-de-armamento-e-tiro");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}