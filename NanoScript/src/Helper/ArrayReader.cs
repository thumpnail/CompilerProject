namespace NanoScript.Helper;
/// <summary>
/// Generic Class for Array based parsing.
/// </summary>
/// <typeparam name="T">List Item Type</typeparam>
public class ArrayReader<T> {
	T[] arr;
	int idx;
	public ArrayReader(T[] arr) {
		this.arr = arr;
		this.idx = 0;
	}
	public T Peekc() {
		if(idx < arr.Length)
			return arr[idx];
		PrintError();
		throw new Exception($"IndexOutOfRangeException() -> '{ToString()}'");
	}
	public bool Peekc(Func<T,bool> predicate) {
		if(idx < arr.Length) return predicate(arr[idx]);
		PrintError();
        throw new IndexOutOfRangeException();
	}
	public T Peekn() {
		if(idx+1 < arr.Length) return arr[idx+1];
		PrintError();
		throw new IndexOutOfRangeException();
	}
	public bool Peekn(int value) {
		if(idx+1 < arr.Length) {
			var tmp = arr[idx + 1];
			if (tmp is not null) {
				if(tmp.Equals(value)) 
					return true; 
				else 
					return false;
			}
			PrintError();
			throw new NullReferenceException();
		}
		PrintError();
		throw new IndexOutOfRangeException();
	}
	public T Consume() {
		if(idx < arr.Length) {
			return arr[idx++];
		}
		PrintError();
                          			throw new IndexOutOfRangeException();
	}
	public void Incr() {
		idx++;
	}
	public bool Incr(T expected, bool ret = false) {
		if (!ret) {
			if (idx > arr.Length) throw new Exception($"Expected: '{expected}' got 'index out of bounds'");
			var tmp = arr[idx];
			if (tmp is not null && tmp.Equals(expected)) {
				idx++;
			} else {
				PrintError();
				throw new Exception($"Expected: '{expected}' got '{arr[idx]}'");
			}
			return true;
		} else {
			try {
				if (idx >arr.Length) throw new Exception($"Expected: '{expected}' got 'index out of bounds'");
				var tmp = arr[idx];
				if (tmp is not null && tmp.Equals(expected)) {
					idx++;
				} else {
					PrintError();
					throw new Exception($"Expected: '{expected}' got '{arr[idx]}'");
				}
				return true;
			}
			catch (Exception e) {
				Console.WriteLine(e);
				return false;
			}
		}
	}
	public void Incr(params T[] expected) {
		foreach (var item in expected) {
			Incr(item);
		}
	}
	public void set(int i) {
		this.idx = i;
	}
	public int get() {
		return this.idx;
	}
	public bool IsEOF() {
		if (idx < arr.Length) {
			return true;
		} else {
			return false;
		}
	}
	public void PrintError() {
		Console.WriteLine($"i: {idx}");
		Console.WriteLine($"current: {Peekc()}");
		Console.WriteLine(ToString());
	}
	public string PrintErrorS() {
		StringBuilder sb = new();
		sb.AppendLine($"i: {idx}");
		sb.AppendLine($"current: {Peekc()}");
		sb.AppendLine(ToString());
		return sb.ToString();
	}
	public string ToString() {
		return string.Join(" | ",arr);
	}

	//##################################################################################
	//##################################################################################
	//##################################################################################
	public void ContinueUntil(int str) {
		while (!arr[idx]!.Equals(str)) {
			idx++;
		}
	}

	public bool LineContainsNumber() {
		var searchset = arr[idx..(idx+15)];
		if(arr is string[]) //gatekeep
			foreach (var item in searchset) {
				if (item is not null && item.Equals("\n")) {
					return false;
				}
				if (item is string) {
					if (item.ToString().ContainsNumbers())
						return true;
				}
			}
		return false;
	}
	public int CountUntil(int str) { //FIXME: Zeitfresser
		var tmp_idx = idx;
		for (int j = idx; j < arr.Length; j++) {
			var tmp = arr[j];
			if (tmp is not null && tmp.Equals(str)) {
				return j - tmp_idx;
			}
		}
		return -1;
	}
}
