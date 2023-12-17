using BookShop1Asm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShop1Asm.ViewModels.UserRolesViewModel;

namespace BookShop1Asm.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<IndexVM>();
            foreach (ApplicationUser user in users)
            {
                var indexVM = new IndexVM();
                indexVM.UserId = user.Id;
                indexVM.Email = user.Email;
                indexVM.Fullname = user.Fullname;
                indexVM.Address = user.Address;
                indexVM.Roles = await GetUserRoles(user);
                userList.Add(indexVM);
            }
            return View(userList);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                
                return NotFound();
            }
            ViewBag.Email = user.Email;
            var model = new List<EditVM>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                var editVM = new EditVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    editVM.Selected = true;
                }
                else
                {
                    editVM.Selected = false;
                }
                model.Add(editVM);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(List<EditVM> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ChangePass(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var model =new ChangePassVM();
            model.UserId = userId;
            model.Email = user.Email;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ChangePass(ChangePassVM changePassVM)
        {
            var user = await _userManager.FindByIdAsync(changePassVM.UserId);
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, changePassVM.Password.Trim());
                string code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, code, changePassVM.Password.Trim());

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(changePassVM);
        }
    }
}
