using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MinhaArma.Core.Services
{
    public class DataService
    {
        public static HttpClient client = new HttpClient();

        public static async Task<List<User>> GetUsuariosAsync()
        {
            try
            {
                string url = "http://www.minhaarma.somee.com/api/Users";
                var response = await client.GetStringAsync(url);
                var usuarios = JsonConvert.DeserializeObject<List<User>>(response);

                return usuarios;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task AddUsuarioAsync (User usuario)
        {
            try
            {
                string url = "http://www.minhaarma.somee.com/api/Users/{0}";
                var uri = new Uri(string.Format(url, usuario.Cpf));

                var data = JsonConvert.SerializeObject(usuario);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao incluir usuario");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
