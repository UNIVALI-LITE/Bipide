using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Linq;
using System.Linq;
namespace BIPIDE.Classes
{
    public class Codigo
    {
        public Codigo()
        {
            InstrucaoASM.Init();
            this.intTamanhoVetor = 0;
        }

        public Codigo( bool pClear )
        {
            if (pClear)
                InstrucaoASM.Init();
            this.intTamanhoVetor = 0;
        }

        private int intTamanhoVetor = 0;

        #region PUBLIC
        public List<InstrucaoASM>   listaDataASM            = new List<InstrucaoASM>();
        public List<InstrucaoASM>   listaTextASM            = new List<InstrucaoASM>();
        public List<InstrucaoASM>   listaInstrucaoASM       = new List<InstrucaoASM>();
        public List<String>         listaStringASM          = new List<String>();

        public int Tamanho
        {
            get { return this.intTamanhoVetor; }
            set
            {
                this.intTamanhoVetor = value;                
            }
        }
        #endregion

        #region METODOS        
        //==========================================================
        public void AddInstrucaoASM(string strInst, string strValue, BIPIDE.Classes.eTipo tipo, int? linha)
        {
            //se for uma seção verifica se já foi adicionada
            if (BIPIDE.Classes.eTipo.Section == tipo)
                if (this.sectionFound(strInst))
                    return;
                else
                    if (strInst == ".DATA")
                        this.listaDataASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha });
                    else
                        this.listaTextASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha });
            else
            {
                if (tipo == BIPIDE.Classes.eTipo.Variavel)
                {
                    //adicionar as variáveis assembly no inicio do código
                    this.listaDataASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha });
                }
                else
                {
                    this.listaTextASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha });
                }
            }
        }
        public void AddInstrucaoASM(string strInst, string strValue, BIPIDE.Classes.eTipo tipo, int? linha, int tam)
        {
            if (tipo == BIPIDE.Classes.eTipo.Variavel)            
                this.listaDataASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha, Tamanho = tam});                           
            
        }

        public void AddInstrucaoASM(Codigo codigoASM)
        {
            foreach (InstrucaoASM item in codigoASM.GetCodigoInstrucaoASM())
                listaTextASM.Add(item);
        }

        //seta o tamanho da variável
        public void setTamanhoVariavel(string strNomevar, int tam)
        {
                foreach (InstrucaoASM a in listaDataASM)                    
                        if (a.Instrucao.ToUpper() == strNomevar.ToUpper())
                            a.Tamanho = tam;
            
        }
        //===========verifica se uma determinada seção já existe====
        public bool sectionFound(string section)
        {
            foreach (InstrucaoASM a in listaTextASM)                
                if (a.Instrucao.ToUpper() == section.ToUpper())                    
                    return true;
            return false;
        }
        //==========================================================
        public List<string> GetCodigoStringASM()
        {
            List<string> listaResultado = new List<string>();
            foreach (InstrucaoASM itemASM in listaDataASM)
            {
                string aux = "";
                if (itemASM.Tipo == eTipo.Variavel && itemASM.Tamanho > 1)                
                    for (int x = 1; x < itemASM.Tamanho; x++)                    
                        aux += ",0";
                    
                
                listaResultado.Add(itemASM.GetInstrucaoASM() + aux);
            }
           foreach (InstrucaoASM itemASM in listaTextASM)            
                listaResultado.Add(itemASM.GetInstrucaoASM());
            
            this.listaStringASM = listaResultado;

            return listaResultado;
        }
        //==========================================================
        public List<InstrucaoASM> GetCodigoInstrucaoASM()
        {
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();
         foreach (InstrucaoASM itemASM in listaDataASM)            
                    listaResultado.Add(itemASM);
        foreach (InstrucaoASM itemASM in listaTextASM)            
                    listaResultado.Add(itemASM);

            this.listaInstrucaoASM = listaResultado;
            return listaResultado;
        }
        public List<InstrucaoASM> GetMemoriaDados()
        {
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();
            

            foreach (InstrucaoASM itemASM in listaDataASM)            
                if (itemASM.Tipo == eTipo.Variavel)
                   listaResultado.Add(itemASM);
            
            foreach (InstrucaoASM itemASM in listaTextASM)            
                if (itemASM.Tipo == eTipo.Variavel)
                    listaResultado.Add(itemASM);
            

            return listaResultado;
        }

        //==========================================================
        public void SetEnderecoRotulosVariavel()
        {
            List<InstrucaoASM> listaRotulos = this.GetListaRotulos();
            List<InstrucaoASM> listaVariaveis = this.GetMemoriaDados();
                       
            foreach (InstrucaoASM itemASM in listaDataASM)                
                if (itemASM.Tipo == eTipo.Instrucao)                    
                    if (itemASM.IsDesvio)
                    {
                        try
                        {
                            InstrucaoASM rotulo = listaRotulos.Where(w => w.Instrucao == itemASM.Operando).First();
                            if (rotulo != null)
                                itemASM.EndRotuloVariavel = rotulo.IndexMemoria;
                        }
                        catch { }
                    }else
                        if ((itemASM.Instrucao == "ADD") || (itemASM.Instrucao == "SUB") || (itemASM.Instrucao == "LD") || (itemASM.Instrucao == "STO"))
                        {
                            try
                            {
                                InstrucaoASM variavel = listaVariaveis.Where(w => w.Instrucao == itemASM.Operando).First();
                                if (variavel != null)
                                    itemASM.EndRotuloVariavel = variavel.IndexMemoria;
                            }
                            catch { }

                        }
                    
                            
        }
        //==========================================================
        public List<InstrucaoASM> GetMemoriaInstrucao()
        {
            this.SetEnderecoRotulosVariavel();
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();
            
            foreach (InstrucaoASM itemASM in listaTextASM)                
                if (itemASM.Tipo == eTipo.Instrucao)                    
                    listaResultado.Add(itemASM);
                    
               
            
            return listaResultado;
        }
        //==========================================================
        public List<InstrucaoASM> GetListaRotulos()
        {
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();
            
            foreach (InstrucaoASM itemASM in listaTextASM)            
                if (itemASM.Tipo == eTipo.Rotulo)
                    listaResultado.Add(itemASM);
            
            
            return listaResultado;
        }
        //==========================================================
        public InstrucaoASM GetRotulo(string strRotulo)
        {            
            foreach (InstrucaoASM itemASM in listaTextASM)                
                if ((itemASM.Tipo == eTipo.Rotulo) && (itemASM.Instrucao == strRotulo))
                    return itemASM;
                
            
            return null;
        }
        //==========================================================
        public InstrucaoASM GetInstrucaoAsmByEnderecoMemoria(int intEnd)
        {
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();
            
            foreach (InstrucaoASM itemASM in listaTextASM)                
                if ((itemASM.Tipo == eTipo.Instrucao) && (itemASM.IndexMemoria == intEnd))
                    return itemASM;
                
            
            return null;
        }
        #endregion
    }
}
