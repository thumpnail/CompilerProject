//TODO: Program:
using "../../../sample/mymodule.nano";
namespace module_main
{
    public static void main()
    {
        System.Console.Write(NanoScript.Parser.AstNodes.StringExpression);

    }

}
namespace module_myModule
{
    //TODO: VariableDeclarationStatement:
    public const float PI = 3.14;
    //TODO: VariableDeclarationStatement:
    public float radius = 5;
    //TODO: VariableDeclarationStatement:
    public readonly float diameter = radius / 2;
    //TODO: StructDeclarationStatement:
    public class Circle
    {
        //TODO: VariableDeclarationStatement:
        public readonly Point center;
        //TODO: VariableDeclarationStatement:
        public readonly float radius;
        public static void init(Point center, float radius)
        {
            //TODO: AssignmentStatement:;
            //TODO: AssignmentStatement:;

        }
        public static float area()
        {
            return math.PI * .radius * .radius;

        }
    }
    public enum Color
    {
        Red,
        Green,
        Blue,
    }
    public static float calculateDistance(Point p1, Point p2)
    {
//TODO: VariableDeclarationStatement:
private readonly float dx = p2.x - p1.x;

    //TODO: VariableDeclarationStatement:
    private readonly float dy = p2.y - p1.y;

return math.sqrt(dx* dx + dy* dy,);

}
public static object getPoint()
{

}

}

