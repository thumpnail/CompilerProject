using System.Text;
using System.Globalization;
using System.Security.Authentication;
using System.CodeDom;
using System.CodeDom.Compiler;


public class CodeGen {
	StringBuilder sb = new StringBuilder();
	static class FLAGS {
		public static int depth {
			get {
				return inClass + inFor + inWhile + inIf + inSwitch + inFunction + inStruct;
			}
		}
		public static string get_c_indent {
        get {
            var tmp = "";
            for (int i = 0; i < depth; i++) {
                tmp += "\t";
            }
            return tmp;
        }
        set {
            var tmp = "";
            for (int i = 0; i < depth; i++) {
                tmp += "\t";
            }
            get_c_indent = tmp;
        }
    }
		public static int inClass = 0;
		public static int inStruct = 0;
		public static char forIdx = 'a';
		public static int inFor = 0;
        internal static int inWhile = 0;
        internal static int inIf = 0;
        internal static int inSwitch = 0;
        internal static int inFunction = 0;

    }

	public CodeGen() {}

	public void CreateHeader(params string[] lines) {
		foreach(var line in lines) {
			sb.AppendLine(FLAGS.get_c_indent+line);
		}
	}
	//classes
	public void BeginClass(string access, string name, string? inh = null) {
		sb.AppendLine(FLAGS.get_c_indent+$"{access} class {name} {(inh is null ? inh : ": "+ inh)} " + "{");
		FLAGS.inClass++;
	}
	public void EndClass() {
		FLAGS.inClass--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	//structs
	public void BeginStruct(string access, string name, string? inh = null) {
		sb.AppendLine(FLAGS.get_c_indent+$"{access} struct {name} {(inh is null ? inh : ": "+ inh)} " + "{");
		FLAGS.inStruct++;
	}
	public void EndStruct() {
		FLAGS.inStruct--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	//enum
	public void BeginEnum(string access, string name) {
		sb.AppendLine(FLAGS.get_c_indent+$"{access} enum {name} " + "{");
	}
	public void EnumElement(string name) {
		sb.AppendLine(FLAGS.get_c_indent+$"{name},");
	}
	public void EndEnum() {
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	// function
	public void BeginFunction(string access, string type, string name, List<(string type, string name)> para) {
		sb.Append(FLAGS.get_c_indent+$"{access} {type} {name}(");
		para.ForEach(x => {
			sb.Append($"{x.type} {x.name}");
			if(para.Last() != x) {
				sb.Append(", ");
			}
		});
		sb.AppendLine(") {");
		FLAGS.inFunction++;
	}
	public void EndFunction() {
		FLAGS.inFunction--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	// for
	public void BeginFor(string name, int sidx, string length) {
		FLAGS.inFor++;
		sb.AppendLine(FLAGS.get_c_indent+$"for(var {FLAGS.forIdx} = {sidx}; {FLAGS.forIdx} < {length}; {FLAGS.forIdx++}++)");
	}
	public void EndFor() {
		FLAGS.inFor--;
		FLAGS.forIdx--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	// while
	public void BeginWhile(string cond) {
		sb.AppendLine(FLAGS.get_c_indent+$"while({cond}) "+"{");
		FLAGS.inWhile++;
	}
	public void EndWhile() {
		FLAGS.inWhile--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	public void BeginDoWhile() {
		sb.AppendLine(FLAGS.get_c_indent+$"do "+"{");
		FLAGS.inWhile++;
	}
	public void EndDoWhile(string cond) {
		sb.AppendLine(FLAGS.get_c_indent+$"while({cond});");
		FLAGS.inWhile--;
	}
	// if
	public void BeginIf(string cond) {
		sb.AppendLine(FLAGS.get_c_indent+$"if({cond}) "+"{");
		FLAGS.inIf++;
	}
	public void BeginElseIf(string cond) {
		FLAGS.inIf--;
		sb.AppendLine(FLAGS.get_c_indent+"}"+$" else if({cond}) "+"{");
		FLAGS.inIf++;
	}
	public void BeginElse() {
		FLAGS.inIf--;
		sb.AppendLine(FLAGS.get_c_indent+"} else {");
		FLAGS.inIf++;
	}
	public void EndIf() {
		FLAGS.inIf--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	// switch
	public void BeginSwitch(string cond) {
		sb.AppendLine(FLAGS.get_c_indent+$"switch({cond}) "+"{");
		FLAGS.inSwitch++;
	}
	public void EndSwitch() {
		FLAGS.inSwitch--;
		sb.AppendLine(FLAGS.get_c_indent+"}");
	}
	// define
	public void Define(string type, string name, string DEFAULT = null) {
		sb.Append(FLAGS.get_c_indent+$"{type} {name}");
		if(DEFAULT is not null) {
			sb.Append(FLAGS.get_c_indent+$"= {DEFAULT}");
		}
		sb.AppendLine(FLAGS.get_c_indent+";");
	}
	public void Const(string type, string name, string DEFAULT = null) {
		sb.Append(FLAGS.get_c_indent+$"const {type} {name}");
		if(DEFAULT is not null) {
			sb.Append($"= {DEFAULT}");
		}
		sb.AppendLine(";");
	}
	public void Const(Type type, string name, string DEFAULT = null) {
		sb.Append(FLAGS.get_c_indent+$"const {type} {name}");
		if(DEFAULT is not null) {
			sb.Append($"= {DEFAULT}");
		}
		sb.AppendLine(";");
	}
	// assign
	public void Assign(string name, string value) {}
	// rawcall
	public void Call(string name, string target = null, params string[] paras) {
		sb.Append(FLAGS.get_c_indent);
		if(target is not null) {
			sb.Append($"{target} = ");
		}
		sb.Append($"{name}(");
		paras.ToList().ForEach(x => {
			sb.Append($"{x}");
			if(paras.Last() != x) {
				sb.Append(", ");
			}
		});
		sb.AppendLine($");");
	}
	public string Build() {
		return sb.ToString();
	}
}