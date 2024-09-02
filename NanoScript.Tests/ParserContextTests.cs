// Initialize ParserContext with default constructor

using NanoScript;
using NanoScript.Lexer;
using NanoScript.Parser;
using Xunit;
using NanoScript;
using NanoScript.Lexer;
using NanoScript.Tests.Common;
using Xunit;

namespace NanoScript.Tests;

public class ParserContextTests {
	[Fact]
	public void initialize_parser_context_with_default_constructor() {
		var parserContext = new ParserContext();
		Assert.NotNull(parserContext);
		Assert.Equal(StateType.None, parserContext.state);
		Assert.Equal(0, parserContext.idx);
		Assert.Empty(parserContext.importedFiles);
		Assert.NotNull(parserContext.frameStack);
	}
	// Peek when token list is empty
	[Fact]
	public void peek_when_token_list_is_empty() {
		var lexerResult = new LexerResult<Token> { result = new List<TokenElement<Token>>() };
		var parserContext = new ParserContext(lexerResult);
		var peekResult = parserContext.Peek();
		Assert.Null(peekResult);
	}

	// Successfully push and pop frames using CreateFrame and ClearFrame
	[Fact]
	public void push_and_pop_frames_successfully() {
		// Arrange
		var parserContext = new ParserContext();

		// Act
		parserContext.CreateFrame();
		parserContext.CreateFrame();
		parserContext.CreateFrame();
		parserContext.ClearFrame();

		// Assert
		Assert.Equal(2, parserContext.frameStack.Count);
	}

	// Initialize ParserContext with LexerResult
	[Fact]
	public void initialize_parser_context_with_lexer_result() {
		// Arrange
		var tokens = new List<TokenElement<Token>>();
		var lexerResult = new LexerResult<Token>() { result = tokens };

		// Act
		var parserContext = new ParserContext(lexerResult);

		// Assert
		Assert.NotNull(parserContext);
		Assert.Equal(StateType.None, parserContext.state);
		Assert.Equal(0, parserContext.idx);
		Assert.Empty(parserContext.importedFiles);
		Assert.NotNull(parserContext.frameStack);
		Assert.Same(lexerResult, parserContext.lexerResult);
		Assert.Same(tokens, parserContext.tokens);
	}

	// Peek at current token value
	[Fact]
	public void peek_at_current_token_value_returns_correct_token_value() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>(Token.IMPORT, "import", 0, "import".Length),
			new TokenElement<Token>(Token.IDENTIFIER, "ident", 8, "ident".Length),
			new TokenElement<Token>(Token.MOD, "mod", 15, "mod".Length),
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var peekedValue = parserContext.Peek();

		// Assert
		Assert.Equal("token1", peekedValue);
	}
// Additional tests can be added to cover different scenarios

	// Peek at specific index token value
	[Fact]
	public void peek_at_specific_index_token_value() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token> { Value = "token1" },
			new TokenElement<Token> { Value = "token2" },
			new TokenElement<Token> { Value = "token3" }
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var peekedToken = parserContext.PeekAtIndex(1);

		// Assert
		Assert.Equal("token2", peekedToken);
	}

	// Consume specific token value
	[Fact]
	public void consume_specific_token_value_successfully() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token> { Value = "token1" },
			new TokenElement<Token> { Value = "token2" },
			new TokenElement<Token> { Value = "token3" }
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var result = parserContext.Consume("token1");

		// Assert
		Assert.True(result);
		Assert.Equal(1, parserContext.idx);
	}
// Additional test cases can be added to cover different scenarios

	// Consume current token value
	[Fact]
	public void consume_current_token_value() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>( Token.NONE, "token1", 0, 6),
			new TokenElement<Token>( Token.NONE, "token2", 7, 6),
			new TokenElement<Token>( Token.NONE, "token3", 14, 6)
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var consumedToken = parserContext.Consume();

		// Assert
		Assert.Equal("token1", consumedToken);
		Assert.Equal(1, parserContext.idx);
	}
// Additional assertions can be added to cover edge cases

	// Peek at next token value
	[Fact]
	public void peek_at_next_token_value_returns_correct_value() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>(Token.NONE, "token1", 0,6),
			new TokenElement<Token>(Token.NONE, "token2", 0,6),
			new TokenElement<Token>(Token.NONE, "token3", 0,6)
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var peekedValue = parserContext.PeekNext();

		// Assert
		Assert.Equal("token2", peekedValue);
	}
