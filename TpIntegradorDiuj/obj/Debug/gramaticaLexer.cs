//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\uri_a\Desktop\UTN\2017\DDS\Integrador\TP Integrador .NET\TpIntegradorDiuj\TpIntegradorDiuj\gramatica.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace TpIntegradorDiuj {
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public partial class gramaticaLexer : Lexer {
	public const int
		INT=1, MAS=2, MENOS=3, POR=4, DIVIDIDO=5, PARENTESISIZQUIERDO=6, PARENTESISDERECHO=7, 
		INDICADOR=8, SEPARADORDECIMAL=9, WS=10;
	public static string[] modeNames = {
		"DEFAULT_MODE"
	};

	public static readonly string[] ruleNames = {
		"INT", "MAS", "MENOS", "POR", "DIVIDIDO", "PARENTESISIZQUIERDO", "PARENTESISDERECHO", 
		"INDICADOR", "SEPARADORDECIMAL", "WS"
	};


	public gramaticaLexer(ICharStream input)
		: base(input)
	{
		_interp = new LexerATNSimulator(this,_ATN);
	}

	private static readonly string[] _LiteralNames = {
		null, null, "'+'", "'-'", "'*'", "'/'", "'('", "')'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "INT", "MAS", "MENOS", "POR", "DIVIDIDO", "PARENTESISIZQUIERDO", 
		"PARENTESISDERECHO", "INDICADOR", "SEPARADORDECIMAL", "WS"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[System.Obsolete("Use Vocabulary instead.")]
	public static readonly string[] tokenNames = GenerateTokenNames(DefaultVocabulary, _SymbolicNames.Length);

	private static string[] GenerateTokenNames(IVocabulary vocabulary, int length) {
		string[] tokenNames = new string[length];
		for (int i = 0; i < tokenNames.Length; i++) {
			tokenNames[i] = vocabulary.GetLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = vocabulary.GetSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}

		return tokenNames;
	}

	[System.Obsolete("Use IRecognizer.Vocabulary instead.")]
	public override string[] TokenNames
	{
		get
		{
			return tokenNames;
		}
	}

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "gramatica.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string[] ModeNames { get { return modeNames; } }

	public override string SerializedAtn { get { return _serializedATN; } }

	public static readonly string _serializedATN =
		"\x3\xAF6F\x8320\x479D\xB75C\x4880\x1605\x191C\xAB37\x2\f\x33\b\x1\x4\x2"+
		"\t\x2\x4\x3\t\x3\x4\x4\t\x4\x4\x5\t\x5\x4\x6\t\x6\x4\a\t\a\x4\b\t\b\x4"+
		"\t\t\t\x4\n\t\n\x4\v\t\v\x3\x2\x6\x2\x19\n\x2\r\x2\xE\x2\x1A\x3\x3\x3"+
		"\x3\x3\x4\x3\x4\x3\x5\x3\x5\x3\x6\x3\x6\x3\a\x3\a\x3\b\x3\b\x3\t\x6\t"+
		"*\n\t\r\t\xE\t+\x3\n\x3\n\x3\v\x3\v\x3\v\x3\v\x2\x2\x2\f\x3\x2\x3\x5\x2"+
		"\x4\a\x2\x5\t\x2\x6\v\x2\a\r\x2\b\xF\x2\t\x11\x2\n\x13\x2\v\x15\x2\f\x3"+
		"\x2\x6\x3\x2\x32;\x4\x2\x43\\\x63|\x4\x2..\x30\x30\x5\x2\v\f\xF\xF\"\""+
		"\x34\x2\x3\x3\x2\x2\x2\x2\x5\x3\x2\x2\x2\x2\a\x3\x2\x2\x2\x2\t\x3\x2\x2"+
		"\x2\x2\v\x3\x2\x2\x2\x2\r\x3\x2\x2\x2\x2\xF\x3\x2\x2\x2\x2\x11\x3\x2\x2"+
		"\x2\x2\x13\x3\x2\x2\x2\x2\x15\x3\x2\x2\x2\x3\x18\x3\x2\x2\x2\x5\x1C\x3"+
		"\x2\x2\x2\a\x1E\x3\x2\x2\x2\t \x3\x2\x2\x2\v\"\x3\x2\x2\x2\r$\x3\x2\x2"+
		"\x2\xF&\x3\x2\x2\x2\x11)\x3\x2\x2\x2\x13-\x3\x2\x2\x2\x15/\x3\x2\x2\x2"+
		"\x17\x19\t\x2\x2\x2\x18\x17\x3\x2\x2\x2\x19\x1A\x3\x2\x2\x2\x1A\x18\x3"+
		"\x2\x2\x2\x1A\x1B\x3\x2\x2\x2\x1B\x4\x3\x2\x2\x2\x1C\x1D\a-\x2\x2\x1D"+
		"\x6\x3\x2\x2\x2\x1E\x1F\a/\x2\x2\x1F\b\x3\x2\x2\x2 !\a,\x2\x2!\n\x3\x2"+
		"\x2\x2\"#\a\x31\x2\x2#\f\x3\x2\x2\x2$%\a*\x2\x2%\xE\x3\x2\x2\x2&\'\a+"+
		"\x2\x2\'\x10\x3\x2\x2\x2(*\t\x3\x2\x2)(\x3\x2\x2\x2*+\x3\x2\x2\x2+)\x3"+
		"\x2\x2\x2+,\x3\x2\x2\x2,\x12\x3\x2\x2\x2-.\t\x4\x2\x2.\x14\x3\x2\x2\x2"+
		"/\x30\t\x5\x2\x2\x30\x31\x3\x2\x2\x2\x31\x32\b\v\x2\x2\x32\x16\x3\x2\x2"+
		"\x2\x5\x2\x1A+\x3\x2\x3\x2";
	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN.ToCharArray());
}
} // namespace TpIntegradorDiuj
