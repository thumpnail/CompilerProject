using NanoScript.Lexer;
using NanoScript.Tests.Common;
using Xunit;

// Lex method correctly tokenizes a simple source string
namespace NanoScript.Tests;

public class LexerTests {
	
	[Fact]
	public void lex_method_tokenizes_simple_source_string()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "simple source string";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SIMPLE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method handles an empty source string without errors
	[Fact]
	public void lex_method_handles_empty_source_string()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Empty(result.result);
	}

	// Lex method handles multiple categories and returns tokens in correct order
	[Fact]
	public void lex_method_handles_multiple_categories_and_returns_tokens_in_correct_order()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "simple source string";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SIMPLE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method correctly skips tokens marked as skippable
	[Fact]
	public void lex_method_skips_skippable_tokens()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "simple source string";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = true },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = true }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(1, result.result.Count);
		Assert.Equal(TestToken.SOURCE, result.result[0].token);
	}

	// Lex method respects the priority of tokens when sorting
	[Fact]
	public void lex_method_respects_token_priority_when_sorting()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};
		string source = "simple source string";

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SIMPLE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method returns a LexerResult object with the correct source and tokens
	[Fact]
	public void lex_method_returns_lexer_result_with_correct_source_and_tokens()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};
		string source = "simple source string";

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SIMPLE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method handles categories with identical literals
	[Fact]
	public void lex_method_handles_categories_with_identical_literals()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "identical identical identical";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "identical", token = TestToken.IDENTICAL, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "identical", token = TestToken.IDENTICAL, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "identical", token = TestToken.IDENTICAL, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.IDENTICAL, result.result[0].token);
		Assert.Equal(TestToken.IDENTICAL, result.result[1].token);
		Assert.Equal(TestToken.IDENTICAL, result.result[2].token);
	}

	// Lex method processes tokens with zero length
	[Fact]
	public void lex_method_processes_tokens_with_zero_length()
	{
		// Arrange
		var lexer = new Lexer<TestToken>()
			.child(TestToken.EMPTY,"")
			.child(TestToken.NON_EMPTY,"non-empty");
		
		string source = "token with zero length";

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Empty(result.result);
	}

	// Lex method processes overlapping tokens correctly
	[Fact]
	public void lex_method_processes_overlapping_tokens_correctly()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};

		// Act
		var result = lexer.Lex("simple source string");

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SIMPLE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method handles null or invalid input gracefully
	[Fact]
	public void lex_method_handles_null_input_gracefully()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string? source = null;
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "simple", token = TestToken.SIMPLE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.NotNull(result);
		Assert.Empty(result.result);
	}

	// Lex method correctly removes tokens that overlap with higher priority tokens
	[Fact]
	public void lex_method_correctly_removes_overlapping_tokens()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "ab", token = TestToken.AB, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "abc", token = TestToken.ABC, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "bc", token = TestToken.BC, isSkipable = false }
		};
		string source = "abc";

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Single(result.result);
		Assert.Equal(TestToken.ABC, result.result[0].token);
	}

	// Lex method maintains performance with large source strings
	[Fact]
	public void lex_method_maintains_performance_with_large_source_strings()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "large source string";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "large", token = TestToken.LARGE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "source", token = TestToken.SOURCE, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "string", token = TestToken.STRING, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.LARGE, result.result[0].token);
		Assert.Equal(TestToken.SOURCE, result.result[1].token);
		Assert.Equal(TestToken.STRING, result.result[2].token);
	}

	// Lex method handles special characters in literals
	[Fact]
	public void lex_method_handles_special_characters_in_literals()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "special characters: !@#$%^&*()";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "special", token = TestToken.SPECIAL, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "characters", token = TestToken.CHARACTERS, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = ": \\!@#\\$%\\^&\\*\\(\\)", token = TestToken.SPECIAL_CHARS, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.SPECIAL, result.result[0].token);
		Assert.Equal(TestToken.CHARACTERS, result.result[1].token);
		Assert.Equal(TestToken.SPECIAL_CHARS, result.result[2].token);
	}

	// Lex method processes categories with very high priority values
	[Fact]
	public void lex_method_processes_categories_with_high_priority_values()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "high", token = TestToken.HIGH, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "priority", token = TestToken.PRIORITY, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "values", token = TestToken.VALUES, isSkipable = false }
		};
		string source = "high priority values";

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.HIGH, result.result[0].token);
		Assert.Equal(TestToken.PRIORITY, result.result[1].token);
		Assert.Equal(TestToken.VALUES, result.result[2].token);
	}

	// Lex method handles categories with very low priority values
	[Fact]
	public void lex_method_handles_categories_with_low_priority_values()
	{
		// Arrange
		var lexer = new Lexer<TestToken>();
		string source = "low priority values test";
		lexer._categories = new List<Category<TestToken>>
		{
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "low", token = TestToken.LOW, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "priority", token = TestToken.PRIORITY, isSkipable = false },
			new Category<TestToken> { guid = Guid.NewGuid(), regex = "values", token = TestToken.VALUES, isSkipable = false }
		};

		// Act
		var result = lexer.Lex(source);

		// Assert
		Assert.Equal(3, result.result.Count);
		Assert.Equal(TestToken.LOW, result.result[0].token);
		Assert.Equal(TestToken.PRIORITY, result.result[1].token);
		Assert.Equal(TestToken.VALUES, result.result[2].token);
	}

}