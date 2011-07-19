using UCDArch.Web.Controller;
using UCDArch.Web.Attributes;

namespace Dogbert2.Controllers
{
    [Version(MajorVersion=1)]
#if DEBUG
#else
    [ServiceMessage("Dogbert", ViewDataKey="ServiceMessages", MessageServiceAppSettingsKey="MessageService")]
#endif
    public class ApplicationController : SuperController
    {

    }

}
