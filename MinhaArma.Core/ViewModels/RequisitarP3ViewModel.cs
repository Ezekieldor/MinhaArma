using MinhaArma.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.ViewModels;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinhaArma.Core.ViewModels
{
    public class RequisitarP3ViewModel : MvxViewModel<User>
    {
        private Stream imageComprovante;
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private User userParameter;

        public static IMvxNavigationService _navigationService;

        public override void Prepare(User parameter)
        {
            userParameter = parameter;
        }

        public RequisitarP3ViewModel(IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _pictureChooserTask = pictureChooserTask;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        private MvxCommand _chooseComprovanteCommand;

        public ICommand ChooseComprovanteCommand
        {
            get
            {
                _chooseComprovanteCommand = _chooseComprovanteCommand ?? new MvxCommand(DoChooseComprovante);
                return _chooseComprovanteCommand;
            }
        }

        private void DoChooseComprovante()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPictureComprovante, () => { });
        }

        private void OnPictureComprovante(Stream image)
        {
            imageComprovante = image;
        }

        private MvxCommand _entrarCommand;

        public ICommand EntrarCommand
        {
            get
            {
                _entrarCommand = _entrarCommand ?? new MvxCommand(DoEntrar);
                return _entrarCommand;
            }
        }

        private void DoEntrar()
        {
            DoEntrarAsync();
        }

        public static byte[] ConverteStreamToByteArray(Stream stream)
        {
            byte[] byteArray = new byte[16 * 1024];
            using (MemoryStream mStream = new MemoryStream())
            {
                int bit;
                while ((bit = stream.Read(byteArray, 0, byteArray.Length)) > 0)
                {
                    mStream.Write(byteArray, 0, bit);
                }
                return mStream.ToArray();
            }
        }

        public void uploadImage(Stream imageStream, int cont)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://minhaarma.somee.com/www.minhaarma.somee.com/Images/" + userParameter.Cpf + "_" + cont.ToString() + ".png");
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential("Ezekieldor", "Eze92251442");

            byte[] fileData = ConverteStreamToByteArray(imageStream);

            req.ContentLength = fileData.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(fileData, 0, fileData.Length);
            reqStream.Close();
        }

        public async void DoEntrarAsync()
        {
            if (imageComprovante != null)
            {
                uploadImage(imageComprovante, 5);

                await _navigationService.Close(this);
                await _navigationService.Navigate<RequisitarP4ViewModel, User>(userParameter);
            }
        }
    }
}
