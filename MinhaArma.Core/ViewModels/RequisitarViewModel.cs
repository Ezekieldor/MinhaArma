using MinhaArma.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinhaArma.Core.ViewModels
{
    public class RequisitarViewModel : MvxViewModel
    {
        private string nomeUsuario, cpfUsuario, dataNascimentoUsuario, telefoneUsuario;
        public static IMvxNavigationService _navigationService;

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public RequisitarViewModel (IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
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

        public async void DoEntrarAsync()
        {
            if (NomeUsuario != null && CpfUsuario != null && DataNascimentoUsuario != null && TelefoneUsuario != null)
            {
                if (NomeUsuario.Length > 0 && CpfUsuario.Length > 0 && DataNascimentoUsuario.Length > 0 && TelefoneUsuario.Length > 0)
                {
                    User user = new User(NomeUsuario, CpfUsuario, DataNascimentoUsuario, TelefoneUsuario, "Em Andamento");
                    await DataService.AddUsuarioAsync(user);
                    await _navigationService.Close(this);
                    await _navigationService.Navigate<RequisitarP2ViewModel, User>(user);
                }
            }
        }

        public string NomeUsuario
        {
            get => nomeUsuario;
            set
            {
                nomeUsuario = value;
                RaisePropertyChanged(() => NomeUsuario);
            }
        }

        public string CpfUsuario
        {
            get => cpfUsuario;
            set
            {
                cpfUsuario = value;
                RaisePropertyChanged(() => CpfUsuario);
            }
        }

        public string DataNascimentoUsuario
        {
            get => dataNascimentoUsuario;
            set
            {
                dataNascimentoUsuario = value;
                RaisePropertyChanged(() => DataNascimentoUsuario);
            }
        }

        public string TelefoneUsuario
        {
            get => telefoneUsuario;
            set
            {
                telefoneUsuario = value;
                RaisePropertyChanged(() => TelefoneUsuario);
            }
        }
    }
}
