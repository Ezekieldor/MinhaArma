using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MinhaArma.Core.ViewModels;
using MvvmCross.Platforms.Android.Views;

namespace MinhaArma.Droid.Views
{
    [Activity(Label = "Minha Arma")]
    public class AcompanharProcessoView : MvxActivity<AcompanharProcessoViewModel>
    {
        private ImageView image;
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AcompanharProcessoView);

            await SetImageAsync("http://www.minhaarma.somee.com/Images/" + AcompanharProcessoViewModel.UrlFoto + ".png");
        }

        public async Task SetImageAsync(string url)
        {
            using (var bm = await GetImageFromUrl(url))
            {
                image = FindViewById<ImageView>(Resource.Id.imageView);
                image.SetImageBitmap(bm);
            }
        }

        private async Task<Bitmap> GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(url);
                if (msg.IsSuccessStatusCode)
                {
                    using (var stream = await msg.Content.ReadAsStreamAsync())
                    {
                        var bitmap = await BitmapFactory.DecodeStreamAsync(stream);
                        return bitmap;
                    }
                }
            }
            return null;
        }
    }
}