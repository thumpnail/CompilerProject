// Generated from c:\Users\fried\OneDrive\Dokumente\Code\Rider Projects\CompilerProject\NanoScript-bflat\syntax\NanoScript.g4 by ANTLR 4.9.2
import org.antlr.v4.runtime.atn.*;
import org.antlr.v4.runtime.dfa.DFA;
import org.antlr.v4.runtime.*;
import org.antlr.v4.runtime.misc.*;
import org.antlr.v4.runtime.tree.*;
import java.util.List;
import java.util.Iterator;
import java.util.ArrayList;

@SuppressWarnings({"all", "warnings", "unchecked", "unused", "cast"})
public class NanoScriptParser extends Parser {
	static { RuntimeMetaData.checkVersion("4.9.2", RuntimeMetaData.VERSION); }

	protected static final DFA[] _decisionToDFA;
	protected static final PredictionContextCache _sharedContextCache =
		new PredictionContextCache();
	public static final int
		T__0=1, T__1=2, T__2=3, T__3=4, T__4=5, T__5=6, T__6=7, T__7=8, T__8=9, 
		T__9=10, T__10=11, T__11=12, T__12=13, T__13=14, T__14=15, T__15=16, T__16=17, 
		T__17=18, T__18=19, T__19=20, T__20=21, T__21=22, T__22=23, T__23=24, 
		T__24=25, T__25=26, T__26=27, T__27=28, T__28=29, T__29=30, T__30=31, 
		T__31=32, T__32=33, T__33=34, T__34=35, T__35=36, T__36=37, T__37=38, 
		T__38=39, T__39=40, T__40=41, T__41=42, T__42=43, T__43=44, T__44=45, 
		T__45=46, T__46=47, T__47=48, T__48=49, T__49=50, T__50=51, T__51=52, 
		T__52=53, T__53=54, T__54=55, T__55=56, T__56=57, T__57=58, T__58=59, 
		T__59=60, T__60=61, T__61=62, T__62=63, T__63=64, T__64=65, T__65=66, 
		T__66=67, T__67=68, T__68=69, T__69=70, T__70=71, T__71=72, T__72=73, 
		T__73=74, T__74=75, T__75=76, ANY=77, WORD=78, DIGIT=79;
	public static final int
		RULE_program = 0, RULE_module_statement = 1, RULE_imoport_statements = 2, 
		RULE_statement = 3, RULE_type_decl = 4, RULE_exp = 5, RULE_string = 6, 
		RULE_identifier = 7, RULE_number = 8, RULE_decimal = 9;
	private static String[] makeRuleNames() {
		return new String[] {
			"program", "module_statement", "imoport_statements", "statement", "type_decl", 
			"exp", "string", "identifier", "number", "decimal"
		};
	}
	public static final String[] ruleNames = makeRuleNames();

	private static String[] makeLiteralNames() {
		return new String[] {
			null, "'pub'", "'mod'", "'{'", "'}'", "'import'", "'as'", "'from'", "'let'", 
			"'var'", "'const'", "'.'", "'='", "'<<'", "'>>'", "'+='", "'-='", "'*='", 
			"'/='", "'if'", "'else'", "'switch'", "':'", "'break'", "'default'", 
			"'continue'", "'for'", "'in'", "';'", "'export'", "'fnc'", "'('", "','", 
			"')'", "'return'", "'enum'", "'error'", "'goto'", "'def'", "'type'", 
			"'struct'", "'class'", "'interface'", "'union'", "'assert'", "'::'", 
			"'#include'", "'<'", "'>'", "'['", "']'", "'{}'", "'true'", "'false'", 
			"'&'", "'+'", "'-'", "'/'", "'*'", "'||'", "'&&'", "'=='", "'!='", "'<='", 
			"'>='", "'..'", "'|'", "'^'", "'~'", "'!'", "'++'", "'--'", "'=>'", "'is'", 
			"'size'", "'str'", "'\"'"
		};
	}
	private static final String[] _LITERAL_NAMES = makeLiteralNames();
	private static String[] makeSymbolicNames() {
		return new String[] {
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, null, null, null, null, null, null, null, 
			null, null, null, null, null, "ANY", "WORD", "DIGIT"
		};
	}
	private static final String[] _SYMBOLIC_NAMES = makeSymbolicNames();
	public static final Vocabulary VOCABULARY = new VocabularyImpl(_LITERAL_NAMES, _SYMBOLIC_NAMES);

	/**
	 * @deprecated Use {@link #VOCABULARY} instead.
	 */
	@Deprecated
	public static final String[] tokenNames;
	static {
		tokenNames = new String[_SYMBOLIC_NAMES.length];
		for (int i = 0; i < tokenNames.length; i++) {
			tokenNames[i] = VOCABULARY.getLiteralName(i);
			if (tokenNames[i] == null) {
				tokenNames[i] = VOCABULARY.getSymbolicName(i);
			}

			if (tokenNames[i] == null) {
				tokenNames[i] = "<INVALID>";
			}
		}
	}

	@Override
	@Deprecated
	public String[] getTokenNames() {
		return tokenNames;
	}

	@Override

	public Vocabulary getVocabulary() {
		return VOCABULARY;
	}

	@Override
	public String getGrammarFileName() { return "NanoScript.g4"; }

	@Override
	public String[] getRuleNames() { return ruleNames; }

	@Override
	public String getSerializedATN() { return _serializedATN; }

	@Override
	public ATN getATN() { return _ATN; }

	public NanoScriptParser(TokenStream input) {
		super(input);
		_interp = new ParserATNSimulator(this,_ATN,_decisionToDFA,_sharedContextCache);
	}

