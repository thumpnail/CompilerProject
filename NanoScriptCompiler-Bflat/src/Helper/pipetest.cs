namespace pipetest;

static class module_Pipes {
	public static class PipeManager {
		public static List<IPipe> pipes = new();
	}

	public struct Pipe<T> : IPipe {
	        public string id { get; set; }
	        T data;
			public static Pipe<T> operator +(Pipe<T> a, Pipe<T> b) {
				return new();
			}
			public static Pipe<T> set(T a) {
				return new();
			}
	}

	public interface IPipe {
		string id { get; set; }
	}

	class test {
		Pipe<int> number = Pipe<int>.set(1);
	}
}