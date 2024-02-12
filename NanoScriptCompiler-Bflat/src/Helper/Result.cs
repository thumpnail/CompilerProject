namespace NanoScriptCompiler_Bflat.Helper; 

class Result<T> {
    private bool success;
    private T? value;
    public Result(bool success, T? value = default) {
        this.success = success;
        this.value = value;
    }
    public bool IsSuccessful(out T? value) {
        if (!success) {
            value = default;
            return false;
        } else {
            value = this.value;
        }
        return success;
    }
}