namespace NanoScript.Helper;
#nullable enable

public class Result<T> {
	public bool IsSuccess { get; }
	public T Value { get; }
	public string ErrorMessage { get; } = string.Empty;
	public Exception Exception { get; } = null!;

	private Result(T value) {
		IsSuccess = true;
		Value = value;
	}

	private Result(string errorMessage) {
		IsSuccess = false;
		ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
	}

	private Result(Exception exception) {
		IsSuccess = false;
		Exception = exception ?? throw new ArgumentNullException(nameof(exception));
	}

	public static implicit operator Result<T>(T value) => new Result<T>(value);
	public static implicit operator Result<T>(string errorMessage) => new Result<T>(errorMessage);
	public static implicit operator Result<T>(Exception exception) => new Result<T>(exception);

	public static Result<T> Success(T value) => new Result<T>(value);
	public static Result<T> Failure(string errorMessage) => new Result<T>(errorMessage);
	public static Result<T> Failure(Exception exception) => new Result<T>(exception);

	public Result<T> OnSuccess(Action<T> action) {
		if (IsSuccess) {
			action(Value);
		}
		return this;
	}

	public Result<T> OnFailure(Action<string> action) {
		if (!IsSuccess) {
			action(ErrorMessage);
		}
		return this;
	}

	public Result<T> OnException(Action<Exception> action) {
		if (!IsSuccess) {
			action(Exception);
		}
		return this;
	}

	public T GetValueOrDefault(T defaultValue) => IsSuccess ? Value : defaultValue;

	public string ToString() {
		if (IsSuccess) {
			return $"Success: {Value}";
		} else if (!string.IsNullOrEmpty(ErrorMessage)) {
			return $"Failure: {ErrorMessage}";
		} else {
			return $"Exception: {Exception.Message}";
		}
	}
}