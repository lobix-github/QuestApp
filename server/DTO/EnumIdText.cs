namespace QuestApp.DTO
{
    public class EnumIdText<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }

        public override bool Equals(object obj) => Equals(obj as EnumIdText<T>);
        public bool Equals(EnumIdText<T> other) => GetHashCode() == other?.GetHashCode();
        public override int GetHashCode() => Id.GetHashCode();
    }
}
