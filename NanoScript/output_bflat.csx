//TODO: Program:
namespace module_main
{
    //TODO: VariableDeclarationStatement:
    public object a = 5;
    //TODO: VariableDeclarationStatement:
    object b = "hello";
    //TODO: VariableDeclarationStatement:
    object c = NanoScript.Parser.AstNodes.BooleanExpression;
    //TODO: VariableDeclarationStatement:
    object e = 3 + 2;
    //TODO: VariableDeclarationStatement:
    object f = a - b;
    //TODO: VariableDeclarationStatement:
    object g = (c / 2);
    //TODO: VariableDeclarationStatement:
    object h = (d * 4);
    //TODO: VariableDeclarationStatement:
    object i = a + b;
    //TODO: VariableDeclarationStatement:
    object j = e || f;
    //TODO: VariableDeclarationStatement:
    object k = g && h;
    //TODO: VariableDeclarationStatement:
    object l = i == j;
    //TODO: VariableDeclarationStatement:
    object m = f != g;
    //TODO: VariableDeclarationStatement:
    object n = h <= e;
    //TODO: VariableDeclarationStatement:
    object o = i < j;
    //TODO: VariableDeclarationStatement:
    object p = k >= l;
    //TODO: VariableDeclarationStatement:
    object q = m > n;
    //TODO: VariableDeclarationStatement:
    object r = myFunction(a, b, c);
    //TODO: VariableDeclarationStatement:
    object s = myArray[i];
    //TODO: VariableDeclarationStatement:
    object u = object.property;
    //TODO: VariableDeclarationStatement:
    object w = e & f;
    //TODO: VariableDeclarationStatement:
    object x = g | h;
    //TODO: VariableDeclarationStatement:
    object y = (Math.Pow(i, j));
    //TODO: VariableDeclarationStatement:
    object z = a << 2;
    //TODO: VariableDeclarationStatement:
    object aa = b >> 1;
    //TODO: VariableDeclarationStatement:
    object cc = !d;
    //TODO: VariableDeclarationStatement:
    object ee = ++f;
    //TODO: VariableDeclarationStatement:
    object ff = g++;
    //TODO: VariableDeclarationStatement:
    object gg = --h;
    //TODO: VariableDeclarationStatement:
    object hh = i--;
    //TODO: VariableDeclarationStatement:
    object nn = [a, b, c,];
    //TODO: VariableDeclarationStatement:
    object fd = System.Console.Write("hello world");
    //TODO: VariableDeclarationStatement:
    object pp = (object x, object y) =>
    {
        return x + y;
    };
    //TODO: VariableDeclarationStatement:
    object qq = typeof(a);
    //TODO: VariableDeclarationStatement:
    object qr = System.Diagnostics.Debug.Assert(a);
    //TODO: VariableDeclarationStatement:
    object rr = sizeof(myArray);
    //TODO: VariableDeclarationStatement:
    object ss = String.Parse(e);
    //TODO: VariableDeclarationStatement:
    object fTest = test("Hello World", 15);
    //TODO: VariableDeclarationStatement:
    object mTest = (2 * (1 / 3 + (15 / (Math.Pow(3, 4)))));
    //TODO: VariableDeclarationStatement:
    object lTest = a || b && c || a && b || a && b || c;
    //TODO: VariableDeclarationStatement:
    readonly Type tTest = System.Type.TestType;
    //TODO: VariableDeclarationStatement:
    const object cTest = "Hello World";
    //TODO: VariableDeclarationStatement:
    readonly double l2Test = 3.1415;
    //TODO: VariableDeclarationStatement:
    object hTest = 1;
    //TODO: VariableDeclarationStatement:
    object arr = [1, 2, 3, 4, 5,];
    //TODO: VariableDeclarationStatement:
    object arr2 = arr[1];

}

