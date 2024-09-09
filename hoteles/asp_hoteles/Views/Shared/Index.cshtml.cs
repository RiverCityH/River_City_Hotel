using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Logging;

namespace asp_hoteles.Pages
{
    public class IndexModel : PageModel
    {
        public bool IsLogged = false;
        [BindProperty] public string? Email { get; set; }
        [BindProperty] public string? Password { get; set; }

        public void OnGet()
        {
            try
            {
                var user = HttpContext.Session.GetObject<string>("User");
                if (user != null)
                {
                    IsLogged = true;
                    ViewData["Logged"] = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                Email = string.Empty;
                Password = string.Empty;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(Email) &&
                    string.IsNullOrEmpty(Password))
                {
                    OnPostBtClean();
                    return;
                }

                if (ServiceData.UserData != Email + "." + Password)
                {
                    OnPostBtClean();
                    return;
                }
                OnPostBtClean();
                ViewData["Logged"] = true;
                HttpContext.Session.SetObject("User", Email);
                IsLogged = true;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }

        public void OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();
                HttpContext.Response.Redirect("/");
                IsLogged = false;
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
            }
        }
    }
}