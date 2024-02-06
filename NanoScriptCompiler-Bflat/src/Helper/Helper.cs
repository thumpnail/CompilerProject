public static class Helper {
    public static bool ContainsNumbers(this string str) {
        foreach (var item in str) {
            if (item is string) {
				
            }
        }
        return true;
    }
    public static void ToConsole(this object obj) {
        Console.WriteLine(obj);
    }
}