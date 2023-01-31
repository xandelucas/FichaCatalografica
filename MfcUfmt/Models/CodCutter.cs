using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MfcUfmt.Models
{
    [Serializable]
    public class Cutter
    {
        private int _codigoCutter;

        public int CodigoCutter
        {
            get { return _codigoCutter; }
            set { _codigoCutter = value; }
        }
        private String _sobrenome;

        public String Sobrenome
        {
            get { return _sobrenome; }
            set { _sobrenome = value; }
        }
        private String _nome;

        public String Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
    }

}
