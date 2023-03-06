using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NetCivitaiModelManager.ViewModels;
using System;
using System.Linq;

namespace NetCivitaiModelManager
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            var splitname = name.Split('.');
            var shortname = splitname.LastOrDefault();
            name = name.Replace(shortname, "Controls") + $".{shortname}";
            type = Type.GetType(name);
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}