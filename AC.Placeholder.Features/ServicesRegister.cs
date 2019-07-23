using AC.Placeholder.Components;
using AC.Placeholder.Documents;
using AC.Placeholder.Features.Navigations;
using AC.Placeholder.Resolvers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace AC.Placeholder.Features
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class ServicesRegister : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(INavigationService), typeof(NavigationService));
        }
    }
}