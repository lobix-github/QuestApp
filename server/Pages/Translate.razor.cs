using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Resources;
using System.Resources.NetStandard;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public partial class Translate
    {
        async void ImportClick(InputFileChangeEventArgs e)
        {
            using var memoryStream = new MemoryStream();
            await e.File.OpenReadStream().CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            using var resourceReader = new ResXResourceReader(memoryStream);
            var rs = new ResourceSet(resourceReader);

            foreach (DictionaryEntry de in rs)
            {
                if (resourceSet.ContainsKey(de.Key.ToString()))
                {
                    resourceSet[de.Key.ToString()] = de.Value?.ToString() ?? "";
                }
            }

            StateHasChanged();
        }

        async void ExportClick()
        {
            if (!isEdit)
            {
                await SaveLabels();
            }

            using var stream = new MemoryStream();
            using var writer = new ResXResourceWriter(stream);
            var rs = ResourcesService.Overlay[selectedCulture][selectedPage];
            rs.Keys.ToList().ForEach(key => writer.AddResource(key, rs[key].ToString()));
            writer.Generate();

            await JS.InvokeVoidAsync("saveAsFile", $"{selectedPage}.{selectedCulture}.resx", Convert.ToBase64String(stream.ToArray()));
        }

        async Task SaveLabels()
        {
            foreach (var entry in resourceSet.ToArray())
            {
                resourceSet[entry.Key] = await JS.InvokeAsync<string>("getElementTextContentById", entry.Key);
            }
        }
    }
}

