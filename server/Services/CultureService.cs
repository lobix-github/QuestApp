using System.Threading;

namespace QuestApp.Services
{
    public class CultureService<T> : ICultureService<T>
    {
        private readonly ResourcesService resourcesService;

        public CultureService(ResourcesService resourcesService)
        {
            this.resourcesService = resourcesService;
        }

        public string GetString(string culture, string key)
        {
            try
            {
                return resourcesService.Overlay[culture][typeof(T).ToString().Replace("QuestApp.", "")][key];
            }
            catch
            {
                return key;
            }
        }

        public string this[string key] => GetString(Thread.CurrentThread.CurrentUICulture.Name, key);
    }

    public interface ICultureService<T>
    {
        string GetString(string culture, string key);

        string this[string name] { get; }
    }
}
