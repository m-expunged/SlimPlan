using SlimPlan.Models;
using System.Text;
using System.Text.Json;

namespace SlimPlan.Services
{
    public sealed class PlansService
    {
        #region Fields

        private readonly DirectoryInfo _storageDirectory;

        #endregion Fields

        #region Constructors

        public PlansService()
        {
            _storageDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plans"));

            _storageDirectory.Create();
        }

        #endregion Constructors

        #region Methods

        public List<string> GetNames()
        {
            var names = new List<string>();

            foreach (var file in _storageDirectory.EnumerateFiles())
            {
                var name = Path.GetFileNameWithoutExtension(file.Name);

                if (name.EndsWith(".plan"))
                {
                    names.Add(name[..^5]);
                }
            }

            return names;
        }

        public bool Exists(string name)
        {
            var names = GetNames();

            return names.Contains(name, StringComparer.InvariantCultureIgnoreCase);
        }

        public Plan? GetPlan(string name)
        {
            Plan plan;

            try
            {
                var filePath = Path.Combine(_storageDirectory.FullName, $"{name}.plan.json");
                var json = File.ReadAllText(filePath, Encoding.UTF8);

                plan = JsonSerializer.Deserialize<Plan>(json) ?? throw new Exception();
            }
            catch
            {
                return null;
            }

            return plan;
        }

        public void WriteToFile(Plan plan)
        {
            var filePath = Path.Combine(_storageDirectory.FullName, $"{plan.Name}.plan.json");
            var json = JsonSerializer.Serialize(plan);
            File.WriteAllText(filePath, json, Encoding.UTF8);
        }

        public void Delete(string name)
        {
            var filePath = Path.Combine(_storageDirectory.FullName, $"{name}.plan.json");
            File.Delete(filePath);
        }

        #endregion Methods
    }
}
