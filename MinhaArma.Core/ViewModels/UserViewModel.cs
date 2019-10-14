using MinhaArma.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinhaArma.Core.ViewModels
{
    public class UserViewModel : MvxViewModel
    {
        private string _cpfUsuario;
        private List<User> users;
        public static IMvxNavigationService _navigationService;

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public UserViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            RequisitarPosseCommand = new MvxAsyncCommand(() => _navigationService.Navigate<RequisitarViewModel>());
        }

        public IMvxAsyncCommand RequisitarPosseCommand { get; private set; }

        public string CpfUsuario
        {
            get => _cpfUsuario;
            set
            {
                _cpfUsuario = value;
                RaisePropertyChanged(() => CpfUsuario);
            }
        }

        private MvxCommand _acompanharProcessoCommand;

        public ICommand AcompanharProcessoCommand
        {
            get
            {
                _acompanharProcessoCommand = _acompanharProcessoCommand ?? new MvxCommand(DoAcompanharProcesso);
                return _acompanharProcessoCommand;
            }
        }

        private void DoAcompanharProcesso()
        {
            DoAcompanharProcessoAsync();
        }

        public async void DoAcompanharProcessoAsync()
        {
            if (CpfUsuario != null)
            {
                users = await DataService.GetUsuariosAsync();

                foreach (User user in users)
                {
                    if (user.Cpf == CpfUsuario)
                    {
                        await _navigationService.Close(this);
                        await _navigationService.Navigate<AcompanharProcessoViewModel, User>(user);
                        break;
                    }
                }
            }
        }
    }
}
