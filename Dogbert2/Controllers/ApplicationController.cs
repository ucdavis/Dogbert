using System;
using System.Linq;
using System.Web.Mvc;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Web.Controller;
using UCDArch.Web.Attributes;

namespace Dogbert2.Controllers
{
    [Version(MajorVersion=1)]
    [ServiceMessage("Dogbert", ViewDataKey="ServiceMessages", MessageServiceAppSettingsKey="MessageService")]
    public class ApplicationController : SuperController
    {

    }

}
