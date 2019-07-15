using System.Collections.Generic;
using AC.Placeholder.Models;

namespace AC.Placeholder
{
    public interface IPlaceholderResolver
    {
        PlaceholderModel Find(int pageContentId);

        int Order { get; }
    }
}