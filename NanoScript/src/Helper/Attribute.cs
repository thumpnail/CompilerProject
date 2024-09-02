namespace NanoScript.Helper;

public class MissingFeatureAttribute : Attribute {
    public MissingFeatureAttribute(string name) {
        throw new Exception($"Missing Feature: {name}");
    }
}