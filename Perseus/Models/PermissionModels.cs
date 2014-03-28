using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Models
{
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
}