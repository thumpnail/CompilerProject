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
    public static void ToFile(this object obj, string filename) {
        using(StreamWriter writer = new StreamWriter(filename)) {
            writer.WriteLine(obj);
            writer.Close();
        }
    }
}