namespace SlimPlan.Models
{
    public sealed class Plan
    {
        #region Properties

        public string Name { get; set; }

        public List<Day> Days { get; set; }

        #endregion Properties

        #region Constructors

        public Plan(string name)
        {
            Name = name;
            Days = new List<Day>();
        }

        #endregion Constructors

        #region Methods

        public List<Day> GetOrAddDays(DateTime date)
        {
            var day = DateOnly.FromDateTime(date);
            var diff = (7 + (day.DayOfWeek - DayOfWeek.Monday)) % 7;
            var startOfWeek = day.AddDays(-1 * diff);

            var days = new List<Day>();

            for (int i = 0; i < 7; i++)
            {
                var dayOfWeek = startOfWeek.AddDays(i);
                days.Add(GetOrAddDay(dayOfWeek));
            }

            Days.Sort((x, y) => x.DateUtc.CompareTo(y.DateUtc));

            return days;
        }

        public bool TryRemoveNote(DateTime date, Guid id)
        {
            var day = DateOnly.FromDateTime(date);

            return Days.SingleOrDefault(d => d.DateUtc == day)?.Notes.Remove(id) ?? false;
        }

        private Day GetOrAddDay(DateOnly date)
        {
            var day = Days.SingleOrDefault(d => d.DateUtc == date);

            if (day is null)
            {
                day = new Day(date);
                Days.Add(day);
            }

            return day;
        }

        #endregion Methods
    }
}
