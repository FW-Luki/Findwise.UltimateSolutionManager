using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Findwise.SolutionManager.Core.Model
{
    public class BindableConfigurationSite : ISite, IMenuCommandService
    {
        public BindableConfigurationSite(IComponent component)
        {
            Component = component;
        }

        #region ISite
        public IComponent Component { get; }

        public IContainer Container => null;

        public bool DesignMode => false;

        public string Name { get; set; }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IMenuCommandService))
                return this;
            return null;
        }
        #endregion

        #region IMenuCommandService
        public DesignerVerbCollection Verbs
        {
            get
            {
                var verbs = new DesignerVerbCollection();
                foreach (var method in Component.GetType().GetMethods().Where(m => m.GetCustomAttributes(false).OfType<BrowsableAttribute>().FirstOrDefault()?.Browsable ?? false))
                {
                    var displayName = method.GetCustomAttributes(false).OfType<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? method.Name;
                    verbs.Add(new DesignerVerb(displayName, (s_, e_) => method.Invoke(Component, new object[] { })));
                }
                return verbs;
            }
        }

        public void AddCommand(MenuCommand command)
        {
            //throw new NotImplementedException();
        }

        public void AddVerb(DesignerVerb verb)
        {
            //throw new NotImplementedException();
        }

        public MenuCommand FindCommand(CommandID commandID)
        {
            throw new NotImplementedException();
        }

        public bool GlobalInvoke(CommandID commandID)
        {
            throw new NotImplementedException();
        }

        public void RemoveCommand(MenuCommand command)
        {
            //throw new NotImplementedException();
        }

        public void RemoveVerb(DesignerVerb verb)
        {
            //throw new NotImplementedException();
        }

        public void ShowContextMenu(CommandID menuID, int x, int y)
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}
