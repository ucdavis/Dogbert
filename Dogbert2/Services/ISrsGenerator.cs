using Dogbert2.Core.Domain;

namespace Dogbert2.Services
{
    public interface ISrsGenerator
    {
        byte[] GeneratePdf(Project project, bool draft);
    }
}
