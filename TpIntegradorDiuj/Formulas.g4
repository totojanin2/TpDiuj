grammar Formulas;

/*
 * Parser Rules
 */

compileUnit
formula	: expresion operando expresion	EOF;
expresion : valor operando valor;
valor : VALOR
operando: OPERANDO;
	

/*
 * Lexer Rules
 */
WS	:	' ' -> channel(HIDDEN);
fragment LOWERCASE  : [a-z] ;
fragment UPPERCASE  : [A-Z] ;
OPERANDO : ('+' | '-');
VALOR : (LOWERCASE || UPPERCASE);


