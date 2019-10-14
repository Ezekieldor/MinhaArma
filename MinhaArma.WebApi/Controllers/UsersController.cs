using MinhaArma.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MinhaArma.WebApi.Controllers
{
    public class UsersController : ApiController
    {
        public List<User> Get()
        {
            List<User> usuarios = new List<User>();
            using (SqlConnection con = new SqlConnection("workstation id=MinhaArma.mssql.somee.com;packet size=4096;user id=Ezekieldor_SQLLogin_1;pwd=gnagw972qc;data source=MinhaArma.mssql.somee.com;persist security info=False;initial catalog=MinhaArma"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var usuario = new User();
                                usuario.Nome = dr["Nome"].ToString();
                                usuario.DataNascimento = dr["DataNascimento"].ToString();
                                usuario.Telefone = dr["Telefone"].ToString();
                                usuario.Cpf = dr["Cpf"].ToString();
                                usuario.Status = dr["Status"].ToString();
                                usuarios.Add(usuario);
                            }
                        }
                        return usuarios;
                    }
                }

            }
        }
        public List<User> Get(int id)
        {
            List<User> usuarios = new List<User>();
            using (SqlConnection con = new SqlConnection("workstation id=MinhaArma.mssql.somee.com;packet size=4096;user id=Ezekieldor_SQLLogin_1;pwd=gnagw972qc;data source=MinhaArma.mssql.somee.com;persist security info=False;initial catalog=MinhaArma"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Cpf = " + id.ToString(), con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                var usuario = new User();
                                usuario.Nome = dr["Nome"].ToString();
                                usuario.Cpf = dr["Cpf"].ToString();
                                usuario.DataNascimento = dr["DataNascimento"].ToString();
                                usuario.Telefone = dr["Telefone"].ToString();
                                usuario.Status = dr["Status"].ToString();
                                usuarios.Add(usuario);
                            }
                        }
                        return usuarios;
                    }
                }
            }
        }

        public void Post(User user)
        {
            using (SqlConnection con = new SqlConnection("workstation id=MinhaArma.mssql.somee.com;packet size=4096;user id=Ezekieldor_SQLLogin_1;pwd=gnagw972qc;data source=MinhaArma.mssql.somee.com;persist security info=False;initial catalog=MinhaArma"))
            {
                string sql = "INSERT INTO Usuario (Nome, Cpf, DataNascimento, Telefone, Status) VALUES (@nome, @cpf, @dataNascimento, @telefone, @status)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nome", user.Nome);
                    cmd.Parameters.AddWithValue("@cpf", user.Cpf);
                    cmd.Parameters.AddWithValue("@dataNascimento", user.DataNascimento);
                    cmd.Parameters.AddWithValue("@telefone", user.Telefone);
                    cmd.Parameters.AddWithValue("@status", user.Status);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
