using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dogbert.Controllers.Helpers;
using Dogbert.Controllers.ViewModels;
using Dogbert.Core.Domain;
using Dogbert.Core.Resources;
using UCDArch.Core.PersistanceSupport;
using UCDArch.Core.Utils;
using UCDArch.Web.Controller;
using UCDArch.Web.Helpers;
using MvcContrib;
using MvcContrib.Attributes;

namespace Dogbert.Controllers
{
    public class ChangeLogController : SuperController
    {
 
        /// <summary>
        /// // GET: /ChangeLog/Create/{id}
        /// </summary>
        /// <param name="projectId">Project Id to link to</param>
        /// <returns></returns>
        public ActionResult Create(int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var viewModel = ChangeLogViewModel.Create(Repository, project);

            return View(viewModel);
        }

        /// <summary>
        /// Creates the specified change log entry.
        /// </summary>
        /// <param name="changeLog">The Change Log Entry.</param>
        /// <param name="projectId">The project id.</param>
        /// <returns></returns>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Create(ChangeLog changelog, int projectId)
        {
            var project = Repository.OfType<Project>().GetNullableByID(projectId);

            if (project == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "Project");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            changelog.Project = project;

            changelog.TransferValidationMessagesTo(ModelState);
            if (ModelState.IsValid)
            {
                Repository.OfType<ChangeLog>().EnsurePersistent(changelog);
                Message = string.Format(NotificationMessages.STR_ObjectCreated, "ChangeLog");
                //return this.RedirectToAction<ProjectController>(a => a.Edit(projectId));
                return Redirect(Url.EditProjectUrl(projectId, StaticValues.Tab_ChangeLog));
            }

            var viewModel = ChangeLogViewModel.Create(Repository, project);
            viewModel.ChangeLog = changelog;
            return View(viewModel);
        }

        /// <summary>
        /// // GET: /ChangeLog/Edit/{id}
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var ChangeLog = Repository.OfType<ChangeLog>().GetNullableByID(id);

            if (ChangeLog == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ChangeLog");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            var viewModel = ChangeLogViewModel.Create(Repository, ChangeLog.Project);
            viewModel.ChangeLog = ChangeLog;

            return View(viewModel);

        }

        /// <summary>
        /// Edits the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="ChangeLog">The ChangeLog.</param>
        /// <returns></returns>
        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, ChangeLog ChangeLog)
        {
            var dest = Repository.OfType<ChangeLog>().GetNullableByID(id);

            if (ChangeLog == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ChangeLog");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            Copiers.CopyChangeLog(ChangeLog, dest);
            dest.TransferValidationMessagesTo(ModelState);

            if (ModelState.IsValid)
            {
                Repository.OfType<ChangeLog>().EnsurePersistent(dest);
                Message = string.Format(NotificationMessages.STR_ObjectUpdated, "ChangeLog");
                //return this.RedirectToAction<ProjectController>(a => a.Edit(projectId));
                return Redirect(Url.EditProjectUrl(dest.Project.Id, StaticValues.Tab_ChangeLog));
            }

            var viewModel = ChangeLogViewModel.Create(Repository, dest.Project);
            viewModel.ChangeLog = ChangeLog;

            return View(viewModel);
        }

        /// <summary>
        /// // GET: /ChangeLog/Delete/{id}
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            var ChangeLog = Repository.OfType<ChangeLog>().GetNullableByID(id);

            if (ChangeLog == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ChangeLog");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }
            var viewModel = ChangeLogViewModel.Create(Repository, ChangeLog.Project);
            viewModel.ChangeLog = ChangeLog;

            return View(viewModel);

        }

        [AcceptPost]
        [ValidateInput(false)]
        public ActionResult Delete(int id, ChangeLog entry)
        {
            var entryToDelete = Repository.OfType<ChangeLog>().GetNullableByID(id);

            if (entryToDelete == null)
            {
                Message = string.Format(NotificationMessages.STR_ObjectNotFound, "ChangeLog");
                return this.RedirectToAction<ProjectController>(a => a.DynamicIndex());
            }

            var saveProjectId = entryToDelete.Project.Id;

            Repository.OfType<ChangeLog>().Remove(entryToDelete);
            Message = string.Format(NotificationMessages.STR_ObjectRemoved, "ChangeLog");
            return Redirect(Url.EditProjectUrl(saveProjectId, StaticValues.Tab_ChangeLog));

        }

    }
}
