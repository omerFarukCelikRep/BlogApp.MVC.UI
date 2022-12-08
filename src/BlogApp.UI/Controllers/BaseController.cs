using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.UI.Controllers;

[Authorize]
public class BaseController : Controller
{
}
