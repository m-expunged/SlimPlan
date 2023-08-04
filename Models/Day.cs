using SlimPlan.Helpers;
using System.Text.Json.Serialization;

namespace SlimPlan.Models
{
    public sealed class Day
    {
        #region Properties

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly DateUtc { get; set; }

        public Dictionary<Guid, Note> Notes { get; set; }

        #endregion Properties

        #region Constructors

        public Day(DateOnly dateUtc)
        {
            DateUtc = dateUtc;
            Notes = new Dictionary<Guid, Note>();
        }

        #endregion Constructors

        #region Methods

        public void AddNote(string text)
        {
            var note = new Note(text);

            Notes.Add(Guid.NewGuid(), note);
        }

        #endregion Methods
    }
}
