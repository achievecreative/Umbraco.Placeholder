using System.Collections.Generic;
using AC.Placeholder.Models;

namespace AC.Placeholder
{
    public interface IPlaceholderResolver : IReplaceable
    {
        PlaceholderModel Find(int pageContentId);
    }
}