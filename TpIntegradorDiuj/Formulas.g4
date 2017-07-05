grammar Formulas;

/*
 * Parser Rules
 */

compileUnit

formula	: expresion operador expresion EOF;
expresion : valor operador valor ;
valor : VALOR;
operador: OPERADOR;
	

/*
 * Lexer Rules
 */
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
WHITESPACE  : (' ' | '\t') ;
OPERADOR : (MAS | MENOS | PRODUCTO | COCIENTE) ;
VALOR : (LOWERCASE | UPPERCASE)+ ;
MAS : '+';
MENOS : '-';
PRODUCTO : '*';
COCIENTE : '/';