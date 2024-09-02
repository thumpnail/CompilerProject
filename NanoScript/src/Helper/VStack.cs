namespace NanoScript.Helper;

public struct VStack<T> {
    //value
    public T Value {
        get => this.stack[stack.Count-1]; 
        set => this.stack[stack.Count-1] = value;
    }
    public int Count => this.stack.Count;
    List<T> stack = new();
    List<int> frame = new();
    public VStack() {
        this.stack = new List<T>();
    }
    public object this[int i] {
        get { return stack[i]??throw new NullReferenceException(); }
        set { stack[i] = (T)value; }
    }
    //push
    public void Push(T value) {
        this.stack.Add(value);
    }
    public void PushFrame(int value) {
        this.frame.Add(value);
    }
    //peek
    public T Peek() {
        return this.stack.Last();
    }
    //pop
    public T Pop() {
        var res = this.stack.Last();
        this.stack.RemoveAt(this.stack.Count - 1);
        return res;
    }
    public void PopFrame() {
        this.stack.RemoveRange(this.frame.Last(), this.frame.Count - this.frame.Last());
        this.frame.RemoveAt(this.frame.Count - 1);
    }
    //clear
    public void Clear() {
        this.stack.Clear();
    }
    //size
    public int Size() {
        return this.stack.Count;
    }
    //GetValue
    public T GetValue() {
        return Value;
    }
    public T GetValue(int i) {
        return this.stack[i];
    }
    public T GetValue(Predicate<T> predicate) {
        return this.stack.FindLast(predicate)??throw new NullReferenceException();
    }
}