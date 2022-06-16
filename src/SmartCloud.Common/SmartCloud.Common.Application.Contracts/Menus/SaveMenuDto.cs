using SmartCloud.Common.RoleMenus;

namespace SmartCloud.Common.Menus
{
    public class SaveMenuDto : MenuDto
    {
        public List<RoleMenuDto> RoleMenus { get; set; }

        public string[] Users { get; set; }
    }
}
