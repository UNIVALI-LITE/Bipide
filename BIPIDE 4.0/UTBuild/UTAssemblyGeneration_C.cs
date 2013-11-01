using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.univali.portugol.integracao.csharp;
using br.univali.portugol.integracao;
using br.univali.portugol.integracao.analise;
using BIPIDE.Classes;
using BIPIDE_4._0;
using br.univali.portugol.integracao.mensagens;
using System.Diagnostics;

namespace UTBuild
{
    [TestClass]
    class UTAssemblyGeneration_C
    {
        private static Portugol _Portugol;
        private static TestContext _TestStore;


        public String executaC(String pSourceCode)
        {

            Codigo iAssembly = new Codigo();
            try
            {
                Programa iProgram = _Portugol.compilar(pSourceCode, "C");

                Tradutor reg = new Tradutor(iProgram, "C");
                iAssembly = reg.Convert(iProgram);
                return AssemblyToString(iAssembly);

            }
            catch (ErroCompilacao ec)
            {
                ResultadoAnalise resultado = ec.resultadoAnalise;

                if (resultado.getNumeroTotalErros() > 0)
                {
                    foreach (ErroSintatico erro in resultado.getErrosSintaticos())
                    {
                        Assert.Fail("Erro Sintatico na linha " + erro.linha + " e coluna " + erro.coluna + ": " + erro.mensagem);
                    }
                    foreach (ErroSemantico erro in resultado.getErrosSemanticos())
                    {
                        Assert.Fail("Erro Semantico na linha " + erro.linha + " e coluna " + erro.coluna + ": " + erro.mensagem);
                    }
                }
            }
            return String.Empty;
        }

        private string AssemblyToString(Codigo iAssembly)
        {
            String iSource = "";

            foreach (InstrucaoASM ins in iAssembly.GetCodigoInstrucaoASM())
            {
                iSource += ins.Instrucao + ins.Operando;
            }
            return iSource;
        }