	public static class ProgramContext extends ParserRuleContext {
		public List<Module_statementContext> module_statement() {
			return getRuleContexts(Module_statementContext.class);
		}
		public Module_statementContext module_statement(int i) {
			return getRuleContext(Module_statementContext.class,i);
		}
		public ProgramContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_program; }
	}

	public final ProgramContext program() throws RecognitionException {
		ProgramContext _localctx = new ProgramContext(_ctx, getState());
		enterRule(_localctx, 0, RULE_program);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(21); 
			_errHandler.sync(this);
			_la = _input.LA(1);
			do {
				{
				{
				setState(20);
				module_statement();
				}
				}
				setState(23); 
				_errHandler.sync(this);
				_la = _input.LA(1);
			} while ( _la==T__0 || _la==T__1 );
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Module_statementContext extends ParserRuleContext {
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public List<Imoport_statementsContext> imoport_statements() {
			return getRuleContexts(Imoport_statementsContext.class);
		}
		public Imoport_statementsContext imoport_statements(int i) {
			return getRuleContext(Imoport_statementsContext.class,i);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public Module_statementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_module_statement; }
	}

	public final Module_statementContext module_statement() throws RecognitionException {
		Module_statementContext _localctx = new Module_statementContext(_ctx, getState());
		enterRule(_localctx, 2, RULE_module_statement);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(26);
			_errHandler.sync(this);
			_la = _input.LA(1);
			if (_la==T__0) {
				{
				setState(25);
				match(T__0);
				}
			}

			setState(28);
			match(T__1);
			setState(29);
			identifier();
			setState(33);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__4) {
				{
				{
				setState(30);
				imoport_statements();
				}
				}
				setState(35);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			setState(50);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,5,_ctx) ) {
			case 1:
				{
				setState(36);
				match(T__2);
				setState(40);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(37);
					statement();
					}
					}
					setState(42);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(43);
				match(T__3);
				}
				break;
			case 2:
				{
				setState(47);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(44);
						statement();
						}
						} 
					}
					setState(49);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,4,_ctx);
				}
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Imoport_statementsContext extends ParserRuleContext {
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public IdentifierContext identifier() {
			return getRuleContext(IdentifierContext.class,0);
		}
		public Imoport_statementsContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_imoport_statements; }
	}

	public final Imoport_statementsContext imoport_statements() throws RecognitionException {
		Imoport_statementsContext _localctx = new Imoport_statementsContext(_ctx, getState());
		enterRule(_localctx, 4, RULE_imoport_statements);
		int _la;
		try {
			setState(63);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,7,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(52);
				match(T__4);
				setState(53);
				string();
				setState(56);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__5) {
					{
					setState(54);
					match(T__5);
					setState(55);
					identifier();
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(58);
				match(T__4);
				setState(59);
				identifier();
				setState(60);
				match(T__6);
				setState(61);
				string();
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class StatementContext extends ParserRuleContext {
		public List<IdentifierContext> identifier() {
			return getRuleContexts(IdentifierContext.class);
		}
		public IdentifierContext identifier(int i) {
			return getRuleContext(IdentifierContext.class,i);
		}
		public List<Type_declContext> type_decl() {
			return getRuleContexts(Type_declContext.class);
		}
		public Type_declContext type_decl(int i) {
			return getRuleContext(Type_declContext.class,i);
		}
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public StatementContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_statement; }
	}

	public final StatementContext statement() throws RecognitionException {
		StatementContext _localctx = new StatementContext(_ctx, getState());
		enterRule(_localctx, 6, RULE_statement);
		int _la;
		try {
			int _alt;
			setState(346);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,50,_ctx) ) {
			case 1:
				enterOuterAlt(_localctx, 1);
				{
				setState(66);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(65);
					match(T__0);
					}
				}

				setState(69);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) {
					{
					setState(68);
					_la = _input.LA(1);
					if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__7) | (1L << T__8) | (1L << T__9))) != 0)) ) {
					_errHandler.recoverInline(this);
					}
					else {
						if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
						_errHandler.reportMatch(this);
						consume();
					}
					}
				}

				setState(72);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__10) {
					{
					setState(71);
					match(T__10);
					}
				}

				setState(74);
				identifier();
				setState(76);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(75);
					type_decl();
					}
				}

				setState(80);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__11) {
					{
					setState(78);
					match(T__11);
					setState(79);
					exp(0);
					}
				}

				}
				break;
			case 2:
				enterOuterAlt(_localctx, 2);
				{
				setState(83);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__10) {
					{
					setState(82);
					match(T__10);
					}
				}

				setState(85);
				identifier();
				setState(87);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(86);
					type_decl();
					}
				}

				setState(103);
				_errHandler.sync(this);
				switch (_input.LA(1)) {
				case T__11:
					{
					setState(89);
					match(T__11);
					setState(90);
					exp(0);
					}
					break;
				case T__12:
					{
					setState(91);
					match(T__12);
					setState(92);
					exp(0);
					}
					break;
				case T__13:
					{
					setState(93);
					match(T__13);
					setState(94);
					exp(0);
					}
					break;
				case T__14:
					{
					setState(95);
					match(T__14);
					setState(96);
					exp(0);
					}
					break;
				case T__15:
					{
					setState(97);
					match(T__15);
					setState(98);
					exp(0);
					}
					break;
				case T__16:
					{
					setState(99);
					match(T__16);
					setState(100);
					exp(0);
					}
					break;
				case T__17:
					{
					setState(101);
					match(T__17);
					setState(102);
					exp(0);
					}
					break;
				case EOF:
				case T__0:
				case T__1:
				case T__3:
				case T__7:
				case T__8:
				case T__9:
				case T__10:
				case T__18:
				case T__20:
				case T__22:
				case T__23:
				case T__24:
				case T__25:
				case T__28:
				case T__29:
				case T__33:
				case T__34:
				case T__35:
				case T__36:
				case T__37:
				case T__38:
				case T__39:
				case T__40:
				case T__41:
				case T__42:
				case T__43:
				case T__44:
				case T__45:
				case WORD:
					break;
				default:
					break;
				}
				}
				break;
			case 3:
				enterOuterAlt(_localctx, 3);
				{
				setState(105);
				match(T__18);
				setState(106);
				exp(0);
				setState(107);
				match(T__2);
				setState(111);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(108);
					statement();
					}
					}
					setState(113);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(114);
				match(T__3);
				setState(127);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(115);
						match(T__19);
						setState(116);
						match(T__18);
						setState(117);
						match(T__2);
						setState(121);
						_errHandler.sync(this);
						_la = _input.LA(1);
						while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
							{
							{
							setState(118);
							statement();
							}
							}
							setState(123);
							_errHandler.sync(this);
							_la = _input.LA(1);
						}
						setState(124);
						match(T__3);
						}
						} 
					}
					setState(129);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,18,_ctx);
				}
				setState(139);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__19) {
					{
					setState(130);
					match(T__19);
					setState(131);
					match(T__2);
					setState(135);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
						{
						{
						setState(132);
						statement();
						}
						}
						setState(137);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					setState(138);
					match(T__3);
					}
				}

				}
				break;
			case 4:
				enterOuterAlt(_localctx, 4);
				{
				setState(141);
				match(T__20);
				setState(142);
				exp(0);
				setState(143);
				match(T__2);
				setState(157);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==WORD) {
					{
					{
					setState(144);
					identifier();
					setState(145);
					match(T__21);
					setState(149);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
					while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
						if ( _alt==1 ) {
							{
							{
							setState(146);
							statement();
							}
							} 
						}
						setState(151);
						_errHandler.sync(this);
						_alt = getInterpreter().adaptivePredict(_input,21,_ctx);
					}
					setState(153);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__22) {
						{
						setState(152);
						match(T__22);
						}
					}

					}
					}
					setState(159);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(160);
				match(T__23);
				setState(161);
				match(T__21);
				setState(165);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,24,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(162);
						statement();
						}
						} 
					}
					setState(167);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,24,_ctx);
				}
				setState(168);
				match(T__22);
				setState(169);
				match(T__3);
				}
				break;
			case 5:
				enterOuterAlt(_localctx, 5);
				{
				setState(171);
				match(T__22);
				}
				break;
			case 6:
				enterOuterAlt(_localctx, 6);
				{
				setState(172);
				match(T__24);
				}
				break;
			case 7:
				enterOuterAlt(_localctx, 7);
				{
				setState(173);
				match(T__25);
				setState(187);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,25,_ctx) ) {
				case 1:
					{
					setState(174);
					identifier();
					setState(175);
					match(T__26);
					setState(176);
					identifier();
					}
					break;
				case 2:
					{
					setState(178);
					identifier();
					setState(179);
					match(T__11);
					setState(180);
					exp(0);
					setState(181);
					match(T__27);
					setState(182);
					exp(0);
					setState(183);
					match(T__27);
					setState(184);
					exp(0);
					}
					break;
				case 3:
					{
					setState(186);
					exp(0);
					}
					break;
				}
				setState(189);
				match(T__2);
				setState(193);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(190);
					statement();
					}
					}
					setState(195);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(196);
				match(T__3);
				}
				break;
			case 8:
				enterOuterAlt(_localctx, 8);
				{
				setState(199);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__28) {
					{
					setState(198);
					match(T__28);
					}
				}

				setState(202);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(201);
					match(T__0);
					}
				}

				setState(204);
				match(T__29);
				setState(206);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__10) {
					{
					setState(205);
					match(T__10);
					}
				}

				setState(208);
				identifier();
				setState(209);
				match(T__30);
				setState(224);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==WORD) {
					{
					setState(210);
					identifier();
					setState(212);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__21) {
						{
						setState(211);
						type_decl();
						}
					}

					setState(221);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__31) {
						{
						{
						setState(214);
						match(T__31);
						setState(215);
						identifier();
						setState(217);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==T__21) {
							{
							setState(216);
							type_decl();
							}
						}

						}
						}
						setState(223);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(226);
				match(T__32);
				setState(227);
				match(T__2);
				setState(231);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(228);
					statement();
					}
					}
					setState(233);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(234);
				match(T__3);
				}
				break;
			case 9:
				enterOuterAlt(_localctx, 9);
				{
				setState(236);
				match(T__33);
				setState(237);
				exp(0);
				}
				break;
			case 10:
				enterOuterAlt(_localctx, 10);
				{
				setState(238);
				match(T__34);
				setState(240);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(239);
					type_decl();
					}
				}

				setState(242);
				match(T__2);
				setState(270);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==WORD) {
					{
					{
					setState(243);
					identifier();
					setState(266);
					_errHandler.sync(this);
					switch (_input.LA(1)) {
					case T__11:
						{
						setState(244);
						match(T__11);
						setState(245);
						exp(0);
						}
						break;
					case T__30:
						{
						setState(246);
						match(T__30);
						setState(261);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==WORD) {
							{
							setState(247);
							identifier();
							setState(249);
							_errHandler.sync(this);
							_la = _input.LA(1);
							if (_la==T__21) {
								{
								setState(248);
								type_decl();
								}
							}

							setState(258);
							_errHandler.sync(this);
							_la = _input.LA(1);
							while (_la==T__31) {
								{
								{
								setState(251);
								match(T__31);
								setState(252);
								identifier();
								setState(254);
								_errHandler.sync(this);
								_la = _input.LA(1);
								if (_la==T__21) {
									{
									setState(253);
									type_decl();
									}
								}

								}
								}
								setState(260);
								_errHandler.sync(this);
								_la = _input.LA(1);
							}
							}
						}

						setState(263);
						match(T__32);
						}
						break;
					case T__2:
						{
						setState(264);
						match(T__2);
						setState(265);
						match(T__3);
						}
						break;
					default:
						throw new NoViableAltException(this);
					}
					}
					}
					setState(272);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(273);
				match(T__3);
				}
				break;
			case 11:
				enterOuterAlt(_localctx, 11);
				{
				setState(274);
				match(T__35);
				setState(275);
				exp(0);
				}
				break;
			case 12:
				enterOuterAlt(_localctx, 12);
				{
				setState(276);
				match(T__22);
				}
				break;
			case 13:
				enterOuterAlt(_localctx, 13);
				{
				setState(277);
				match(T__24);
				}
				break;
			case 14:
				enterOuterAlt(_localctx, 14);
				{
				setState(278);
				match(T__36);
				}
				break;
			case 15:
				enterOuterAlt(_localctx, 15);
				{
				setState(279);
				_la = _input.LA(1);
				if ( !(_la==T__37 || _la==T__38) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				setState(280);
				identifier();
				setState(283);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__11) {
					{
					setState(281);
					match(T__11);
					setState(282);
					exp(0);
					}
				}

				}
				break;
			case 16:
				enterOuterAlt(_localctx, 16);
				{
				setState(286);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(285);
					match(T__0);
					}
				}

				setState(288);
				match(T__39);
				setState(289);
				identifier();
				setState(291);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(290);
					type_decl();
					}
				}

				setState(293);
				match(T__2);
				setState(294);
				statement();
				setState(295);
				match(T__3);
				}
				break;
			case 17:
				enterOuterAlt(_localctx, 17);
				{
				setState(298);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__0) {
					{
					setState(297);
					match(T__0);
					}
				}

				setState(300);
				match(T__40);
				setState(301);
				identifier();
				setState(303);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(302);
					type_decl();
					}
				}

				setState(305);
				match(T__2);
				setState(306);
				statement();
				setState(307);
				match(T__3);
				}
				break;
			case 18:
				enterOuterAlt(_localctx, 18);
				{
				setState(309);
				match(T__41);
				setState(310);
				identifier();
				setState(312);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(311);
					type_decl();
					}
				}

				setState(314);
				match(T__2);
				setState(315);
				statement();
				setState(316);
				match(T__3);
				}
				break;
			case 19:
				enterOuterAlt(_localctx, 19);
				{
				setState(318);
				match(T__42);
				setState(320);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(319);
					type_decl();
					}
				}

				setState(322);
				match(T__2);
				setState(326);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(323);
					statement();
					}
					}
					setState(328);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(329);
				match(T__3);
				}
				break;
			case 20:
				enterOuterAlt(_localctx, 20);
				{
				setState(330);
				match(T__38);
				setState(331);
				identifier();
				setState(332);
				match(T__11);
				setState(333);
				exp(0);
				}
				break;
			case 21:
				enterOuterAlt(_localctx, 21);
				{
				setState(335);
				match(T__43);
				setState(336);
				exp(0);
				}
				break;
			case 22:
				enterOuterAlt(_localctx, 22);
				{
				setState(337);
				match(T__44);
				setState(338);
				identifier();
				}
				break;
			case 23:
				enterOuterAlt(_localctx, 23);
				{
				setState(339);
				match(T__36);
				setState(340);
				identifier();
				}
				break;
			case 24:
				enterOuterAlt(_localctx, 24);
				{
				setState(341);
				match(T__45);
				setState(342);
				match(T__46);
				setState(343);
				string();
				setState(344);
				match(T__47);
				}
				break;
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class Type_declContext extends ParserRuleContext {
		public List<IdentifierContext> identifier() {
			return getRuleContexts(IdentifierContext.class);
		}
		public IdentifierContext identifier(int i) {
			return getRuleContext(IdentifierContext.class,i);
		}
		public NumberContext number() {
			return getRuleContext(NumberContext.class,0);
		}
		public Type_declContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_type_decl; }
	}

	public final Type_declContext type_decl() throws RecognitionException {
		Type_declContext _localctx = new Type_declContext(_ctx, getState());
		enterRule(_localctx, 8, RULE_type_decl);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(348);
			match(T__21);
			setState(356);
			_errHandler.sync(this);
			switch (_input.LA(1)) {
			case T__48:
				{
				setState(349);
				match(T__48);
				setState(352);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,51,_ctx) ) {
				case 1:
					{
					setState(350);
					identifier();
					}
					break;
				case 2:
					{
					setState(351);
					number();
					}
					break;
				}
				setState(354);
				match(T__49);
				}
				break;
			case T__50:
				{
				setState(355);
				match(T__50);
				}
				break;
			case WORD:
				break;
			default:
				break;
			}
			setState(358);
			identifier();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class ExpContext extends ParserRuleContext {
		public List<IdentifierContext> identifier() {
			return getRuleContexts(IdentifierContext.class);
		}
		public IdentifierContext identifier(int i) {
			return getRuleContext(IdentifierContext.class,i);
		}
		public NumberContext number() {
			return getRuleContext(NumberContext.class,0);
		}
		public StringContext string() {
			return getRuleContext(StringContext.class,0);
		}
		public List<ExpContext> exp() {
			return getRuleContexts(ExpContext.class);
		}
		public ExpContext exp(int i) {
			return getRuleContext(ExpContext.class,i);
		}
		public List<StatementContext> statement() {
			return getRuleContexts(StatementContext.class);
		}
		public StatementContext statement(int i) {
			return getRuleContext(StatementContext.class,i);
		}
		public List<Type_declContext> type_decl() {
			return getRuleContexts(Type_declContext.class);
		}
		public Type_declContext type_decl(int i) {
			return getRuleContext(Type_declContext.class,i);
		}
		public ExpContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_exp; }
	}

	public final ExpContext exp() throws RecognitionException {
		return exp(0);
	}

	private ExpContext exp(int _p) throws RecognitionException {
		ParserRuleContext _parentctx = _ctx;
		int _parentState = getState();
		ExpContext _localctx = new ExpContext(_ctx, _parentState);
		ExpContext _prevctx = _localctx;
		int _startState = 10;
		enterRecursionRule(_localctx, 10, RULE_exp, _p);
		int _la;
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(515);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,69,_ctx) ) {
			case 1:
				{
				setState(361);
				identifier();
				}
				break;
			case 2:
				{
				setState(362);
				number();
				}
				break;
			case 3:
				{
				setState(363);
				string();
				}
				break;
			case 4:
				{
				setState(364);
				_la = _input.LA(1);
				if ( !(_la==T__51 || _la==T__52) ) {
				_errHandler.recoverInline(this);
				}
				else {
					if ( _input.LA(1)==Token.EOF ) matchedEOF = true;
					_errHandler.reportMatch(this);
					consume();
				}
				}
				break;
			case 5:
				{
				setState(365);
				match(T__53);
				setState(366);
				identifier();
				}
				break;
			case 6:
				{
				setState(367);
				match(T__30);
				setState(368);
				exp(0);
				setState(369);
				match(T__32);
				}
				break;
			case 7:
				{
				setState(371);
				identifier();
				setState(372);
				match(T__30);
				setState(381);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,54,_ctx) ) {
				case 1:
					{
					setState(373);
					exp(0);
					setState(378);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__31) {
						{
						{
						setState(374);
						match(T__31);
						setState(375);
						exp(0);
						}
						}
						setState(380);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
					break;
				}
				setState(383);
				match(T__32);
				}
				break;
			case 8:
				{
				setState(385);
				identifier();
				setState(386);
				match(T__48);
				setState(392);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,55,_ctx) ) {
				case 1:
					{
					setState(387);
					exp(0);
					}
					break;
				case 2:
					{
					setState(388);
					exp(0);
					setState(389);
					match(T__64);
					setState(390);
					exp(0);
					}
					break;
				}
				setState(394);
				match(T__49);
				}
				break;
			case 9:
				{
				setState(396);
				match(T__10);
				setState(398);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,56,_ctx) ) {
				case 1:
					{
					setState(397);
					identifier();
					}
					break;
				}
				setState(404);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
				while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
					if ( _alt==1 ) {
						{
						{
						setState(400);
						match(T__10);
						setState(401);
						identifier();
						}
						} 
					}
					setState(406);
					_errHandler.sync(this);
					_alt = getInterpreter().adaptivePredict(_input,57,_ctx);
				}
				setState(409);
				_errHandler.sync(this);
				switch ( getInterpreter().adaptivePredict(_input,58,_ctx) ) {
				case 1:
					{
					setState(407);
					match(T__21);
					setState(408);
					identifier();
					}
					break;
				}
				}
				break;
			case 10:
				{
				setState(411);
				match(T__30);
				setState(412);
				identifier();
				setState(413);
				match(T__32);
				setState(414);
				exp(24);
				}
				break;
			case 11:
				{
				setState(416);
				match(T__68);
				setState(417);
				exp(17);
				}
				break;
			case 12:
				{
				setState(418);
				match(T__55);
				setState(419);
				exp(16);
				}
				break;
			case 13:
				{
				setState(420);
				match(T__69);
				setState(421);
				exp(15);
				}
				break;
			case 14:
				{
				setState(422);
				match(T__70);
				setState(423);
				exp(13);
				}
				break;
			case 15:
				{
				setState(424);
				match(T__53);
				setState(425);
				exp(11);
				}
				break;
			case 16:
				{
				setState(426);
				match(T__57);
				setState(427);
				exp(10);
				}
				break;
			case 17:
				{
				setState(428);
				match(T__30);
				setState(429);
				exp(0);
				setState(430);
				match(T__32);
				setState(431);
				match(T__71);
				setState(432);
				match(T__2);
				setState(436);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(433);
					statement();
					}
					}
					setState(438);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(439);
				match(T__3);
				}
				break;
			case 18:
				{
				setState(441);
				match(T__30);
				setState(442);
				exp(0);
				{
				setState(443);
				match(T__31);
				setState(444);
				exp(0);
				}
				setState(446);
				match(T__32);
				}
				break;
			case 19:
				{
				setState(448);
				match(T__48);
				setState(449);
				exp(0);
				{
				setState(450);
				match(T__31);
				setState(451);
				exp(0);
				}
				setState(453);
				match(T__49);
				}
				break;
			case 20:
				{
				setState(455);
				match(T__2);
				setState(466);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while (_la==WORD) {
					{
					{
					setState(456);
					identifier();
					setState(458);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__21) {
						{
						setState(457);
						type_decl();
						}
					}

					setState(462);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__11) {
						{
						setState(460);
						match(T__11);
						setState(461);
						exp(0);
						}
					}

					}
					}
					setState(468);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(469);
				match(T__3);
				}
				break;
			case 21:
				{
				setState(470);
				match(T__29);
				setState(471);
				match(T__30);
				setState(486);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==WORD) {
					{
					setState(472);
					identifier();
					setState(474);
					_errHandler.sync(this);
					_la = _input.LA(1);
					if (_la==T__21) {
						{
						setState(473);
						type_decl();
						}
					}

					setState(483);
					_errHandler.sync(this);
					_la = _input.LA(1);
					while (_la==T__31) {
						{
						{
						setState(476);
						match(T__31);
						setState(477);
						identifier();
						setState(479);
						_errHandler.sync(this);
						_la = _input.LA(1);
						if (_la==T__21) {
							{
							setState(478);
							type_decl();
							}
						}

						}
						}
						setState(485);
						_errHandler.sync(this);
						_la = _input.LA(1);
					}
					}
				}

				setState(488);
				match(T__32);
				setState(490);
				_errHandler.sync(this);
				_la = _input.LA(1);
				if (_la==T__21) {
					{
					setState(489);
					type_decl();
					}
				}

				setState(492);
				match(T__2);
				setState(496);
				_errHandler.sync(this);
				_la = _input.LA(1);
				while ((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << T__0) | (1L << T__7) | (1L << T__8) | (1L << T__9) | (1L << T__10) | (1L << T__18) | (1L << T__20) | (1L << T__22) | (1L << T__24) | (1L << T__25) | (1L << T__28) | (1L << T__29) | (1L << T__33) | (1L << T__34) | (1L << T__35) | (1L << T__36) | (1L << T__37) | (1L << T__38) | (1L << T__39) | (1L << T__40) | (1L << T__41) | (1L << T__42) | (1L << T__43) | (1L << T__44) | (1L << T__45))) != 0) || _la==WORD) {
					{
					{
					setState(493);
					statement();
					}
					}
					setState(498);
					_errHandler.sync(this);
					_la = _input.LA(1);
				}
				setState(499);
				match(T__3);
				}
				break;
			case 22:
				{
				setState(500);
				match(T__38);
				setState(501);
				match(T__30);
				setState(502);
				exp(0);
				setState(503);
				match(T__32);
				}
				break;
			case 23:
				{
				setState(505);
				match(T__73);
				setState(506);
				match(T__30);
				setState(507);
				exp(0);
				setState(508);
				match(T__32);
				}
				break;
			case 24:
				{
				setState(510);
				match(T__74);
				setState(511);
				match(T__30);
				setState(512);
				exp(0);
				setState(513);
				match(T__32);
				}
				break;
			}
			_ctx.stop = _input.LT(-1);
			setState(580);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,71,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( _parseListeners!=null ) triggerExitRuleEvent();
					_prevctx = _localctx;
					{
					setState(578);
					_errHandler.sync(this);
					switch ( getInterpreter().adaptivePredict(_input,70,_ctx) ) {
					case 1:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(517);
						if (!(precpred(_ctx, 40))) throw new FailedPredicateException(this, "precpred(_ctx, 40)");
						setState(518);
						match(T__54);
						setState(519);
						exp(41);
						}
						break;
					case 2:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(520);
						if (!(precpred(_ctx, 39))) throw new FailedPredicateException(this, "precpred(_ctx, 39)");
						setState(521);
						match(T__55);
						setState(522);
						exp(40);
						}
						break;
					case 3:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(523);
						if (!(precpred(_ctx, 38))) throw new FailedPredicateException(this, "precpred(_ctx, 38)");
						setState(524);
						match(T__56);
						setState(525);
						exp(39);
						}
						break;
					case 4:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(526);
						if (!(precpred(_ctx, 37))) throw new FailedPredicateException(this, "precpred(_ctx, 37)");
						setState(527);
						match(T__57);
						setState(528);
						exp(38);
						}
						break;
					case 5:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(529);
						if (!(precpred(_ctx, 35))) throw new FailedPredicateException(this, "precpred(_ctx, 35)");
						setState(530);
						match(T__58);
						setState(531);
						exp(36);
						}
						break;
					case 6:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(532);
						if (!(precpred(_ctx, 34))) throw new FailedPredicateException(this, "precpred(_ctx, 34)");
						setState(533);
						match(T__59);
						setState(534);
						exp(35);
						}
						break;
					case 7:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(535);
						if (!(precpred(_ctx, 33))) throw new FailedPredicateException(this, "precpred(_ctx, 33)");
						setState(536);
						match(T__60);
						setState(537);
						exp(34);
						}
						break;
					case 8:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(538);
						if (!(precpred(_ctx, 32))) throw new FailedPredicateException(this, "precpred(_ctx, 32)");
						setState(539);
						match(T__61);
						setState(540);
						exp(33);
						}
						break;
					case 9:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(541);
						if (!(precpred(_ctx, 31))) throw new FailedPredicateException(this, "precpred(_ctx, 31)");
						setState(542);
						match(T__62);
						setState(543);
						exp(32);
						}
						break;
					case 10:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(544);
						if (!(precpred(_ctx, 30))) throw new FailedPredicateException(this, "precpred(_ctx, 30)");
						setState(545);
						match(T__46);
						setState(546);
						exp(31);
						}
						break;
					case 11:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(547);
						if (!(precpred(_ctx, 29))) throw new FailedPredicateException(this, "precpred(_ctx, 29)");
						setState(548);
						match(T__63);
						setState(549);
						exp(30);
						}
						break;
					case 12:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(550);
						if (!(precpred(_ctx, 28))) throw new FailedPredicateException(this, "precpred(_ctx, 28)");
						setState(551);
						match(T__47);
						setState(552);
						exp(29);
						}
						break;
					case 13:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(553);
						if (!(precpred(_ctx, 23))) throw new FailedPredicateException(this, "precpred(_ctx, 23)");
						setState(554);
						match(T__53);
						setState(555);
						exp(24);
						}
						break;
					case 14:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(556);
						if (!(precpred(_ctx, 22))) throw new FailedPredicateException(this, "precpred(_ctx, 22)");
						setState(557);
						match(T__65);
						setState(558);
						exp(23);
						}
						break;
					case 15:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(559);
						if (!(precpred(_ctx, 21))) throw new FailedPredicateException(this, "precpred(_ctx, 21)");
						setState(560);
						match(T__66);
						setState(561);
						exp(22);
						}
						break;
					case 16:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(562);
						if (!(precpred(_ctx, 20))) throw new FailedPredicateException(this, "precpred(_ctx, 20)");
						setState(563);
						match(T__12);
						setState(564);
						exp(21);
						}
						break;
					case 17:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(565);
						if (!(precpred(_ctx, 19))) throw new FailedPredicateException(this, "precpred(_ctx, 19)");
						setState(566);
						match(T__13);
						setState(567);
						exp(20);
						}
						break;
					case 18:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(568);
						if (!(precpred(_ctx, 18))) throw new FailedPredicateException(this, "precpred(_ctx, 18)");
						setState(569);
						match(T__67);
						setState(570);
						exp(19);
						}
						break;
					case 19:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(571);
						if (!(precpred(_ctx, 14))) throw new FailedPredicateException(this, "precpred(_ctx, 14)");
						setState(572);
						match(T__69);
						}
						break;
					case 20:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(573);
						if (!(precpred(_ctx, 12))) throw new FailedPredicateException(this, "precpred(_ctx, 12)");
						setState(574);
						match(T__70);
						}
						break;
					case 21:
						{
						_localctx = new ExpContext(_parentctx, _parentState);
						pushNewRecursionContext(_localctx, _startState, RULE_exp);
						setState(575);
						if (!(precpred(_ctx, 8))) throw new FailedPredicateException(this, "precpred(_ctx, 8)");
						setState(576);
						match(T__72);
						setState(577);
						identifier();
						}
						break;
					}
					} 
				}
				setState(582);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,71,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			unrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public static class StringContext extends ParserRuleContext {
		public TerminalNode ANY() { return getToken(NanoScriptParser.ANY, 0); }
		public StringContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_string; }
	}

	public final StringContext string() throws RecognitionException {
		StringContext _localctx = new StringContext(_ctx, getState());
		enterRule(_localctx, 12, RULE_string);
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(583);
			match(T__75);
			setState(584);
			match(ANY);
			setState(585);
			match(T__75);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class IdentifierContext extends ParserRuleContext {
		public List<TerminalNode> WORD() { return getTokens(NanoScriptParser.WORD); }
		public TerminalNode WORD(int i) {
			return getToken(NanoScriptParser.WORD, i);
		}
		public IdentifierContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_identifier; }
	}

	public final IdentifierContext identifier() throws RecognitionException {
		IdentifierContext _localctx = new IdentifierContext(_ctx, getState());
		enterRule(_localctx, 14, RULE_identifier);
		int _la;
		try {
			enterOuterAlt(_localctx, 1);
			{
			setState(587);
			match(WORD);
			setState(592);
			_errHandler.sync(this);
			_la = _input.LA(1);
			while (_la==T__10) {
				{
				{
				setState(588);
				match(T__10);
				setState(589);
				match(WORD);
				}
				}
				setState(594);
				_errHandler.sync(this);
				_la = _input.LA(1);
			}
			{
			setState(595);
			match(T__21);
			setState(596);
			match(WORD);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class NumberContext extends ParserRuleContext {
		public List<TerminalNode> DIGIT() { return getTokens(NanoScriptParser.DIGIT); }
		public TerminalNode DIGIT(int i) {
			return getToken(NanoScriptParser.DIGIT, i);
		}
		public DecimalContext decimal() {
			return getRuleContext(DecimalContext.class,0);
		}
		public NumberContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_number; }
	}

	public final NumberContext number() throws RecognitionException {
		NumberContext _localctx = new NumberContext(_ctx, getState());
		enterRule(_localctx, 16, RULE_number);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(601);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,73,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(598);
					match(DIGIT);
					}
					} 
				}
				setState(603);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,73,_ctx);
			}
			setState(605);
			_errHandler.sync(this);
			switch ( getInterpreter().adaptivePredict(_input,74,_ctx) ) {
			case 1:
				{
				setState(604);
				decimal();
				}
				break;
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public static class DecimalContext extends ParserRuleContext {
		public List<TerminalNode> DIGIT() { return getTokens(NanoScriptParser.DIGIT); }
		public TerminalNode DIGIT(int i) {
			return getToken(NanoScriptParser.DIGIT, i);
		}
		public DecimalContext(ParserRuleContext parent, int invokingState) {
			super(parent, invokingState);
		}
		@Override public int getRuleIndex() { return RULE_decimal; }
	}

	public final DecimalContext decimal() throws RecognitionException {
		DecimalContext _localctx = new DecimalContext(_ctx, getState());
		enterRule(_localctx, 18, RULE_decimal);
		try {
			int _alt;
			enterOuterAlt(_localctx, 1);
			{
			setState(607);
			match(T__10);
			setState(611);
			_errHandler.sync(this);
			_alt = getInterpreter().adaptivePredict(_input,75,_ctx);
			while ( _alt!=2 && _alt!=org.antlr.v4.runtime.atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					{
					{
					setState(608);
					match(DIGIT);
					}
					} 
				}
				setState(613);
				_errHandler.sync(this);
				_alt = getInterpreter().adaptivePredict(_input,75,_ctx);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			_errHandler.reportError(this, re);
			_errHandler.recover(this, re);
		}
		finally {
			exitRule();
		}
		return _localctx;
	}

	public boolean sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 5:
			return exp_sempred((ExpContext)_localctx, predIndex);
		}
		return true;
	}
	private boolean exp_sempred(ExpContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0:
			return precpred(_ctx, 40);
		case 1:
			return precpred(_ctx, 39);
		case 2:
			return precpred(_ctx, 38);
		case 3:
			return precpred(_ctx, 37);
		case 4:
			return precpred(_ctx, 35);
		case 5:
			return precpred(_ctx, 34);
		case 6:
			return precpred(_ctx, 33);
		case 7:
			return precpred(_ctx, 32);
		case 8:
			return precpred(_ctx, 31);
		case 9:
			return precpred(_ctx, 30);
		case 10:
			return precpred(_ctx, 29);
		case 11:
			return precpred(_ctx, 28);
		case 12:
			return precpred(_ctx, 23);
		case 13:
			return precpred(_ctx, 22);
		case 14:
			return precpred(_ctx, 21);
		case 15:
			return precpred(_ctx, 20);
		case 16:
			return precpred(_ctx, 19);
		case 17:
			return precpred(_ctx, 18);
		case 18:
			return precpred(_ctx, 14);
		case 19:
			return precpred(_ctx, 12);
		case 20:
			return precpred(_ctx, 8);
		}
		return true;
	}

	public static final String _serializedATN =
		"\3\u608b\ua72a\u8133\ub9ed\u417c\u3be7\u7786\u5964\3Q\u0269\4\2\t\2\4"+
		"\3\t\3\4\4\t\4\4\5\t\5\4\6\t\6\4\7\t\7\4\b\t\b\4\t\t\t\4\n\t\n\4\13\t"+
		"\13\3\2\6\2\30\n\2\r\2\16\2\31\3\3\5\3\35\n\3\3\3\3\3\3\3\7\3\"\n\3\f"+
		"\3\16\3%\13\3\3\3\3\3\7\3)\n\3\f\3\16\3,\13\3\3\3\3\3\7\3\60\n\3\f\3\16"+
		"\3\63\13\3\5\3\65\n\3\3\4\3\4\3\4\3\4\5\4;\n\4\3\4\3\4\3\4\3\4\3\4\5\4"+
		"B\n\4\3\5\5\5E\n\5\3\5\5\5H\n\5\3\5\5\5K\n\5\3\5\3\5\5\5O\n\5\3\5\3\5"+
		"\5\5S\n\5\3\5\5\5V\n\5\3\5\3\5\5\5Z\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5j\n\5\3\5\3\5\3\5\3\5\7\5p\n\5\f\5\16\5"+
		"s\13\5\3\5\3\5\3\5\3\5\3\5\7\5z\n\5\f\5\16\5}\13\5\3\5\7\5\u0080\n\5\f"+
		"\5\16\5\u0083\13\5\3\5\3\5\3\5\7\5\u0088\n\5\f\5\16\5\u008b\13\5\3\5\5"+
		"\5\u008e\n\5\3\5\3\5\3\5\3\5\3\5\3\5\7\5\u0096\n\5\f\5\16\5\u0099\13\5"+
		"\3\5\5\5\u009c\n\5\7\5\u009e\n\5\f\5\16\5\u00a1\13\5\3\5\3\5\3\5\7\5\u00a6"+
		"\n\5\f\5\16\5\u00a9\13\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00be\n\5\3\5\3\5\7\5\u00c2\n\5\f\5"+
		"\16\5\u00c5\13\5\3\5\3\5\3\5\5\5\u00ca\n\5\3\5\5\5\u00cd\n\5\3\5\3\5\5"+
		"\5\u00d1\n\5\3\5\3\5\3\5\3\5\5\5\u00d7\n\5\3\5\3\5\3\5\5\5\u00dc\n\5\7"+
		"\5\u00de\n\5\f\5\16\5\u00e1\13\5\5\5\u00e3\n\5\3\5\3\5\3\5\7\5\u00e8\n"+
		"\5\f\5\16\5\u00eb\13\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u00f3\n\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\5\5\u00fc\n\5\3\5\3\5\3\5\5\5\u0101\n\5\7\5\u0103\n"+
		"\5\f\5\16\5\u0106\13\5\5\5\u0108\n\5\3\5\3\5\3\5\5\5\u010d\n\5\7\5\u010f"+
		"\n\5\f\5\16\5\u0112\13\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u011e"+
		"\n\5\3\5\5\5\u0121\n\5\3\5\3\5\3\5\5\5\u0126\n\5\3\5\3\5\3\5\3\5\3\5\5"+
		"\5\u012d\n\5\3\5\3\5\3\5\5\5\u0132\n\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5"+
		"\u013b\n\5\3\5\3\5\3\5\3\5\3\5\3\5\5\5\u0143\n\5\3\5\3\5\7\5\u0147\n\5"+
		"\f\5\16\5\u014a\13\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3\5\3"+
		"\5\3\5\3\5\3\5\3\5\5\5\u015d\n\5\3\6\3\6\3\6\3\6\5\6\u0163\n\6\3\6\3\6"+
		"\5\6\u0167\n\6\3\6\3\6\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\7\7\u017b\n\7\f\7\16\7\u017e\13\7\5\7\u0180\n\7\3\7\3"+
		"\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7\u018b\n\7\3\7\3\7\3\7\3\7\5\7\u0191"+
		"\n\7\3\7\3\7\7\7\u0195\n\7\f\7\16\7\u0198\13\7\3\7\3\7\5\7\u019c\n\7\3"+
		"\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\3\7\7\7\u01b5\n\7\f\7\16\7\u01b8\13\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\5\7\u01cd"+
		"\n\7\3\7\3\7\5\7\u01d1\n\7\7\7\u01d3\n\7\f\7\16\7\u01d6\13\7\3\7\3\7\3"+
		"\7\3\7\3\7\5\7\u01dd\n\7\3\7\3\7\3\7\5\7\u01e2\n\7\7\7\u01e4\n\7\f\7\16"+
		"\7\u01e7\13\7\5\7\u01e9\n\7\3\7\3\7\5\7\u01ed\n\7\3\7\3\7\7\7\u01f1\n"+
		"\7\f\7\16\7\u01f4\13\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\5\7\u0206\n\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3"+
		"\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7"+
		"\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\3\7\7\7\u0245"+
		"\n\7\f\7\16\7\u0248\13\7\3\b\3\b\3\b\3\b\3\t\3\t\3\t\7\t\u0251\n\t\f\t"+
		"\16\t\u0254\13\t\3\t\3\t\3\t\3\n\7\n\u025a\n\n\f\n\16\n\u025d\13\n\3\n"+
		"\5\n\u0260\n\n\3\13\3\13\7\13\u0264\n\13\f\13\16\13\u0267\13\13\3\13\2"+
		"\3\f\f\2\4\6\b\n\f\16\20\22\24\2\5\3\2\n\f\3\2()\3\2\66\67\2\u02f4\2\27"+
		"\3\2\2\2\4\34\3\2\2\2\6A\3\2\2\2\b\u015c\3\2\2\2\n\u015e\3\2\2\2\f\u0205"+
		"\3\2\2\2\16\u0249\3\2\2\2\20\u024d\3\2\2\2\22\u025b\3\2\2\2\24\u0261\3"+
		"\2\2\2\26\30\5\4\3\2\27\26\3\2\2\2\30\31\3\2\2\2\31\27\3\2\2\2\31\32\3"+
		"\2\2\2\32\3\3\2\2\2\33\35\7\3\2\2\34\33\3\2\2\2\34\35\3\2\2\2\35\36\3"+
		"\2\2\2\36\37\7\4\2\2\37#\5\20\t\2 \"\5\6\4\2! \3\2\2\2\"%\3\2\2\2#!\3"+
		"\2\2\2#$\3\2\2\2$\64\3\2\2\2%#\3\2\2\2&*\7\5\2\2\')\5\b\5\2(\'\3\2\2\2"+
		"),\3\2\2\2*(\3\2\2\2*+\3\2\2\2+-\3\2\2\2,*\3\2\2\2-\65\7\6\2\2.\60\5\b"+
		"\5\2/.\3\2\2\2\60\63\3\2\2\2\61/\3\2\2\2\61\62\3\2\2\2\62\65\3\2\2\2\63"+
		"\61\3\2\2\2\64&\3\2\2\2\64\61\3\2\2\2\64\65\3\2\2\2\65\5\3\2\2\2\66\67"+
		"\7\7\2\2\67:\5\16\b\289\7\b\2\29;\5\20\t\2:8\3\2\2\2:;\3\2\2\2;B\3\2\2"+
		"\2<=\7\7\2\2=>\5\20\t\2>?\7\t\2\2?@\5\16\b\2@B\3\2\2\2A\66\3\2\2\2A<\3"+
		"\2\2\2B\7\3\2\2\2CE\7\3\2\2DC\3\2\2\2DE\3\2\2\2EG\3\2\2\2FH\t\2\2\2GF"+
		"\3\2\2\2GH\3\2\2\2HJ\3\2\2\2IK\7\r\2\2JI\3\2\2\2JK\3\2\2\2KL\3\2\2\2L"+
		"N\5\20\t\2MO\5\n\6\2NM\3\2\2\2NO\3\2\2\2OR\3\2\2\2PQ\7\16\2\2QS\5\f\7"+
		"\2RP\3\2\2\2RS\3\2\2\2S\u015d\3\2\2\2TV\7\r\2\2UT\3\2\2\2UV\3\2\2\2VW"+
		"\3\2\2\2WY\5\20\t\2XZ\5\n\6\2YX\3\2\2\2YZ\3\2\2\2Zi\3\2\2\2[\\\7\16\2"+
		"\2\\j\5\f\7\2]^\7\17\2\2^j\5\f\7\2_`\7\20\2\2`j\5\f\7\2ab\7\21\2\2bj\5"+
		"\f\7\2cd\7\22\2\2dj\5\f\7\2ef\7\23\2\2fj\5\f\7\2gh\7\24\2\2hj\5\f\7\2"+
		"i[\3\2\2\2i]\3\2\2\2i_\3\2\2\2ia\3\2\2\2ic\3\2\2\2ie\3\2\2\2ig\3\2\2\2"+
		"ij\3\2\2\2j\u015d\3\2\2\2kl\7\25\2\2lm\5\f\7\2mq\7\5\2\2np\5\b\5\2on\3"+
		"\2\2\2ps\3\2\2\2qo\3\2\2\2qr\3\2\2\2rt\3\2\2\2sq\3\2\2\2t\u0081\7\6\2"+
		"\2uv\7\26\2\2vw\7\25\2\2w{\7\5\2\2xz\5\b\5\2yx\3\2\2\2z}\3\2\2\2{y\3\2"+
		"\2\2{|\3\2\2\2|~\3\2\2\2}{\3\2\2\2~\u0080\7\6\2\2\177u\3\2\2\2\u0080\u0083"+
		"\3\2\2\2\u0081\177\3\2\2\2\u0081\u0082\3\2\2\2\u0082\u008d\3\2\2\2\u0083"+
		"\u0081\3\2\2\2\u0084\u0085\7\26\2\2\u0085\u0089\7\5\2\2\u0086\u0088\5"+
		"\b\5\2\u0087\u0086\3\2\2\2\u0088\u008b\3\2\2\2\u0089\u0087\3\2\2\2\u0089"+
		"\u008a\3\2\2\2\u008a\u008c\3\2\2\2\u008b\u0089\3\2\2\2\u008c\u008e\7\6"+
		"\2\2\u008d\u0084\3\2\2\2\u008d\u008e\3\2\2\2\u008e\u015d\3\2\2\2\u008f"+
		"\u0090\7\27\2\2\u0090\u0091\5\f\7\2\u0091\u009f\7\5\2\2\u0092\u0093\5"+
		"\20\t\2\u0093\u0097\7\30\2\2\u0094\u0096\5\b\5\2\u0095\u0094\3\2\2\2\u0096"+
		"\u0099\3\2\2\2\u0097\u0095\3\2\2\2\u0097\u0098\3\2\2\2\u0098\u009b\3\2"+
		"\2\2\u0099\u0097\3\2\2\2\u009a\u009c\7\31\2\2\u009b\u009a\3\2\2\2\u009b"+
		"\u009c\3\2\2\2\u009c\u009e\3\2\2\2\u009d\u0092\3\2\2\2\u009e\u00a1\3\2"+
		"\2\2\u009f\u009d\3\2\2\2\u009f\u00a0\3\2\2\2\u00a0\u00a2\3\2\2\2\u00a1"+
		"\u009f\3\2\2\2\u00a2\u00a3\7\32\2\2\u00a3\u00a7\7\30\2\2\u00a4\u00a6\5"+
		"\b\5\2\u00a5\u00a4\3\2\2\2\u00a6\u00a9\3\2\2\2\u00a7\u00a5\3\2\2\2\u00a7"+
		"\u00a8\3\2\2\2\u00a8\u00aa\3\2\2\2\u00a9\u00a7\3\2\2\2\u00aa\u00ab\7\31"+
		"\2\2\u00ab\u00ac\7\6\2\2\u00ac\u015d\3\2\2\2\u00ad\u015d\7\31\2\2\u00ae"+
		"\u015d\7\33\2\2\u00af\u00bd\7\34\2\2\u00b0\u00b1\5\20\t\2\u00b1\u00b2"+
		"\7\35\2\2\u00b2\u00b3\5\20\t\2\u00b3\u00be\3\2\2\2\u00b4\u00b5\5\20\t"+
		"\2\u00b5\u00b6\7\16\2\2\u00b6\u00b7\5\f\7\2\u00b7\u00b8\7\36\2\2\u00b8"+
		"\u00b9\5\f\7\2\u00b9\u00ba\7\36\2\2\u00ba\u00bb\5\f\7\2\u00bb\u00be\3"+
		"\2\2\2\u00bc\u00be\5\f\7\2\u00bd\u00b0\3\2\2\2\u00bd\u00b4\3\2\2\2\u00bd"+
		"\u00bc\3\2\2\2\u00be\u00bf\3\2\2\2\u00bf\u00c3\7\5\2\2\u00c0\u00c2\5\b"+
		"\5\2\u00c1\u00c0\3\2\2\2\u00c2\u00c5\3\2\2\2\u00c3\u00c1\3\2\2\2\u00c3"+
		"\u00c4\3\2\2\2\u00c4\u00c6\3\2\2\2\u00c5\u00c3\3\2\2\2\u00c6\u00c7\7\6"+
		"\2\2\u00c7\u015d\3\2\2\2\u00c8\u00ca\7\37\2\2\u00c9\u00c8\3\2\2\2\u00c9"+
		"\u00ca\3\2\2\2\u00ca\u00cc\3\2\2\2\u00cb\u00cd\7\3\2\2\u00cc\u00cb\3\2"+
		"\2\2\u00cc\u00cd\3\2\2\2\u00cd\u00ce\3\2\2\2\u00ce\u00d0\7 \2\2\u00cf"+
		"\u00d1\7\r\2\2\u00d0\u00cf\3\2\2\2\u00d0\u00d1\3\2\2\2\u00d1\u00d2\3\2"+
		"\2\2\u00d2\u00d3\5\20\t\2\u00d3\u00e2\7!\2\2\u00d4\u00d6\5\20\t\2\u00d5"+
		"\u00d7\5\n\6\2\u00d6\u00d5\3\2\2\2\u00d6\u00d7\3\2\2\2\u00d7\u00df\3\2"+
		"\2\2\u00d8\u00d9\7\"\2\2\u00d9\u00db\5\20\t\2\u00da\u00dc\5\n\6\2\u00db"+
		"\u00da\3\2\2\2\u00db\u00dc\3\2\2\2\u00dc\u00de\3\2\2\2\u00dd\u00d8\3\2"+
		"\2\2\u00de\u00e1\3\2\2\2\u00df\u00dd\3\2\2\2\u00df\u00e0\3\2\2\2\u00e0"+
		"\u00e3\3\2\2\2\u00e1\u00df\3\2\2\2\u00e2\u00d4\3\2\2\2\u00e2\u00e3\3\2"+
		"\2\2\u00e3\u00e4\3\2\2\2\u00e4\u00e5\7#\2\2\u00e5\u00e9\7\5\2\2\u00e6"+
		"\u00e8\5\b\5\2\u00e7\u00e6\3\2\2\2\u00e8\u00eb\3\2\2\2\u00e9\u00e7\3\2"+
		"\2\2\u00e9\u00ea\3\2\2\2\u00ea\u00ec\3\2\2\2\u00eb\u00e9\3\2\2\2\u00ec"+
		"\u00ed\7\6\2\2\u00ed\u015d\3\2\2\2\u00ee\u00ef\7$\2\2\u00ef\u015d\5\f"+
		"\7\2\u00f0\u00f2\7%\2\2\u00f1\u00f3\5\n\6\2\u00f2\u00f1\3\2\2\2\u00f2"+
		"\u00f3\3\2\2\2\u00f3\u00f4\3\2\2\2\u00f4\u0110\7\5\2\2\u00f5\u010c\5\20"+
		"\t\2\u00f6\u00f7\7\16\2\2\u00f7\u010d\5\f\7\2\u00f8\u0107\7!\2\2\u00f9"+
		"\u00fb\5\20\t\2\u00fa\u00fc\5\n\6\2\u00fb\u00fa\3\2\2\2\u00fb\u00fc\3"+
		"\2\2\2\u00fc\u0104\3\2\2\2\u00fd\u00fe\7\"\2\2\u00fe\u0100\5\20\t\2\u00ff"+
		"\u0101\5\n\6\2\u0100\u00ff\3\2\2\2\u0100\u0101\3\2\2\2\u0101\u0103\3\2"+
		"\2\2\u0102\u00fd\3\2\2\2\u0103\u0106\3\2\2\2\u0104\u0102\3\2\2\2\u0104"+
		"\u0105\3\2\2\2\u0105\u0108\3\2\2\2\u0106\u0104\3\2\2\2\u0107\u00f9\3\2"+
		"\2\2\u0107\u0108\3\2\2\2\u0108\u0109\3\2\2\2\u0109\u010d\7#\2\2\u010a"+
		"\u010b\7\5\2\2\u010b\u010d\7\6\2\2\u010c\u00f6\3\2\2\2\u010c\u00f8\3\2"+
		"\2\2\u010c\u010a\3\2\2\2\u010d\u010f\3\2\2\2\u010e\u00f5\3\2\2\2\u010f"+
		"\u0112\3\2\2\2\u0110\u010e\3\2\2\2\u0110\u0111\3\2\2\2\u0111\u0113\3\2"+
		"\2\2\u0112\u0110\3\2\2\2\u0113\u015d\7\6\2\2\u0114\u0115\7&\2\2\u0115"+
		"\u015d\5\f\7\2\u0116\u015d\7\31\2\2\u0117\u015d\7\33\2\2\u0118\u015d\7"+
		"\'\2\2\u0119\u011a\t\3\2\2\u011a\u011d\5\20\t\2\u011b\u011c\7\16\2\2\u011c"+
		"\u011e\5\f\7\2\u011d\u011b\3\2\2\2\u011d\u011e\3\2\2\2\u011e\u015d\3\2"+
		"\2\2\u011f\u0121\7\3\2\2\u0120\u011f\3\2\2\2\u0120\u0121\3\2\2\2\u0121"+
		"\u0122\3\2\2\2\u0122\u0123\7*\2\2\u0123\u0125\5\20\t\2\u0124\u0126\5\n"+
		"\6\2\u0125\u0124\3\2\2\2\u0125\u0126\3\2\2\2\u0126\u0127\3\2\2\2\u0127"+
		"\u0128\7\5\2\2\u0128\u0129\5\b\5\2\u0129\u012a\7\6\2\2\u012a\u015d\3\2"+
		"\2\2\u012b\u012d\7\3\2\2\u012c\u012b\3\2\2\2\u012c\u012d\3\2\2\2\u012d"+
		"\u012e\3\2\2\2\u012e\u012f\7+\2\2\u012f\u0131\5\20\t\2\u0130\u0132\5\n"+
		"\6\2\u0131\u0130\3\2\2\2\u0131\u0132\3\2\2\2\u0132\u0133\3\2\2\2\u0133"+
		"\u0134\7\5\2\2\u0134\u0135\5\b\5\2\u0135\u0136\7\6\2\2\u0136\u015d\3\2"+
		"\2\2\u0137\u0138\7,\2\2\u0138\u013a\5\20\t\2\u0139\u013b\5\n\6\2\u013a"+
		"\u0139\3\2\2\2\u013a\u013b\3\2\2\2\u013b\u013c\3\2\2\2\u013c\u013d\7\5"+
		"\2\2\u013d\u013e\5\b\5\2\u013e\u013f\7\6\2\2\u013f\u015d\3\2\2\2\u0140"+
		"\u0142\7-\2\2\u0141\u0143\5\n\6\2\u0142\u0141\3\2\2\2\u0142\u0143\3\2"+
		"\2\2\u0143\u0144\3\2\2\2\u0144\u0148\7\5\2\2\u0145\u0147\5\b\5\2\u0146"+
		"\u0145\3\2\2\2\u0147\u014a\3\2\2\2\u0148\u0146\3\2\2\2\u0148\u0149\3\2"+
		"\2\2\u0149\u014b\3\2\2\2\u014a\u0148\3\2\2\2\u014b\u015d\7\6\2\2\u014c"+
		"\u014d\7)\2\2\u014d\u014e\5\20\t\2\u014e\u014f\7\16\2\2\u014f\u0150\5"+
		"\f\7\2\u0150\u015d\3\2\2\2\u0151\u0152\7.\2\2\u0152\u015d\5\f\7\2\u0153"+
		"\u0154\7/\2\2\u0154\u015d\5\20\t\2\u0155\u0156\7\'\2\2\u0156\u015d\5\20"+
		"\t\2\u0157\u0158\7\60\2\2\u0158\u0159\7\61\2\2\u0159\u015a\5\16\b\2\u015a"+
		"\u015b\7\62\2\2\u015b\u015d\3\2\2\2\u015cD\3\2\2\2\u015cU\3\2\2\2\u015c"+
		"k\3\2\2\2\u015c\u008f\3\2\2\2\u015c\u00ad\3\2\2\2\u015c\u00ae\3\2\2\2"+
		"\u015c\u00af\3\2\2\2\u015c\u00c9\3\2\2\2\u015c\u00ee\3\2\2\2\u015c\u00f0"+
		"\3\2\2\2\u015c\u0114\3\2\2\2\u015c\u0116\3\2\2\2\u015c\u0117\3\2\2\2\u015c"+
		"\u0118\3\2\2\2\u015c\u0119\3\2\2\2\u015c\u0120\3\2\2\2\u015c\u012c\3\2"+
		"\2\2\u015c\u0137\3\2\2\2\u015c\u0140\3\2\2\2\u015c\u014c\3\2\2\2\u015c"+
		"\u0151\3\2\2\2\u015c\u0153\3\2\2\2\u015c\u0155\3\2\2\2\u015c\u0157\3\2"+
		"\2\2\u015d\t\3\2\2\2\u015e\u0166\7\30\2\2\u015f\u0162\7\63\2\2\u0160\u0163"+
		"\5\20\t\2\u0161\u0163\5\22\n\2\u0162\u0160\3\2\2\2\u0162\u0161\3\2\2\2"+
		"\u0162\u0163\3\2\2\2\u0163\u0164\3\2\2\2\u0164\u0167\7\64\2\2\u0165\u0167"+
		"\7\65\2\2\u0166\u015f\3\2\2\2\u0166\u0165\3\2\2\2\u0166\u0167\3\2\2\2"+
		"\u0167\u0168\3\2\2\2\u0168\u0169\5\20\t\2\u0169\13\3\2\2\2\u016a\u016b"+
		"\b\7\1\2\u016b\u0206\5\20\t\2\u016c\u0206\5\22\n\2\u016d\u0206\5\16\b"+
		"\2\u016e\u0206\t\4\2\2\u016f\u0170\78\2\2\u0170\u0206\5\20\t\2\u0171\u0172"+
		"\7!\2\2\u0172\u0173\5\f\7\2\u0173\u0174\7#\2\2\u0174\u0206\3\2\2\2\u0175"+
		"\u0176\5\20\t\2\u0176\u017f\7!\2\2\u0177\u017c\5\f\7\2\u0178\u0179\7\""+
		"\2\2\u0179\u017b\5\f\7\2\u017a\u0178\3\2\2\2\u017b\u017e\3\2\2\2\u017c"+
		"\u017a\3\2\2\2\u017c\u017d\3\2\2\2\u017d\u0180\3\2\2\2\u017e\u017c\3\2"+
		"\2\2\u017f\u0177\3\2\2\2\u017f\u0180\3\2\2\2\u0180\u0181\3\2\2\2\u0181"+
		"\u0182\7#\2\2\u0182\u0206\3\2\2\2\u0183\u0184\5\20\t\2\u0184\u018a\7\63"+
		"\2\2\u0185\u018b\5\f\7\2\u0186\u0187\5\f\7\2\u0187\u0188\7C\2\2\u0188"+
		"\u0189\5\f\7\2\u0189\u018b\3\2\2\2\u018a\u0185\3\2\2\2\u018a\u0186\3\2"+
		"\2\2\u018b\u018c\3\2\2\2\u018c\u018d\7\64\2\2\u018d\u0206\3\2\2\2\u018e"+
		"\u0190\7\r\2\2\u018f\u0191\5\20\t\2\u0190\u018f\3\2\2\2\u0190\u0191\3"+
		"\2\2\2\u0191\u0196\3\2\2\2\u0192\u0193\7\r\2\2\u0193\u0195\5\20\t\2\u0194"+
		"\u0192\3\2\2\2\u0195\u0198\3\2\2\2\u0196\u0194\3\2\2\2\u0196\u0197\3\2"+
		"\2\2\u0197\u019b\3\2\2\2\u0198\u0196\3\2\2\2\u0199\u019a\7\30\2\2\u019a"+
		"\u019c\5\20\t\2\u019b\u0199\3\2\2\2\u019b\u019c\3\2\2\2\u019c\u0206\3"+
		"\2\2\2\u019d\u019e\7!\2\2\u019e\u019f\5\20\t\2\u019f\u01a0\7#\2\2\u01a0"+
		"\u01a1\5\f\7\32\u01a1\u0206\3\2\2\2\u01a2\u01a3\7G\2\2\u01a3\u0206\5\f"+
		"\7\23\u01a4\u01a5\7:\2\2\u01a5\u0206\5\f\7\22\u01a6\u01a7\7H\2\2\u01a7"+
		"\u0206\5\f\7\21\u01a8\u01a9\7I\2\2\u01a9\u0206\5\f\7\17\u01aa\u01ab\7"+
		"8\2\2\u01ab\u0206\5\f\7\r\u01ac\u01ad\7<\2\2\u01ad\u0206\5\f\7\f\u01ae"+
		"\u01af\7!\2\2\u01af\u01b0\5\f\7\2\u01b0\u01b1\7#\2\2\u01b1\u01b2\7J\2"+
		"\2\u01b2\u01b6\7\5\2\2\u01b3\u01b5\5\b\5\2\u01b4\u01b3\3\2\2\2\u01b5\u01b8"+
		"\3\2\2\2\u01b6\u01b4\3\2\2\2\u01b6\u01b7\3\2\2\2\u01b7\u01b9\3\2\2\2\u01b8"+
		"\u01b6\3\2\2\2\u01b9\u01ba\7\6\2\2\u01ba\u0206\3\2\2\2\u01bb\u01bc\7!"+
		"\2\2\u01bc\u01bd\5\f\7\2\u01bd\u01be\7\"\2\2\u01be\u01bf\5\f\7\2\u01bf"+
		"\u01c0\3\2\2\2\u01c0\u01c1\7#\2\2\u01c1\u0206\3\2\2\2\u01c2\u01c3\7\63"+
		"\2\2\u01c3\u01c4\5\f\7\2\u01c4\u01c5\7\"\2\2\u01c5\u01c6\5\f\7\2\u01c6"+
		"\u01c7\3\2\2\2\u01c7\u01c8\7\64\2\2\u01c8\u0206\3\2\2\2\u01c9\u01d4\7"+
		"\5\2\2\u01ca\u01cc\5\20\t\2\u01cb\u01cd\5\n\6\2\u01cc\u01cb\3\2\2\2\u01cc"+
		"\u01cd\3\2\2\2\u01cd\u01d0\3\2\2\2\u01ce\u01cf\7\16\2\2\u01cf\u01d1\5"+
		"\f\7\2\u01d0\u01ce\3\2\2\2\u01d0\u01d1\3\2\2\2\u01d1\u01d3\3\2\2\2\u01d2"+
		"\u01ca\3\2\2\2\u01d3\u01d6\3\2\2\2\u01d4\u01d2\3\2\2\2\u01d4\u01d5\3\2"+
		"\2\2\u01d5\u01d7\3\2\2\2\u01d6\u01d4\3\2\2\2\u01d7\u0206\7\6\2\2\u01d8"+
		"\u01d9\7 \2\2\u01d9\u01e8\7!\2\2\u01da\u01dc\5\20\t\2\u01db\u01dd\5\n"+
		"\6\2\u01dc\u01db\3\2\2\2\u01dc\u01dd\3\2\2\2\u01dd\u01e5\3\2\2\2\u01de"+
		"\u01df\7\"\2\2\u01df\u01e1\5\20\t\2\u01e0\u01e2\5\n\6\2\u01e1\u01e0\3"+
		"\2\2\2\u01e1\u01e2\3\2\2\2\u01e2\u01e4\3\2\2\2\u01e3\u01de\3\2\2\2\u01e4"+
		"\u01e7\3\2\2\2\u01e5\u01e3\3\2\2\2\u01e5\u01e6\3\2\2\2\u01e6\u01e9\3\2"+
		"\2\2\u01e7\u01e5\3\2\2\2\u01e8\u01da\3\2\2\2\u01e8\u01e9\3\2\2\2\u01e9"+
		"\u01ea\3\2\2\2\u01ea\u01ec\7#\2\2\u01eb\u01ed\5\n\6\2\u01ec\u01eb\3\2"+
		"\2\2\u01ec\u01ed\3\2\2\2\u01ed\u01ee\3\2\2\2\u01ee\u01f2\7\5\2\2\u01ef"+
		"\u01f1\5\b\5\2\u01f0\u01ef\3\2\2\2\u01f1\u01f4\3\2\2\2\u01f2\u01f0\3\2"+
		"\2\2\u01f2\u01f3\3\2\2\2\u01f3\u01f5\3\2\2\2\u01f4\u01f2\3\2\2\2\u01f5"+
		"\u0206\7\6\2\2\u01f6\u01f7\7)\2\2\u01f7\u01f8\7!\2\2\u01f8\u01f9\5\f\7"+
		"\2\u01f9\u01fa\7#\2\2\u01fa\u0206\3\2\2\2\u01fb\u01fc\7L\2\2\u01fc\u01fd"+
		"\7!\2\2\u01fd\u01fe\5\f\7\2\u01fe\u01ff\7#\2\2\u01ff\u0206\3\2\2\2\u0200"+
		"\u0201\7M\2\2\u0201\u0202\7!\2\2\u0202\u0203\5\f\7\2\u0203\u0204\7#\2"+
		"\2\u0204\u0206\3\2\2\2\u0205\u016a\3\2\2\2\u0205\u016c\3\2\2\2\u0205\u016d"+
		"\3\2\2\2\u0205\u016e\3\2\2\2\u0205\u016f\3\2\2\2\u0205\u0171\3\2\2\2\u0205"+
		"\u0175\3\2\2\2\u0205\u0183\3\2\2\2\u0205\u018e\3\2\2\2\u0205\u019d\3\2"+
		"\2\2\u0205\u01a2\3\2\2\2\u0205\u01a4\3\2\2\2\u0205\u01a6\3\2\2\2\u0205"+
		"\u01a8\3\2\2\2\u0205\u01aa\3\2\2\2\u0205\u01ac\3\2\2\2\u0205\u01ae\3\2"+
		"\2\2\u0205\u01bb\3\2\2\2\u0205\u01c2\3\2\2\2\u0205\u01c9\3\2\2\2\u0205"+
		"\u01d8\3\2\2\2\u0205\u01f6\3\2\2\2\u0205\u01fb\3\2\2\2\u0205\u0200\3\2"+
		"\2\2\u0206\u0246\3\2\2\2\u0207\u0208\f*\2\2\u0208\u0209\79\2\2\u0209\u0245"+
		"\5\f\7+\u020a\u020b\f)\2\2\u020b\u020c\7:\2\2\u020c\u0245\5\f\7*\u020d"+
		"\u020e\f(\2\2\u020e\u020f\7;\2\2\u020f\u0245\5\f\7)\u0210\u0211\f\'\2"+
		"\2\u0211\u0212\7<\2\2\u0212\u0245\5\f\7(\u0213\u0214\f%\2\2\u0214\u0215"+
		"\7=\2\2\u0215\u0245\5\f\7&\u0216\u0217\f$\2\2\u0217\u0218\7>\2\2\u0218"+
		"\u0245\5\f\7%\u0219\u021a\f#\2\2\u021a\u021b\7?\2\2\u021b\u0245\5\f\7"+
		"$\u021c\u021d\f\"\2\2\u021d\u021e\7@\2\2\u021e\u0245\5\f\7#\u021f\u0220"+
		"\f!\2\2\u0220\u0221\7A\2\2\u0221\u0245\5\f\7\"\u0222\u0223\f \2\2\u0223"+
		"\u0224\7\61\2\2\u0224\u0245\5\f\7!\u0225\u0226\f\37\2\2\u0226\u0227\7"+
		"B\2\2\u0227\u0245\5\f\7 \u0228\u0229\f\36\2\2\u0229\u022a\7\62\2\2\u022a"+
		"\u0245\5\f\7\37\u022b\u022c\f\31\2\2\u022c\u022d\78\2\2\u022d\u0245\5"+
		"\f\7\32\u022e\u022f\f\30\2\2\u022f\u0230\7D\2\2\u0230\u0245\5\f\7\31\u0231"+
		"\u0232\f\27\2\2\u0232\u0233\7E\2\2\u0233\u0245\5\f\7\30\u0234\u0235\f"+
		"\26\2\2\u0235\u0236\7\17\2\2\u0236\u0245\5\f\7\27\u0237\u0238\f\25\2\2"+
		"\u0238\u0239\7\20\2\2\u0239\u0245\5\f\7\26\u023a\u023b\f\24\2\2\u023b"+
		"\u023c\7F\2\2\u023c\u0245\5\f\7\25\u023d\u023e\f\20\2\2\u023e\u0245\7"+
		"H\2\2\u023f\u0240\f\16\2\2\u0240\u0245\7I\2\2\u0241\u0242\f\n\2\2\u0242"+
		"\u0243\7K\2\2\u0243\u0245\5\20\t\2\u0244\u0207\3\2\2\2\u0244\u020a\3\2"+
		"\2\2\u0244\u020d\3\2\2\2\u0244\u0210\3\2\2\2\u0244\u0213\3\2\2\2\u0244"+
		"\u0216\3\2\2\2\u0244\u0219\3\2\2\2\u0244\u021c\3\2\2\2\u0244\u021f\3\2"+
		"\2\2\u0244\u0222\3\2\2\2\u0244\u0225\3\2\2\2\u0244\u0228\3\2\2\2\u0244"+
		"\u022b\3\2\2\2\u0244\u022e\3\2\2\2\u0244\u0231\3\2\2\2\u0244\u0234\3\2"+
		"\2\2\u0244\u0237\3\2\2\2\u0244\u023a\3\2\2\2\u0244\u023d\3\2\2\2\u0244"+
		"\u023f\3\2\2\2\u0244\u0241\3\2\2\2\u0245\u0248\3\2\2\2\u0246\u0244\3\2"+
		"\2\2\u0246\u0247\3\2\2\2\u0247\r\3\2\2\2\u0248\u0246\3\2\2\2\u0249\u024a"+
		"\7N\2\2\u024a\u024b\7O\2\2\u024b\u024c\7N\2\2\u024c\17\3\2\2\2\u024d\u0252"+
		"\7P\2\2\u024e\u024f\7\r\2\2\u024f\u0251\7P\2\2\u0250\u024e\3\2\2\2\u0251"+
		"\u0254\3\2\2\2\u0252\u0250\3\2\2\2\u0252\u0253\3\2\2\2\u0253\u0255\3\2"+
		"\2\2\u0254\u0252\3\2\2\2\u0255\u0256\7\30\2\2\u0256\u0257\7P\2\2\u0257"+
		"\21\3\2\2\2\u0258\u025a\7Q\2\2\u0259\u0258\3\2\2\2\u025a\u025d\3\2\2\2"+
		"\u025b\u0259\3\2\2\2\u025b\u025c\3\2\2\2\u025c\u025f\3\2\2\2\u025d\u025b"+
		"\3\2\2\2\u025e\u0260\5\24\13\2\u025f\u025e\3\2\2\2\u025f\u0260\3\2\2\2"+
		"\u0260\23\3\2\2\2\u0261\u0265\7\r\2\2\u0262\u0264\7Q\2\2\u0263\u0262\3"+
		"\2\2\2\u0264\u0267\3\2\2\2\u0265\u0263\3\2\2\2\u0265\u0266\3\2\2\2\u0266"+
		"\25\3\2\2\2\u0267\u0265\3\2\2\2N\31\34#*\61\64:ADGJNRUYiq{\u0081\u0089"+
		"\u008d\u0097\u009b\u009f\u00a7\u00bd\u00c3\u00c9\u00cc\u00d0\u00d6\u00db"+
		"\u00df\u00e2\u00e9\u00f2\u00fb\u0100\u0104\u0107\u010c\u0110\u011d\u0120"+
		"\u0125\u012c\u0131\u013a\u0142\u0148\u015c\u0162\u0166\u017c\u017f\u018a"+
		"\u0190\u0196\u019b\u01b6\u01cc\u01d0\u01d4\u01dc\u01e1\u01e5\u01e8\u01ec"+
		"\u01f2\u0205\u0244\u0246\u0252\u025b\u025f\u0265";
	public static final ATN _ATN =
		new ATNDeserializer().deserialize(_serializedATN.toCharArray());
	static {
		_decisionToDFA = new DFA[_ATN.getNumberOfDecisions()];
		for (int i = 0; i < _ATN.getNumberOfDecisions(); i++) {
			_decisionToDFA[i] = new DFA(_ATN.getDecisionState(i), i);
		}
	}
}