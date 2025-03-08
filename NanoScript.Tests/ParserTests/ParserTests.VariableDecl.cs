namespace NanoScript.Tests.ParserTests;
public class ParserTests_VariableDecl {
// mod main
// // import "../../../sample/mymodule.nano"
// pub var a = 5
// var b = "hello"
// var c = true
// //var d = &x
	[Fact]
	public void TestVariableDecl_basic_assign() {
		string src = """
		             mod main
		             pub var a = 5
		             var b = "hello"
		             var c = true
		             //var d = &x
		             """;
		
	}
// //no references //TODO
// var e = 3 + 2
// var f = a - b
// var g = c / 2
// var h = d * 4
// var i = (a + b)
// var j = e || f
// var k = g && h
// var l = i == j
// var m = f != g
// var n = h <= e
// var o = i < j
// var p = k >= l
// var q = m > n
// var r = myFunction(a, b, c)
// var s = myArray[i]
// //TODO //var t = myArray[a..b] 
// var u = object.property
// //TODO //var v = (int) a 
// var w = e & f
// var x = g | h
// var y = i ** j
// var z = a << 2
// var aa = b >> 1
// //TODO //var bb = ~c 
// var cc = !d
// //TODO //var dd = -e 
// var ee = ++f
// var ff = g++
// var gg = --h
// var hh = i--
// //TODO // var ii = &j // no references 
// //TODO // var jj = *k // no pointers 
// //var kk = (a) => { return a + 1 }
// //var ll = l is int
// //var mm = (a, b)
// var nn = [a, b, c]
// //var oo = myArray {a, b, c}
// var fd = print("hello world")
// // Should Work Now
// var pp = fnc(x, y) : int { return x + y }
// //todo:
// var qq = type(a)
// var qr = assert(a)
// var rr = size(myArray)
// var ss = str(e)
// 
// var fTest = test("Hello World", 15)
// var mTest = 2 * 1 / 3 + 15 / 3 ** 4
// var lTest = a || b && c || a && b || a && ( b || c)
// let tTest: Type = System.Type.TestType
// const cTest = "Hello World"
// let l2Test: double = 3.1415
// var hTest = 0x0001
// var arr = [1,2,3,4,5]
// var arr2 = arr[1]
}