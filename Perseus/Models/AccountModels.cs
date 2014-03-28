using Perseus.DataModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Perseus.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class NewUserModel
    {
        [Required]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }

    public class EditAccountModel
    {
        [Required]
        [Display(Name = "User id")]
        //[HiddenInput]
        public string UId { get; set; }

        [Required]
        [Display(Name = "Felhasználónév")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Add meg a neved!")]
        [Display(Name = "Keresztnév név")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Add meg a neved!")]
        [Display(Name = "Vezetéknév")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Állapot")]
        public bool Status { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Jelszó megerősítése")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Szerepkörök")]
        public List<RoleCheckBox> Roles { get; set; }

        public EditAccountModel()
        {
            Roles = new List<RoleCheckBox>();
        }
    
    }


    #region PermissionModels - ezek tartoznak a People/Permission oldalhoz
    public class RoleCheckBox
    {
        public string Text { get; set; }
        public bool Checked { get; set; }

        public RoleCheckBox(string text, bool check)
        {
            Text = text;
            Checked = check;
        }
        public RoleCheckBox() { }
    }

    public class PermissionsTableModel
    {
        public List<Role> Roles { get; set; }
        public List<ModuleRoles> Modules { get; set; }

        public PermissionsTableModel()
        {
            Roles = new List<Role>();
            Modules = new List<ModuleRoles>();
        }
    }

    public class ModuleRoles
    {
        public string ModuleName { get; set; }
        public List<ModuleRolesLine> AccessLevels { get; set; }

        public ModuleRoles() { AccessLevels = new List<ModuleRolesLine>(); }
        public ModuleRoles(string name, List<ModuleRolesLine> access)
        {
            ModuleName = name; AccessLevels = access;
        }
    }
    public class ModuleRolesLine
    {
        public int Pid { get; set; }
        public string Name { get; set; }
        public List<bool> Boxes { get; set; }

        public ModuleRolesLine() { Boxes = new List<bool>(); }
        public ModuleRolesLine(int pid, string name, List<bool> boxes)
        {
            Pid = pid; Name = name; Boxes = boxes;
        }
        public ModuleRolesLine(int pid, string name) { Pid = pid; Name = name; Boxes = new List<bool>(); }
    }

    #endregion

}
