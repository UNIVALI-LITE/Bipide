using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BIP.Montador
{
    //======================================================
    /// <summary>Contém as configurações do Montador</summary>
    public class Configuracoes
    {
        /// <summary>Indica se o montador removerá as variaveis não utilizadas</summary>
        public bool RemoverVariaveisNaoUtilizadas { get { return _blnRemoverVariaveisNaoUtilizadas; } set { this._blnRemoverVariaveisNaoUtilizadas = value; } }
        private bool _blnRemoverVariaveisNaoUtilizadas = false;

    }
}
