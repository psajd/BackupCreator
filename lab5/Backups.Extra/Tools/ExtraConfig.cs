using Backups.Extra.Logger;
using Backups.Extra.Modifications;
using Backups.Extra.Selection;

namespace Backups.Extra.Tools;

public class ExtraConfig
{
    public ExtraConfig(ILogger logger, IMerger merger, IDeleter deleter, ISelectionAlgorithm selectionAlgorithm)
    {
        Logger = logger;
        Merger = merger;
        Deleter = deleter;
        SelectionAlgorithm = selectionAlgorithm;
    }

    public ILogger Logger { get; }

    public IMerger Merger { get; }

    public IDeleter Deleter { get; }

    public ISelectionAlgorithm SelectionAlgorithm { get; }
}