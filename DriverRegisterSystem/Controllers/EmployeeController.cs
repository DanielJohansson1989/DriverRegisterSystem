using DriverRegisterSystem.Models;
using DriverRegisterSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DriverRegisterSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public EmployeeController(UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var result = await _userManager.GetUsersInRoleAsync("Employee");
            if (result == null)
            {
                return NotFound();
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(u => u.UserName.ToLower().Contains(searchString.ToLower())).ToList();
            }
            return View(result);
        }

        //public IActionResult Create()
        //{
        //    return RedirectToPage("/Account/Register", new { area = "Identity" });
        //}

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            //Fetch Roles for dropdown
            var roles = _roleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name, // r.Id Osäker på om värdet ska vara Id eller Name
                Text = r.Name
            }).ToList();

            //Fetch current role of the user
            var currentRoles = await _userManager.GetRolesAsync(user);

            var model = new EmployeeViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = currentRoles.FirstOrDefault(),
                Roles = roles
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.UserName = model.Email;
            user.NormalizedUserName = model.Email.ToUpper();
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
            
            // Update the user's role
            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
            {
                var removeRoleResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                if (!removeRoleResult.Succeeded)
                {
                    foreach (var error in removeRoleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            // Assign the new role
            var addRoleResult = await _userManager.AddToRoleAsync(user, model.Role);
            if (!addRoleResult.Succeeded)
            {
                foreach (var error in addRoleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Index");
        }
    }
}
