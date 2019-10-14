using MinhaArma.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.PictureChooser;
using MvvmCross.ViewModels;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinhaArma.Core.ViewModels
{
    public class RequisitarP2ViewModel : MvxViewModel<User>
    {
        private Stream imageRg, imageRequerimento, imageFoto, imageComprovanteResidencia;
        private readonly IMvxPictureChooserTask _pictureChooserTask;
        private User userParameter;

        public static IMvxNavigationService _navigationService;

        public override void Prepare (User parameter)
        {
            userParameter = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public RequisitarP2ViewModel (IMvxPictureChooserTask pictureChooserTask, IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            _pictureChooserTask = pictureChooserTask;
        }

        private MvxCommand _entrarCommand;

        public ICommand EntrarCommand
        {
            get
            {
                _entrarCommand = _entrarCommand ?? new MvxCommand (DoEntrar);
                return _entrarCommand;
            }
        }

        private void DoEntrar ()
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

        public void uploadImage (Stream imageStream, int cont)
        {
            FtpWebRequest req = (FtpWebRequest)WebRequest.Create("ftp://minhaarma.somee.com/www.minhaarma.somee.com/Images/" + userParameter.Cpf + "_" + cont.ToString () + ".png");
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
            //if (imageRg != null && imageRequerimento != null && imageFoto != null && imageComprovanteResidencia != null)
            if (imageFoto != null)
            {

               // uploadImage(imageRg, 1);
               // uploadImage(imageRequerimento, 2);
                 uploadImage(imageFoto, 3);
                //uploadImage(imageComprovanteResidencia, 4);

                await _navigationService.Close(this);
                await _navigationService.Navigate<RequisitarP3ViewModel, User>(userParameter);
            }
        }

        private MvxCommand _chooseRgCommand;

        public ICommand ChooseRgCommand
        {
            get
            {
                _chooseRgCommand = _chooseRgCommand ?? new MvxCommand(DoChooseRg);
                return _chooseRgCommand;
            }
        }

        private void DoChooseRg ()
        {
            _pictureChooserTask.ChoosePictureFromLibrary (400, 95, OnPictureRg, () => { });
        }

        private void OnPictureRg(Stream image)
        {
            imageRg = image;
        }

        private MvxCommand _chooseRequerimentoCommand;

        public ICommand ChooseRequerimentoCommand
        {
            get
            {
                _chooseRequerimentoCommand = _chooseRequerimentoCommand ?? new MvxCommand(DoChooseRequerimento);
                return _chooseFotoCommand;
            }
        }

        private void DoChooseRequerimento ()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPictureRequerimento, () => { });
        }

        private void OnPictureRequerimento (Stream image)
        {
            imageRequerimento = image;
        }

        private MvxCommand _chooseFotoCommand;

        public ICommand ChooseFotoCommand
        {
            get
            {
                _chooseFotoCommand = _chooseFotoCommand ?? new MvxCommand(DoChooseFoto);
                return _chooseFotoCommand;
            }
        }

        private void DoChooseFoto()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPictureFoto, () => { });
        }

        private void OnPictureFoto(Stream image)
        {
            imageFoto = image;
        }

        private MvxCommand _chooseComprovanteResidenciaCommand;

        public ICommand ChooseComprovanteResidenciaCommand
        {
            get
            {
                _chooseComprovanteResidenciaCommand = _chooseComprovanteResidenciaCommand ?? new MvxCommand(DoChooseComprovanteResidencia);
                return _chooseComprovanteResidenciaCommand;
            }
        }

        private void DoChooseComprovanteResidencia ()
        {
            _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPictureComprovanteResidencia, () => { });
        }

        private void OnPictureComprovanteResidencia (Stream image)
        {
            imageComprovanteResidencia = image;
        }
    }
}