        [TestMethod]
        public void UTFCSe()
        {
            var iReturn = executaC("int main()  { int x = 1;  if (2 == 2) {  x = 30;  }  }  ");
            Assert.AreEqual(".datax1.text_MAINLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }

        [TestMethod]
        public void UTFCSeSenao()
        {
            var iReturn = executaC("int main() { int x = 1; if (2 == 2) { x = 30; } else { x = 60; } } ");
            Assert.AreEqual(".datax1.text_MAINLDI2SUBI2BNEELSE1LDI30STOxJMPFIMSE1ELSE1LDI60STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }
        [TestMethod]
        public void UTFCPara()
        {
            try
            {
                var iReturn = executaC("int main()  {  int fat = 1,temp = 0, i = 0, j = 0, num = 5;  for (i = 2; i <= num; i++){  temp = fat;  for (j = 1; j<= i-1; j++)  fat = fat + temp;  }  }  ");
                Assert.AreEqual(".datafat1temp0i0j0num5.text_MAINLDI2STOiPARA1LDiSUBnumBGTFIMPARA1LDfatSTOtempLDI1STOjPARA2LDjSUBiADDI1BGTFIMPARA2LDfatADDtempSTOfatLDjADDI1STOjJMPPARA2FIMPARA2LDiADDI1STOiJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn.ToUpper());

            }
            catch (Exception e)
            {
                Debug.WriteLine("UTFPara" + e.Message);
                Assert.Fail("UTFPara" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCPara2()
        {
            try
            {
                var iReturn = executaC("int main() { int x, y = 0; for (x = 1; x <= 5; x++) y = 1 + x; } ");
                Assert.AreEqual(".datax0y0.text_MAINLDI1STOxPARA1LDxSUBI5BGTFIMPARA1LDI1ADDxSTOyLDxADDI1STOxJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCBitwise()
        {
            try
            {
                var iReturn = executaC("int main() {    int a=2, b=3, c=0;  c = a+b << 1 & 4 ~ 1;  escreva(c);    }  ");
                Assert.AreEqual(".dataa2b3c0t_expr10.text_MAINLDaADDbSLL1ANDI4STOt_expr1LDI1NOT0ORt_expr1STOcSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCBitwise2()
        {
            try
            {
                var iReturn = executaC("int main() {    int a=2, b=3, c=0;    c = (a+b) << 1;    escreva(c); }");
                Assert.AreEqual(".dataa2b3c0.text_MAINLDaADDbSLL1STOcSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCSubrotina()
        {
            try
            {
                var iReturn = executaC(" int multiplica(int a, int c){ int i, result =0; for (i = 1; i <= c; i++) result = result+a; return result; } int main() { int j = 2,k =3; k = multiplica(k,j); escreva(k); } ");
                Assert.AreEqual(".datamultiplica_a0multiplica_c0multiplica_i0multiplica_result0j2k3.textJMP_MAIN_MULTIPLICALDI1STOmultiplica_iPARA1LDmultiplica_iSUBmultiplica_cBGTFIMPARA1LDmultiplica_resultADDmultiplica_aSTOmultiplica_resultLDmultiplica_iADDI1STOmultiplica_iJMPPARA1FIMPARA1LDmultiplica_resultRETURN0_MAINLDkSTOmultiplica_aLDjSTOmultiplica_cCALL_MULTIPLICASTOkSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCSubrotina2()
        {
            try
            {
                var iReturn = executaC("int multiplica(int a, int c){  int i, result =0;   for (i = 1; i <= c; i++)  result = result+a ;    return result ;  }   int quadrado(int n){  return multiplica(n,n);  }   int main()  {  int j = 2,k = 3;  k = quadrado(j+1);  escreva(k);  }  ");
                Assert.AreEqual(".datamultiplica_a0multiplica_c0multiplica_i0multiplica_result0quadrado_n0j2k3.textJMP_MAIN_MULTIPLICALDI1STOmultiplica_iPARA1LDmultiplica_iSUBmultiplica_cBGTFIMPARA1LDmultiplica_resultADDmultiplica_aSTOmultiplica_resultLDmultiplica_iADDI1STOmultiplica_iJMPPARA1FIMPARA1LDmultiplica_resultRETURN0_QUADRADOLDquadrado_nSTOmultiplica_aLDquadrado_nSTOmultiplica_cCALL_MULTIPLICARETURN0_MAINLDjADDI1STOquadrado_nCALL_QUADRADOSTOkSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCEnquanto()
        {
            try
            {
                var iReturn = executaC(" int main() { int atual = 1, anterior = 0, proximo, saida[10], i =1; while (i < 10) { saida[i] = atual; i = i+1; proximo = anterior + atual; anterior = atual; atual = proximo; } } ");
                Assert.AreEqual(".DATAATUAL1ANTERIOR0PROXIMO0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0SAIDA0I1.TEXT_MAININI_ENQ1LDISUBI10BGEFIM_ENQ1LDISTO$INDRLDATUALSTOVSAIDALDIADDI1STOILDANTERIORADDATUALSTOPROXIMOLDATUALSTOANTERIORLDPROXIMOSTOATUALJMPINI_ENQ1FIM_ENQ1HLT0".ToUpper(), iReturn.ToUpper());
                //vetor no formato NOME0NOME0 ao invés de NOME 0,...
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCParaSe()
        {
            try
            {
                var iReturn = executaC("int main()  {  int vetor[5] = {5,3,4,2,1};  int i, j, aux;   for (i = 0; i<= 4; i++){  for (j = i+1; j <= 4; j++){  if (vetor[i] > vetor[j]){  aux = vetor[i];  vetor[i] = vetor[j];  vetor[j] = aux;  }  }  }  }  ");
                Assert.AreEqual(".DATAVETOR5VETOR3VETOR4VETOR2VETOR1I0J0AUX0T_VETOR10T_VETOR20T_VETOR30T_VETOR40.TEXT_MAINLDI0STOIPARA1LDISUBI4BGTFIMPARA1PARA2LDIADDI1STOJSUBI4BGTFIMPARA2LDISTO$INDRLDVVETORSTOT_VETOR1LDJSTO$INDRLDVVETORSTOT_VETOR2LDT_VETOR1SUBT_VETOR2BLEFIMSE1LDISTO$INDRLDVVETORSTOT_VETOR3STOAUXLDJSTO$INDRLDVVETORSTOT_VETOR4LDISTO$INDRLDVT_VETOR4STOVVETORLDJSTO$INDRLDAUXSTOVVETORFIMSE1LDJADDI1STOJJMPPARA2FIMPARA2LDIADDI1STOIJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn.ToUpper());
                //vetor no formato NOME0NOME0 ao invés de NOME 0,...
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCLeiaEscreva()
        {
            try
            {
                var iReturn = executaC("programa { funcao inicio() { inteiro a, b, soma, sub, mult, div leia(a,b) soma = a+b sub = a-b escreva(soma) escreva(sub) } } ");
                Assert.AreEqual(".dataa0b0soma0sub0mult0div0.text_MAINLD$in_portSTOaLD$in_portSTObLDaADDbSTOsomaLDaSUBbSTOsubLDsomaSTO$out_portLDsubSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCSeSenaoEncadeado()
        {
            try
            {
                var iReturn = executaC("programa { funcao inicio(){ inteiro a,b,c leia (a,b,c) se (a==b) escreva (1) senao { se (a==b) escreva (2) senao escreva (3) } } } ");
                Assert.AreEqual(".dataa0b0c0.text_MAINLD$in_portSTOaLD$in_portSTObLD$in_portSTOcLDaSUBbBNEELSE1LDI1STO$out_portJMPFIMSE1ELSE1LDaSUBbBNEELSE2LDI2STO$out_portJMPFIMSE2ELSE2LDI3STO$out_portFIMSE2FIMSE1HLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCEscolhaCaso()
        {
            try
            {
                var iReturn = executaC("int main(){ int opc=0; scanf(opc); switch (opc) { case 1 printf (1); break; case 2 printf (2); break; case 3 printf (3); break; default printf (4); } } ");
                Assert.AreEqual(".dataopc0.text_MAINLD$in_portSTOopcSTOt_escolha1SUBI1BNEPROXCASO1LDI1STO$out_portJMPFIMESCOLHA1PROXCASO1LDt_escolha1SUBI2BNEPROXCASO2LDI2STO$out_portJMPFIMESCOLHA1PROXCASO2LDt_escolha1SUBI3BNECASOCONTRARIO3LDI3STO$out_portJMPFIMESCOLHA1CASOCONTRARIO3LDI4STO$out_portFIMESCOLHA1HLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCEnquantoIncremento()
        {
            try
            {
                var iReturn = executaC("int main() { int contador = 10; while (contador > 0) { printf (contador); contador--; } printf (1000); } ");
                Assert.AreEqual(".datacontador10.text_MAININI_ENQ1LDcontadorSUBI0BLEFIM_ENQ1LDcontadorSTO$out_portLDcontadorSUBI1STOcontadorJMPINI_ENQ1FIM_ENQ1LDI1000STO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCFacaEnquanto()
        {
            try
            {
                var iReturn = executaC("int main()  {  int idade=0;  do {  scanf (idade);  } while (idade > 150);  }  ");
                Assert.AreEqual(".dataidade0.text_MAININI_ENQ1LD$in_portSTOidadeSUBI150BGTINI_ENQ1FIM_ENQ1HLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCParaEscrevaIncremento()
        {
            try
            {
                var iReturn = executaC("int main() { int vet[10], i; for(i = 0; i < 10; i++) vet [i] = i ; for(i = 9; i >=0; i--) printf (vet [i]); } ");
                Assert.AreEqual(".datavet0vet0vet0vet0vet0vet0vet0vet0vet0vet0i0.text_MAINLDI0STOiPARA1LDiSUBI10BGEFIMPARA1LDiSTO$indrLDiSTOVvetLDiADDI1STOiJMPPARA1FIMPARA1LDI9STOiPARA2LDiSUBI0BLTFIMPARA2LDiSTO$indrLDvetSTO$out_portLDiSUBI1STOiJMPPARA2FIMPARA2HLT0".ToUpper(), iReturn.ToUpper());
                //VETOR
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCEscrevaSubrotina()
        {
            try
            {
                var iReturn = executaC("void mensagem (int in){ int i; for(i = 0; i < in; i++){ printf (0); } for(i = 0; i < in; i++){ printf (1); } } int calcula (int a, int b){ int resp; resp = a - a + b - b; return (resp); } int main() { printf (calcula (3, 4)); } ");
                Assert.AreEqual(".datamensagem_in0mensagem_i0calcula_a0calcula_b0calcula_resp0.textJMP_MAIN_MENSAGEMLDI0STOmensagem_iPARA1LDmensagem_iSUBmensagem_inBGEFIMPARA1LDI0STO$out_portLDmensagem_iADDI1STOmensagem_iJMPPARA1FIMPARA1LDI0STOmensagem_iPARA2LDmensagem_iSUBmensagem_inBGEFIMPARA2LDI1STO$out_portLDmensagem_iADDI1STOmensagem_iJMPPARA2FIMPARA2RETURN0_CALCULALDcalcula_aSUBcalcula_aADDcalcula_bSUBcalcula_bSTOcalcula_respRETURN0_MAINLDI3STOcalcula_aLDI4STOcalcula_bCALL_CALCULASTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCConstParaSeSenao()
        {
            try
            {
                var iReturn = executaC("int main() { const int TAM = 5; int c; int vet[TAM]; for(c = 0; c < TAM; c++) { if (0 ==c) vet[c] = 1 ; else vet [c] = 0; printf(vet[c]); } } ");
                Assert.AreEqual(".datatam5c0vet0vet0vet0vet0vet0.text_MAINLDI0STOcPARA1LDcSUBTAMBGEFIMPARA1LDI0SUBcBNEELSE1LDcSTO$indrLDI1STOVvetJMPFIMSE1ELSE1LDcSTO$indrLDI0STOVvetFIMSE1LDcSTO$indrLDvetSTO$out_portLDcADDI1STOcJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn.ToUpper());
                //VETOR
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCEscrevaInteiroNotacaoHexaBin()
        {
            try
            {
                var iReturn = executaC("main() { printf(32); printf(0x20); printf(0b00100000); } ");
                Assert.AreEqual(".data.text_MAINLDI32STO$out_portLDI32STO$out_portLDI32STO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCVazioSubrotina()
        {
            try
            {
                var iReturn = executaC("main() { imprime(1); imprime(2); imprime(3); } void imprime(int var) { printf(var); } } ");
                Assert.AreEqual(".dataimprime_var0.text_MAINLDI1CALL_IMPRIMELDI2CALL_IMPRIMELDI3CALL_IMPRIMEHLT0_IMPRIMELDimprime_varSTO$out_portRETURN0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }

        [TestMethod]
        public void UTFCLeia()
        {
            try
            {
                var iReturn = executaC("main() { int numero =0; scanf(numero) ; } } ");
                Assert.AreEqual(".datanumero0.text_MAINLD$in_portSTOnumeroHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }

        [TestMethod]
        public void UTFCEscreva()
        {
            try
            {
                var iReturn = executaC("main() { printf(32); int a = 15; printf(a); } } ");
                Assert.AreEqual(".dataa15.text_MAINLDI32STO$out_portLDaSTO$out_portHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCAtribuicao()
        {
            try
            {
                var iReturn = executaC("main() { int a =0; a = 2; int b =0; scanf(b) ; int c ; c = b ; } } ");
                Assert.AreEqual(".dataa0b0c0.text_MAINLDI2STOaLD$in_portSTObSTOcHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCOperacoesLogicas()
        {
            try
            {
                var iReturn = executaC("main() { int a = 5, b = 3 ; if(a > b) printf(a); if(a == b) printf(a,b); if(a >= b) printf(a); } } ");
                Assert.AreEqual(".dataa5b3.text_MAINLDaSUBbBLEFIMSE1LDaSTO$out_portFIMSE1LDaSUBbBNEFIMSE2LDaSTO$out_portLDbSTO$out_portFIMSE2LDaSUBbBLTFIMSE3LDaSTO$out_portFIMSE3HLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCRetorneOperacao()
        {
            try
            {
                var iReturn = executaC("programa{   funcao inteiro soma(inteiro a, inteiro b){   retorne a+b }   funcao vazio exibir(inteiro a, inteiro b){   escreva(a+b)   }   funcao vazio mostrar(){   inteiro a, b   leia(a)   leia(b)   escreva(a+b) }   funcao inicio(){   escreva(soma(5,2))   exibir(5,2)   mostrar()   }  } ");
                Assert.AreEqual(".datasoma_a0soma_b0exibir_a0exibir_b0mostrar_a0mostrar_b0.textJMP_MAIN_SOMALDsoma_aADDsoma_bRETURN0_EXIBIRLDexibir_aADDexibir_bSTO$out_port_MOSTRARLD$in_portSTOmostrar_aLD$in_portSTOmostrar_bLDmostrar_aADDmostrar_bSTO$out_port_MAINLDI5STOsoma_aLDI2STOsoma_bCALL_SOMASTO$out_portLDI5STOexibir_aLDI2STOexibir_bCALL_EXIBIRCALL_MOSTRARHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }
        [TestMethod]
        public void UTFCVariavelGlobal()
        {
            try
            {
                var iReturn = executaC("programa{ inteiro variavel = 0 funcao inicio() {  inteiro outra_variavel = 1 } } ");
                Assert.AreEqual(".datavariavel0outra_variavel1.text_MAINHLT0".ToUpper(), iReturn.ToUpper());
            }
            catch (Exception e)
            {
                Debug.WriteLine("UTF" + e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }


    }
}
