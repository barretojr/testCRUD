using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using testCRUD.Models;

namespace testCRUD.Controllers
{
    public class PessoasController : Controller
    {
        private readonly string connectionString = "Data Source=192.168.5.6,49170;Initial Catalog=TestCRUD;User ID=sa;Password=Alemanha1982";
        
        
        public ActionResult Index()
        {
            List<Pessoa> pessoas = new List<Pessoa>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("MostrarCadastroPessoa", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        pessoa.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                        pessoa.TelefoneFixo = reader.IsDBNull("TelefoneFixo") ? 0 : reader.GetInt32("TelefoneFixo");
                        pessoa.TelefoneCel = reader.IsDBNull("TelefoneCelular") ? 0 : reader.GetInt32("TelefoneCelular");
                        pessoa.Email = reader.GetString(reader.GetOrdinal("Email"));
                        pessoa.Sexo = reader.GetInt32(reader.GetOrdinal("SexoId"));
                        pessoa.ECivil = reader.GetInt32(reader.GetOrdinal("EstadoCivilId"));
                        pessoa.Salario = reader.IsDBNull("Salario") ? 0: reader.GetDecimal("Salario");

                        pessoas.Add(pessoa);
                    }
                }
            }
            return View(pessoas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))                
                using (SqlCommand cmd = new SqlCommand("CriarCadastroPessoa", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@TelefoneFixo", pessoa.TelefoneFixo);
                    cmd.Parameters.AddWithValue("@TelefoneCelular", pessoa.TelefoneCel);
                    cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                    cmd.Parameters.AddWithValue("@SexoId", pessoa.Sexo);
                    cmd.Parameters.AddWithValue("@EstadoCivilId", pessoa.ECivil);
                    cmd.Parameters.AddWithValue("@Salario", pessoa.Salario);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Pessoa> pessoas = new List<Pessoa>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("MostrarCadastroPessoaId", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        pessoa.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                        pessoa.TelefoneFixo = reader.IsDBNull("TelefoneFixo") ? 0 : reader.GetInt32("TelefoneFixo");
                        pessoa.TelefoneCel = reader.IsDBNull("TelefoneCelular") ? 0 : reader.GetInt32("TelefoneCelular");
                        pessoa.Email = reader.GetString(reader.GetOrdinal("Email"));
                        pessoa.Sexo = reader.GetInt32(reader.GetOrdinal("SexoId"));
                        pessoa.ECivil = reader.GetInt32(reader.GetOrdinal("EstadoCivilId"));
                        pessoa.Salario = reader.IsDBNull("Salario") ? 0 : reader.GetDecimal("Salario");

                        pessoas.Add(pessoa);
                    }
                }
            }

            return View(pessoas.FirstOrDefault());
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            List<Pessoa> pessoas = new List<Pessoa>();

            using (SqlConnection con = new SqlConnection(connectionString))            
            using (SqlCommand cmd = new SqlCommand("MostrarCadastroPessoaId", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        pessoa.Nome = reader.GetString(reader.GetOrdinal("Nome"));
                        pessoa.TelefoneFixo = reader.IsDBNull("TelefoneFixo") ? 0 : reader.GetInt32("TelefoneFixo");
                        pessoa.TelefoneCel = reader.IsDBNull("TelefoneCelular") ? 0 : reader.GetInt32("TelefoneCelular");
                        pessoa.Email = reader.GetString(reader.GetOrdinal("Email"));
                        pessoa.Sexo = reader.GetInt32(reader.GetOrdinal("SexoId"));
                        pessoa.ECivil = reader.GetInt32(reader.GetOrdinal("EstadoCivilId"));
                        pessoa.Salario = reader.IsDBNull("Salario") ? 0 : reader.GetDecimal("Salario");

                        pessoas.Add(pessoa);
                    }
                }
            }

            return View(pessoas.FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pessoa pessoa)
        {
            if(id != pessoa.Id)
            {
                return NotFound();
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))                
                using (SqlCommand cmd = new SqlCommand("EditarCadastroPessoa", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmd.Parameters.AddWithValue("@TelefoneFixo", pessoa.TelefoneFixo);
                    cmd.Parameters.AddWithValue("@TelefoneCelular", pessoa.TelefoneCel);
                    cmd.Parameters.AddWithValue("@Email", pessoa.Email);
                    cmd.Parameters.AddWithValue("@SexoId", pessoa.Sexo);
                    cmd.Parameters.AddWithValue("@EstadoCivilId", pessoa.ECivil);
                    cmd.Parameters.AddWithValue("@Salario", pessoa.Salario);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            List<Pessoa> pessoas = new List<Pessoa>();

            using (SqlConnection con = new SqlConnection(connectionString))            
            using (SqlCommand cmd = new SqlCommand("MostrarCadastroPessoaId", con))
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Pessoa pessoa = new Pessoa();
                        pessoa.Id = reader.GetInt32(reader.GetOrdinal("id"));
                        pessoa.Nome = reader.GetString(reader.GetOrdinal("Nome"));                        
                        pessoa.TelefoneFixo = reader.IsDBNull("TelefoneFixo") ? 0 : reader.GetInt32("TelefoneFixo");
                        pessoa.TelefoneCel = reader.IsDBNull("TelefoneCelular") ? 0 : reader.GetInt32("TelefoneCelular");
                        pessoa.Email = reader.GetString(reader.GetOrdinal("Email"));
                        pessoa.Sexo = reader.GetInt32(reader.GetOrdinal("SexoId"));
                        pessoa.ECivil = reader.GetInt32(reader.GetOrdinal("EstadoCivilId"));
                        pessoa.Salario = reader.IsDBNull("Salario") ? 0 : reader.GetDecimal("Salario");

                        pessoas.Add(pessoa);
                    }
                }
            }

            return View(pessoas.FirstOrDefault());
        }
        public ActionResult DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("ExcluirCadastroPessoa", con))
                {
                    con.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}