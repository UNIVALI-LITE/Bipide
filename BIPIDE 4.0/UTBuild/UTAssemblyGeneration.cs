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
    public class UTAssemblyGeneration
    {
        private static Portugol _Portugol;
        private static TestContext _TestStore;

        [AssemblyInitialize]
        public static void InicializaTeste(TestContext testStore)
        {
            _TestStore = testStore;
            try
            {
                ServicoIntegracao ServicoIntegracaoPortugol = ServicoIntegracao.getInstance();
                _Portugol = ServicoIntegracaoPortugol.iniciar();
            }
            catch (Exception ex)
            {
                Assert.Fail("Erro ao inicializar CORBA: " + ex.Message);
            }
            

        }
        public String executa(String pSourceCode)
        {

            Codigo iAssembly = new Codigo();
            try
            {
                Programa iProgram = _Portugol.compilar(pSourceCode,"Portugol");

                Tradutor reg = new Tradutor(iProgram, "Portugol");
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
        public void UTFSe()
        {
            var iReturn = executa("programa{ funcao inicio(){ inteiro x x=1 se(2==2){ x=30 } } }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEFIMSE1LDI30STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }

        [TestMethod]
        public void UTFSeSenao()
        {
            var iReturn = executa("programa  { funcao inicio() { inteiro x x = 1 se (2 == 2) { x = 30 } senao { x = 60 } }  }");
            Assert.AreEqual(".datax0.text_INICIOLDI1STOxLDI2SUBI2BNEELSE1LDI30STOxJMPFIMSE1ELSE1LDI60STOxFIMSE1HLT0".ToUpper(), iReturn.ToUpper());

        }
        [TestMethod]
        public void UTFPara()
        {
            try
            {
                var iReturn = executa("programa  {  funcao inicio()  {  inteiro fat,temp, i, j, num  num = 5  fat = 1  temp = 0  i = 0  j = 0  para (i = 2; i < num; i++){  temp = fat  para (j = 1; j< i-1; j++) {  fat = fat + temp  }  }  }  }  ");
                Assert.AreEqual(".DATAFAT0TEMP0I0J0NUM0.TEXT_INICIOLDI5STONUMLDI1STOFATLDI0STOTEMPLDI0STOILDI0STOJLDI2STOIPARA1SUBNUMBGEFIMPARA1LDFATSTOTEMPLDI1STOJPARA2SUBIADDI1BGEFIMPARA2LDFATADDTEMPSTOFATLDJADDI1STOJJMPPARA2FIMPARA2LDIADDI1STOIJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn.ToUpper());

                var iReturn2 = executa("programa { funcao inicio() { inteiro x, y y = 0 para (x = 1; x < 5; x++){ y = 1 + x } } } ");
                Assert.AreEqual(".datax0y0.text_INICIOLDI0STOyLDI1STOxPARA1SUBI5BGEFIMPARA1LDI1ADDxSTOyLDxADDI1STOxJMPPARA1FIMPARA1HLT0".ToUpper(), iReturn2.ToUpper());

            
            }catch(Exception e){
                Debug.WriteLine("UTFPara"+e.Message);
                Assert.Fail("UTFPara" + e.Message);
            }
        }
        [TestMethod]
        public void UTF()
        {
            try{
                //var iReturn = executa("");
                //Assert.AreEqual("".ToUpper(), iReturn.ToUpper());
             }catch(Exception e){
                Debug.WriteLine("UTF"+e.Message);
                Assert.Fail("UTF" + e.Message);
            }
        }

        [AssemblyCleanup]
        public static void Cleanup()
        {
            _Portugol.encerrar();
        }
    }
}
