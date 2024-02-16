using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace QuestApp.Services
{
    public class ResourcesService
    {
        public ResourcesService()
        {
            foreach (var culture in Extensions.CULTURES.Keys)
            {
                Overlay[culture] = new Dictionary<string, Dictionary<string, string>>();
                foreach (var page in Pages)
                {
                    var resourceManager = new ResourceManager("QuestApp.Resources." + page, Assembly.GetExecutingAssembly());
                    Overlay[culture][page] = resourceManager.GetResourceSet(new CultureInfo(culture), true, true)
                                                .Cast<DictionaryEntry>()
                                                .ToDictionary(d => d.Key.ToString(), d => d.Value?.ToString() ?? "");
                }
            }
        }

        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> Overlay { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        private IEnumerable<string> pages;
        public IEnumerable<string> Pages => pages ?? (pages = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(x => x.ToString().StartsWith("QuestApp.Pages.") || x.ToString() == "QuestApp.App")
            .Where(x => !x.ToString().EndsWith("Base"))
            .Where(x => !x.ToString().Contains('+'))
            .Where(x => !x.ToString().Contains('_'))
            .Select(x => x.ToString().Replace("QuestApp.", "")));
    }
}
