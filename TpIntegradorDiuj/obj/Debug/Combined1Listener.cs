//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.6.4
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\uri_a\Desktop\UTN\2017\DDS\Integrador\TP Integrador .NET\TpIntegradorDiuj\TpIntegradorDiuj\ANTLR\Combined1.g4 by ANTLR 4.6.4

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace TpIntegradorDiuj.ANTLR {
using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="Combined1Parser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.6.4")]
[System.CLSCompliant(false)]
public interface ICombined1Listener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.indicador"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIndicador([NotNull] Combined1Parser.IndicadorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.indicador"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIndicador([NotNull] Combined1Parser.IndicadorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.producto"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterProducto([NotNull] Combined1Parser.ProductoContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.producto"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitProducto([NotNull] Combined1Parser.ProductoContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFactor([NotNull] Combined1Parser.FactorContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.factor"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFactor([NotNull] Combined1Parser.FactorContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.exponente"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExponente([NotNull] Combined1Parser.ExponenteContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.exponente"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExponente([NotNull] Combined1Parser.ExponenteContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="Combined1Parser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompileUnit([NotNull] Combined1Parser.CompileUnitContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="Combined1Parser.compileUnit"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompileUnit([NotNull] Combined1Parser.CompileUnitContext context);
}
} // namespace TpIntegradorDiuj.ANTLR
