using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classes
{
    public class Variavel
    {
        public string nome { get; set; }
        public int val { get; set; }

        public Variavel(string strNome, int intVal)
        {
            this.nome = strNome;
            this.val = intVal;
        }
    }

    public class Rotulo
    {
        public string nome { get; set; }
        public int endereco { get; set; }

        public Rotulo(string strNome, int intEnd)
        {
            this.nome = strNome;
            this.endereco = intEnd;
        }
    }

    class Classes
    {
    }
}
