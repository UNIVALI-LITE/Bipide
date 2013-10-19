using BIPIDE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIPIDE_4._0
{
    class Otimizacao
    {

        public Codigo OptimizesSTOFollowedByLD(Codigo _Source)
        {
            List<InstrucaoASM> _Result = _Source.GetCodigoInstrucaoASM();

            int             _Index                  = -1;
            String          _Operating              = String.Empty;
            List<int>       _IndexesToBeExcluded    = new List<int>();
            List<String>    _VarToBeExcluded        = new List<String>();
            bool            _OptimizationRealized   = false;

            foreach (InstrucaoASM itemASM in _Result)
            {
                if (_Index != -1)
                {
                    if ((itemASM.Instrucao.Equals("LD") ||
                        itemASM.Instrucao.Equals("LDI") ||
                        itemASM.Instrucao.Equals("LDV")) &&
                        _Operating.Equals(itemASM.Operando))
                    {
                        _IndexesToBeExcluded.Add(_Result.IndexOf(itemASM));
                        _Index          = -1;
                        _OptimizationRealized = true;
                        _Operating      = String.Empty;
                    }
                }

                if (itemASM.Instrucao.Equals("STO") ||
                    itemASM.Instrucao.Equals("STOV"))
                {
                    _Index = _Result.IndexOf(itemASM);
                    _Operating = itemASM.Operando;
                }
            }
            if (_OptimizationRealized)
            {
                Codigo _NewSource = new Codigo();
                foreach (InstrucaoASM itemASM in _Result)
                    if (!IndexIsOnTheList(_IndexesToBeExcluded, _Result.IndexOf(itemASM)))                       
                        {
                            if (itemASM.Tipo == eTipo.Variavel)
                                _NewSource.AddInstrucaoASM(itemASM.Instrucao, itemASM.Operando, itemASM.Tipo, itemASM.NrLinha, itemASM.Tamanho);
                            else
                                _NewSource.AddInstrucaoASM(itemASM.Instrucao, itemASM.Operando, itemASM.Tipo, itemASM.NrLinha);
                        }
                return _NewSource;
            }

            return _Source;  

        }

        private bool IndexIsOnTheList(List<int> _IndexesToBeExcluded, int _IndexToVerificate)
        {
            foreach (int index in _IndexesToBeExcluded)            
                if (index == _IndexToVerificate)
                    return true;           

                return false;
        }

        private bool VarIsOnTheList(List<String> _VarToBeExcluded, String _VarToVerificate)
        {
            foreach (String var in _VarToBeExcluded)
                if (var.ToUpper() == _VarToVerificate.ToUpper())
                    return true;

            return false;
        }


    }
}
