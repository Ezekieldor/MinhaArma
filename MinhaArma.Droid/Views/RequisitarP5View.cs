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
    public class RequisitarP5View : MvxActivity<RequisitarP5ViewModel>
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RequisitarP5View);

            Button button = FindViewById<Button>(Resource.Id.buttonLink);

            //Assign The Event To Button
            button.Click += delegate
            {
                OpenBrowserAsync();
            };

            button = FindViewById<Button>(Resource.Id.buttonFinalizar);

            //Assign The Event To Button
            button.Click += delegate
            {
                Toast.MakeText(this, "Agora é só acompanhar o andamento do processo!", ToastLength.Short).Show();
            };  
        }
        public async Task OpenBrowserAsync()
        {
            try
            {
                Uri uri = new Uri("http://consulta.tesouro.fazenda.gov.br/gru_novosite/gru_simples.asp");
                await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}