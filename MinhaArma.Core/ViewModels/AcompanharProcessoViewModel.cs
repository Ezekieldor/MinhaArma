using MinhaArma.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinhaArma.Core.ViewModels
{
    public class AcompanharProcessoViewModel : MvxViewModel<User>
    {
        public static string _urlFoto;
        private string _nome, _dataNascimento, _telefoneUsuario, _statusUsuario;
        private User userParameter;

        public static IMvxNavigationService _navigationService;

        public override void Prepare(User parameter)
        {
            userParameter = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

            _nome = userParameter.Nome;
            _dataNascimento = userParameter.DataNascimento;
            _telefoneUsuario = userParameter.Telefone;
            _statusUsuario = userParameter.Status;
            _urlFoto = userParameter.Cpf + "_3";
        }

        public AcompanharProcessoViewModel (IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public static string UrlFoto
        {
            get => _urlFoto;
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
            await _navigationService.Close(this);
            await _navigationService.Navigate<UserViewModel>();
        }

        public string NomeUsuario
        {
            get => _nome;
        }

        public string DataNascimentoUsuario
        {
            get => _dataNascimento;
        }

        public string TelefoneUsuario
        {
            get => _telefoneUsuario;
        }

        public string StatusUsuario
        {
            get => _statusUsuario;
        }
    }
}
