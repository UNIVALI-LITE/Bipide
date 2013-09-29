/**
 * SyntaxHighlighter
 * http://alexgorbatchev.com/SyntaxHighlighter
 *
 * SyntaxHighlighter is donationware. If you are using it, please donate.
 * http://alexgorbatchev.com/SyntaxHighlighter/donate.html
 *
 * @version
 * 3.0.83 (July 02 2010)
 * 
 * @copyright
 * Copyright (C) 2004-2010 Alex Gorbatchev.
 *
 * @license
 * Dual licensed under the MIT and GPL licenses.
 */
;(function()
{
	// CommonPortugol
	typeof(require) != 'undefined' ? SyntaxHighlighter = require('shCore').SyntaxHighlighter : null;

	function Brush()
	{

	    var keywords = 'programa real vazio logico cadeia inteiro caracter ' +
                       'escolha caso contrario const funcao retorne para ' +
                       'pare faca enquanto se senao inclua biblioteca ';

	    var functions = 'leia escreva';

		function fixComments(match, regexInfo)
		{
			var css = (match[0].indexOf("///") == 0)
				? 'color1'
				: 'comments'
				;
			
			return [new SyntaxHighlighter.Match(match[0], match.index, css)];
		}

		this.regexList = [
			{ regex: SyntaxHighlighter.regexLib.singleLineCComments,	func : fixComments },		// one line comments
			{ regex: SyntaxHighlighter.regexLib.multiLineCComments,		css: 'comments'    },		// multiline comments
			{ regex: /\b([\d]+(\.[\d]+)?|0x[a-f0-9]+)\b/gi,				css: 'value'       },		// numbers
			{ regex: /@"(?:[^"]|"")*"/g,								css: 'string'      },		// @-quoted strings
			{ regex: SyntaxHighlighter.regexLib.doubleQuotedString,		css: 'string'      },		// strings
			{ regex: SyntaxHighlighter.regexLib.singleQuotedString,		css: 'string'      },		// strings
			{ regex: new RegExp(this.getKeywords(keywords), 'gm'),      css: 'keyword'     },		// portugol keyword
            { regex: new RegExp(this.getKeywords(functions), 'gm'),     css: 'color1'      },		// portugol functions
			];
		
		this.forHtmlScript(SyntaxHighlighter.regexLib.aspScriptTags);
	};

	Brush.prototype	= new SyntaxHighlighter.Highlighter();
	Brush.aliases	= ['portugol', 'Portugol'];

	SyntaxHighlighter.brushes.CSharp = Brush;

	// CommonJS
	typeof(exports) != 'undefined' ? exports.Brush = Brush : null;
})();

