namespace NanoScriptCompiler_Bflat.Helper; 

public class MissingFeatureAttribute : Attribute {
    public MissingFeatureAttribute(string name) {
        throw new Exception($"Missing Feature: {name}");
    }
}