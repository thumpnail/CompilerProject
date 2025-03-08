namespace NanoScript.Parser;

public static class NumberExtesions {
	public static byte[] ToByteArray(this int number) {
		return BitConverter.GetBytes(number);
	}
	public static int ToInt32(this byte[] number) {
		return BitConverter.ToInt32(number, 0);
	}
}