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
        public void AddInstrucaoASM(string strInst, string strValue, BIPIDE.Classes.eTipo tipo, int? linha, int tam, int indice)
        {
            if (tipo == BIPIDE.Classes.eTipo.Variavel)
                this.listaDataASM.Add(new InstrucaoASM { Instrucao = strInst, Operando = strValue, Tipo = tipo, NrLinha = linha, Tamanho = tam, Indice = indice });                           
            
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
        public String GetCodigoStringASM()
        {
            List<string> listaResultado = new List<string>();
            String retorna = "";


           
            //foreach (InstrucaoASM itemASM in listaDataASM)
            for (int i = 0; i < listaDataASM.Count(); i++)
            {
                if (listaDataASM[i].Tipo == eTipo.Variavel && listaDataASM[i].Tamanho > 1)
                {
                    String aux = "";
                    for (int j = 0; j < listaDataASM[i].Tamanho - 1; j++)
                    {
                        aux += listaDataASM[i + j].Operando + ", ";
                    }
                    aux += listaDataASM[i + listaDataASM[i].Tamanho - 1].Operando;
                    listaResultado.Add("\t" + listaDataASM[i].Instrucao.ToLower() + " : " + aux);


                    i = i + listaDataASM[i].Tamanho -1;
                }else
                //    continue;

                listaResultado.Add(listaDataASM[i].GetInstrucaoASM());
            }

            foreach (InstrucaoASM itemASM in listaTextASM)
                listaResultado.Add(itemASM.GetInstrucaoASM());

            this.listaStringASM = listaResultado;

            foreach (String i in listaResultado)
                retorna += i + Environment.NewLine;

            return retorna;
        }

        //==========================================================
        public List<InstrucaoASM> GetCodigoInstrucaoASM()
        {
            return PutCodigoTogether();
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

        public List<InstrucaoASM> PutCodigoTogether()
        {
            //reordena indice de arquivo sequencialmente
            int indexArquivo = 1;
            List<InstrucaoASM> listaResultado = new List<InstrucaoASM>();

            foreach (InstrucaoASM itemASM in listaDataASM)
            {
                itemASM.IndexArquivo = (itemASM.Tamanho > 0)? indexArquivo : indexArquivo++;
                listaResultado.Add(itemASM);
            }
            foreach (InstrucaoASM itemASM in listaTextASM)
            {
                itemASM.IndexArquivo = indexArquivo++;
                listaResultado.Add(itemASM);
            }

            this.listaInstrucaoASM = listaResultado;
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

        public void InjectInstruction(InstrucaoASM instr, int index)
        {
            Codigo codigo = new Codigo();
            codigo.AddInstrucaoASM(instr.Instrucao, instr.Operando, instr.Tipo, instr.NrLinha);
            InjectInstruction(codigo, index);
        }
        public void InjectInstruction(Codigo _Object_Interrupt, int index)
        {
            List<InstrucaoASM> iListaTemp = new List<InstrucaoASM>();

            //percorrer lista de instruções, encontrar instrução para quebra
            foreach (InstrucaoASM iInstrucoes in this.listaTextASM)
            {
                if (iInstrucoes.IndexArquivo >= index)
                {
                    //armazenar instruções em lista temporária
                    iListaTemp.Add(iInstrucoes);
                }
            }
            //remover instruções depois do indice
            foreach (InstrucaoASM iInstrRemover in iListaTemp)
            {
                listaTextASM.Remove(iInstrRemover);
            }
            //adicionar instruções do interrupt
            foreach (InstrucaoASM iInterrupt in _Object_Interrupt.listaTextASM)
            {
                listaTextASM.Add(iInterrupt);
            }
            //adicionar instruções de temp
            foreach (InstrucaoASM iTemp in iListaTemp)
            {
                listaTextASM.Add(iTemp);
            }

            int indice = -1;
            //refazer índice memória
            foreach (InstrucaoASM iList in listaTextASM)
            {
                switch (iList.Tipo)
                {
                    case eTipo.Instrucao:
                        iList.IndexMemoria = ++indice;
                        break;
                    case eTipo.Rotulo:
                        iList.IndexMemoria = 0;
                        break;
                }
            }
            //refazer índice rótulos
            foreach (InstrucaoASM iList in listaTextASM)
            {
                if (iList.Tipo == eTipo.Rotulo)
                {
                    iList.IndexMemoria = listaTextASM[listaTextASM.IndexOf(iList)+1].IndexMemoria;
                }
            }
            //refazer índice arquivo
            PutCodigoTogether();
        }


        public void RedoProgramIndex()
        {
            int indice = -1;
            //refazer índice memória
            foreach (InstrucaoASM iList in listaTextASM)
            {
                switch (iList.Tipo)
                {
                    case eTipo.Instrucao:
                        iList.IndexMemoria = ++indice;
                        break;
                    case eTipo.Rotulo:
                        iList.IndexMemoria = 0;
                        break;
                }
            }
            //refazer índice rótulos
            foreach (InstrucaoASM iList in listaTextASM)
            {
                if (iList.Tipo == eTipo.Rotulo)
                {
                    iList.IndexMemoria = listaTextASM[listaTextASM.IndexOf(iList) + 1].IndexMemoria;
                }
            }
        }
    }
}
