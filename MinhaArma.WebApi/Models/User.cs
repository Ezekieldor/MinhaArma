using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinhaArma.WebApi.Models
{
    public class User
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string DataNascimento { get; set; }
        public string Telefone { get; set; }

        public string Status { get; set; }

        public User(string nome, string cpf, string dataNascimento, string telefone, string status)
        {
            Nome = nome;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Status = status;
        }

        public User ()
        {

        }
    }
}