// Additional tests can be added to cover different scenarios

	// Peek at current token type
	[Fact]
	public void peek_at_current_token_type() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>(Token.NONE, "token1", 0, 6),
			new TokenElement<Token>(Token.NONE, "token2", 0, 6),
			new TokenElement<Token>(Token.NONE, "token3", 0, 6)
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		var peekedToken = parserContext.Peek_tk();

		// Assert
		Assert.Equal(Token.NONE, peekedToken);
	}

	// Consume when token list is empty
	[Fact]
	public void consume_returns_null_when_token_list_is_empty() {
		// Arrange
		var parserContext = new ParserContext();

		// Act
		var result = parserContext.Consume();

		// Assert
		Assert.Null(result);
	}

	// Consume current token type
	[Fact]
	public void consume_current_token_type_when_token_matches() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>(Token.NONE, "token1", 0 , 6),
			new TokenElement<Token>(Token.NONE, "token2", 0 , 6),
			new TokenElement<Token>(Token.NONE, "token3", 0 , 6)
		};
		var lexerResult = new LexerResult<Token>() {result = tokens};
		var parserContext = new ParserContext(lexerResult);

		// Act
		var result = parserContext.Consume_tk(Token.NONE);

		// Assert
		Assert.True(result);
		Assert.Equal(Token.NONE, parserContext.Peek_tk());
	}

	// Pop frame when frame stack is empty
	[Fact]
	public void pop_frame_when_frame_stack_is_empty() {
		// Arrange
		var parserContext = new ParserContext();

		// Act
		parserContext.PopFrame();

		// Assert
		Assert.Empty(parserContext.frameStack);
	}

	// Peek at index out of bounds
	[Fact]
	public void peek_at_index_out_of_bounds_returns_null() {
		// Arrange
		var parserContext = new ParserContext();

		// Act
		var result = parserContext.PeekAtIndex(-1);

		// Assert
		Assert.Null(result);
	}

	// Consume token type not present
	[Fact]
	public void consume_token_type_not_present() {
		// Arrange
		var parserContext = new ParserContext();

		// Act
		var result = parserContext.Consume_tk(Token.IDENTIFIER);

		// Assert
		Assert.False(result);
	}

	// Handle null or invalid LexerResult in constructor
	// stupid since LexerResult is a value type(struct)
		//[Fact]
		//public void handle_null_or_invalid_lexer_result_in_constructor() {
		//	// Arrange
		//	LexerResult<Token>? invalidResult = null;
		//	// Act
		//	var parserContext = new ParserContext(invalidResult);
		//	// Assert
		//	Assert.NotNull(parserContext);
		//	Assert.Equal(StateType.None, parserContext.state);
		//	Assert.Equal(0, parserContext.idx);
		//	Assert.Empty(parserContext.importedFiles);
		//	Assert.NotNull(parserContext.frameStack);
		//}

	// Test PeekRangeArray with invalid ranges
	[Fact]
	public void test_peek_range_array_invalid_ranges() {
		// Arrange
		var parserContext = new ParserContext();
		// Act & Assert
		Assert.Throws<IndexOutOfRangeException>(() => parserContext.PeekRangeArray(3));
	}

	// Validate error handling for out of range access
	[Fact]
	public void validate_error_handling_for_out_of_range_access() {
		// Arrange
		var parserContext = new ParserContext();

		// Act & Assert
		Assert.False(parserContext.boundCheck(-1));
		Assert.False(parserContext.boundCheck(0));
		Assert.False(parserContext.boundCheck(parserContext.tokens.Count));
		Assert.False(parserContext.boundCheck(parserContext.tokens.Count + 1));

		Assert.Throws<IndexOutOfRangeException>(() => parserContext.PeekRangeArray(-1));
		Assert.Throws<IndexOutOfRangeException>(() => parserContext.PeekRangeArray(parserContext.tokens.Count + 1));
	}

	// Test PeekRange with varying lengths
	[Fact]
	public void test_peek_range_with_varying_lengths() {
		// Arrange
		var tokens = new List<TokenElement<Token>> {
			new TokenElement<Token>(Token.STRUCT, "struct", 0, 6),
			new TokenElement<Token>(Token.STRING, "\"string\"", 0, 6),
			new TokenElement<Token>(Token.CLASS, "class", 0, 6),
			new TokenElement<Token>(Token.FNC, "fnc", 0, 6)
		};
		var lexerResult = new LexerResult<Token> { result = tokens };
		var parserContext = new ParserContext(lexerResult);

		// Act
		parserContext.idx = 0;
		bool result1 = parserContext.PeekRange("apple", "banana");
		bool result2 = parserContext.PeekRange("banana", "cherry", "date");
		bool result3 = parserContext.PeekRange("apple", "banana", "cherry", "date");

		// Assert
		Assert.True(result1);
		Assert.True(result2);
		Assert.False(result3); // Should fail as the range is longer than available tokens
	}

	// Test Consume with invalid token values
	//[Fact]
	//public void test_consume_with_invalid_token_values() {
	//	// Arrange
	//	var tokens = new List<TokenElement<Token>> {
	//		new TokenElement<Token> { token = Token.NONE, Value = "valid_token" },
	//		new TokenElement<Token> { token = Token.NONE, Value = "another_valid_token" }
	//	};
	//	var lexerResult = new LexerResult<Token> { result = tokens };
	//	var parserContext = new ParserContext(lexerResult);
	//	// Act
	//	var result1 = parserContext.Consume("invalid_token");
	//	var result2 = parserContext.Consume(Token.NONE, out _);
	//	// Assert
	//	Assert.Null(result1);
	//	Assert.False(result2);
	//}
}