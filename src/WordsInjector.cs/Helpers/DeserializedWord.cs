namespace WordsInjector.cs.Helpers
{
    public class PolishWord
    {
        public string? Description { get; set; }
        public string? Remark { get; set; }
        public string? CategoryId { get; set; }
        public int Level { get; set; }
        public List<TranslatedWord>? TranslatedWords { get; set; }
    }

    public class TranslatedWord
    {
        public int Language { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
