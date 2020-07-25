using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class Aluno
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public string telefone { get; set; }
        public int ra { get; set; }

        public List<Aluno> ListarAlunos()
        {
            var caminhoarquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = System.IO.File.ReadAllText(caminhoarquivo);
            var listaAlunos = JsonConvert.DeserializeObject<List<Aluno>>(json);

            return listaAlunos;
        }

        public bool ReescreverArquivo(List<Aluno> listaAlunos)
        {
            var caminhoarquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");
            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);
            File.WriteAllText(caminhoarquivo, json);

            return true;
        }

        public Aluno Inserir(Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var maxId = listaAlunos.Max(p => p.id);
            Aluno.id = maxId + 1;
            listaAlunos.Add(Aluno);

            ReescreverArquivo(listaAlunos);

            return Aluno;
        }

        public Aluno Atualizar(int id, Aluno Aluno)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(p => p.id == Aluno.id);
            if (itemIndex >= 0)
            {
                Aluno.id = id;
                listaAlunos[itemIndex] = Aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaAlunos);
            return Aluno;

        }

        public bool Deletar(int id)
        {
            var listaAlunos = this.ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(p => p.id == id);
            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAlunos);
            return true;
        }
    }
}