using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BIPIDE_4._0.ControlResources
{
    [XmlInclude(typeof(LanguageMapping))]
    public class LanguageMapping
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Icon;

        public string Icon
        {
            get { return _Icon; }
            set { _Icon = value; }
        }

        private string _HelpMapping;

        public string HelpMapping
        {
            get { return _HelpMapping; }
            set { _HelpMapping = value; }
        }

        private string _InterfaceMapping;

        public string InterfaceMapping
        {
            get { return _InterfaceMapping; }
            set { _InterfaceMapping = value; }
        }

        private string _TipsMapping;

        public string TipsMapping
        {
            get { return _TipsMapping; }
            set { _TipsMapping = value; }
        }

        private string _NotSuportedErrors;

        public string NotSuportedErrors
        {
            get { return _NotSuportedErrors; }
            set { _NotSuportedErrors = value; }
        }

        private string _ErroSimboloNaoInicializado;

        public string ErroSimboloNaoInicializado
        {
            get { return _ErroSimboloNaoInicializado; }
            set { _ErroSimboloNaoInicializado = value; }
        }

        private string _ErroAoAtribuirEmVetor;

        public string ErroAoAtribuirEmVetor
        {
            get { return _ErroAoAtribuirEmVetor; }
            set { _ErroAoAtribuirEmVetor = value; }
        }

        private string _ErroAoInicializarVetor;

        public string ErroAoInicializarVetor
        {
            get { return _ErroAoInicializarVetor; }
            set { _ErroAoInicializarVetor = value; }
        }

        private string _ErroAtribuirEmChamadaFuncao;

        public string ErroAtribuirEmChamadaFuncao
        {
            get { return _ErroAtribuirEmChamadaFuncao; }
            set { _ErroAtribuirEmChamadaFuncao = value; }
        }

        private string _ErroAtribuirEmConstante;

        public string ErroAtribuirEmConstante
        {
            get { return _ErroAtribuirEmConstante; }
            set { _ErroAtribuirEmConstante = value; }
        }

        private string _ErroDefinirTipoDadoVetorLiteral;

        public string ErroDefinirTipoDadoVetorLiteral
        {
            get { return _ErroDefinirTipoDadoVetorLiteral; }
            set { _ErroDefinirTipoDadoVetorLiteral = value; }
        }

        private string _ErroInicializacaoConstante;

        public string ErroInicializacaoConstante
        {
            get { return _ErroInicializacaoConstante; }
            set { _ErroInicializacaoConstante = value; }
        }

        private string _ErroInicializacaoInvalida;

        public string ErroInicializacaoInvalida
        {
            get { return _ErroInicializacaoInvalida; }
            set { _ErroInicializacaoInvalida = value; }
        }

        private string _ErroNumeroParametrosFuncao;

        public string ErroNumeroParametrosFuncao
        {
            get { return _ErroNumeroParametrosFuncao; }
            set { _ErroNumeroParametrosFuncao = value; }
        }

        private string _ErroOperacaoComExpressaoConstante;

        public string ErroOperacaoComExpressaoConstante
        {
            get { return _ErroOperacaoComExpressaoConstante; }
            set { _ErroOperacaoComExpressaoConstante = value; }
        }

        private string _ErroOperandoEsquerdoAtribuicaoConstante;

        public string ErroOperandoEsquerdoAtribuicaoConstante
        {
            get { return _ErroOperandoEsquerdoAtribuicaoConstante; }
            set { _ErroOperandoEsquerdoAtribuicaoConstante = value; }
        }

        private string _ErroParaSemExpressaoComparacao;

        public string ErroParaSemExpressaoComparacao
        {
            get { return _ErroParaSemExpressaoComparacao; }
            set { _ErroParaSemExpressaoComparacao = value; }
        }

        private string _ErroParametroExcedente;

        public string ErroParametroExcedente
        {
            get { return _ErroParametroExcedente; }
            set { _ErroParametroExcedente = value; }
        }

        private string _ErroParametroRedeclarado;

        public string ErroParametroRedeclarado
        {
            get { return _ErroParametroRedeclarado; }
            set { _ErroParametroRedeclarado = value; }
        }

        private string _ErroPassagemParametroInvalida;

        public string ErroPassagemParametroInvalida
        {
            get { return _ErroPassagemParametroInvalida; }
            set { _ErroPassagemParametroInvalida = value; }
        }

        private string _ErroQuantidadeElementosInicializacaoVetor;

        public string ErroQuantidadeElementosInicializacaoVetor
        {
            get { return _ErroQuantidadeElementosInicializacaoVetor; }
            set { _ErroQuantidadeElementosInicializacaoVetor = value; }
        }

        private string _ErroQuantificadorParametroFuncao;

        public string ErroQuantificadorParametroFuncao
        {
            get { return _ErroQuantificadorParametroFuncao; }
            set { _ErroQuantificadorParametroFuncao = value; }
        }

        private string _ErroReferenciaInvalida;

        public string ErroReferenciaInvalida
        {
            get { return _ErroReferenciaInvalida; }
            set { _ErroReferenciaInvalida = value; }
        }

        private string _ErroSimboloNaoDeclarado;

        public string ErroSimboloNaoDeclarado
        {
            get { return _ErroSimboloNaoDeclarado; }
            set { _ErroSimboloNaoDeclarado = value; }
        }

        private string _ErroSimboloRedeclarado;

        public string ErroSimboloRedeclarado
        {
            get { return _ErroSimboloRedeclarado; }
            set { _ErroSimboloRedeclarado = value; }
        }

        private string _ErroTamanhoVetorMatriz;

        public string ErroTamanhoVetorMatriz
        {
            get { return _ErroTamanhoVetorMatriz; }
            set { _ErroTamanhoVetorMatriz = value; }
        }

        private string _ErroTipoParametroIncompativel;

        public string ErroTipoParametroIncompativel
        {
            get { return _ErroTipoParametroIncompativel; }
            set { _ErroTipoParametroIncompativel = value; }
        }

        private string _ErroTiposIncompativeis;

        public string ErroTiposIncompativeis
        {
            get { return _ErroTiposIncompativeis; }
            set { _ErroTiposIncompativeis = value; }
        }

        private string _ErroVaParaSemTitulo;

        public string ErroVaParaSemTitulo
        {
            get { return _ErroVaParaSemTitulo; }
            set { _ErroVaParaSemTitulo = value; }
        }

        private string _ErroVetorSemElementos;

        public string ErroVetorSemElementos
        {
            get { return _ErroVetorSemElementos; }
            set { _ErroVetorSemElementos = value; }
        }

        private string _ErroComandoEsperado;

        public string ErroComandoEsperado
        {
            get { return _ErroComandoEsperado; }
            set { _ErroComandoEsperado = value; }
        }

        private string _ErroEscopo;

        public string ErroEscopo
        {
            get { return _ErroEscopo; }
            set { _ErroEscopo = value; }
        }

        private string _ErroExpressaoEsperada;

        public string ErroExpressaoEsperada
        {
            get { return _ErroExpressaoEsperada; }
            set { _ErroExpressaoEsperada = value; }
        }

        private string _ErroExpressaoIncompleta;

        public string ErroExpressaoIncompleta
        {
            get { return _ErroExpressaoIncompleta; }
            set { _ErroExpressaoIncompleta = value; }
        }

        private string _ErroExpressaoInesperada;

        public string ErroExpressaoInesperada
        {
            get { return _ErroExpressaoInesperada; }
            set { _ErroExpressaoInesperada = value; }
        }

        private string _ErroExpressoesForaEscopoPrograma;

        public string ErroExpressoesForaEscopoPrograma
        {
            get { return _ErroExpressoesForaEscopoPrograma; }
            set { _ErroExpressoesForaEscopoPrograma = value; }
        }

        private string _ErroFaltaDoisPontos;

        public string ErroFaltaDoisPontos
        {
            get { return _ErroFaltaDoisPontos; }
            set { _ErroFaltaDoisPontos = value; }
        }

        private string _ErroNomeIncompativel;

        public string ErroNomeIncompativel
        {
            get { return _ErroNomeIncompativel; }
            set { _ErroNomeIncompativel = value; }
        }

        private string _ErroNomeSimboloEstaFaltando;

        public string ErroNomeSimboloEstaFaltando
        {
            get { return _ErroNomeSimboloEstaFaltando; }
            set { _ErroNomeSimboloEstaFaltando = value; }
        }

        private string _ErroPalavraReservadaEstaFaltando;

        public string ErroPalavraReservadaEstaFaltando
        {
            get { return _ErroPalavraReservadaEstaFaltando; }
            set { _ErroPalavraReservadaEstaFaltando = value; }
        }

        private string _ErroParaEsperaCondicao;

        public string ErroParaEsperaCondicao
        {
            get { return _ErroParaEsperaCondicao; }
            set { _ErroParaEsperaCondicao = value; }
        }

        private string _ErroParentesis;

        public string ErroParentesis
        {
            get { return _ErroParentesis; }
            set { _ErroParentesis = value; }
        }

        private string _ErroTipoDeDadoEstaFaltando;

        public string ErroTipoDeDadoEstaFaltando
        {
            get { return _ErroTipoDeDadoEstaFaltando; }
            set { _ErroTipoDeDadoEstaFaltando = value; }
        }

        private string _ErroTokenFaltando;

        public string ErroTokenFaltando
        {
            get { return _ErroTokenFaltando; }
            set { _ErroTokenFaltando = value; }
        }

        private string _Success;

        public string Success
        {
            get { return _Success; }
            set { _Success = value; }
        }
    }
}
