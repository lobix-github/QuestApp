namespace DbManager.DTO
{
    internal class AnswerDTO
    {
        public int Id { get; set; }

        public string Text_pl { get; set; }
        public string Text_en { get; set; }
        public string Text_de { get; set; }
        public string Text_nl { get; set; }
        public string Comment_pl { get; set; }
        public string Comment_en { get; set; }
        public string Comment_de { get; set; }
        public string Comment_nl { get; set; }
        public int Index { get; set; }

        public override string ToString() => $"{Id}: {Text_pl}";

        public AnswerDTO Clone()
        {
            return new AnswerDTO
            {
                Id = Id,
                Text_pl = Text_pl,
                Text_en = Text_en,
                Text_de = Text_de,
                Text_nl = Text_nl,
                Comment_pl = Comment_pl,
                Comment_en = Comment_en,
                Comment_de = Comment_de,
                Comment_nl = Comment_nl,
                Index = Index,
            };
        }
    }
}
