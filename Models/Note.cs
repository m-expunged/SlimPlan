namespace SlimPlan.Models
{
    public sealed class Note
    {
        #region Properties

        public string Text { get; set; }

        #endregion Properties

        #region Constructors

        public Note(string text)
        {
            Text = text;
        }

        #endregion Constructors
    }
}
