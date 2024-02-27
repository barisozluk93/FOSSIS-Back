using MenuManagement.Entity;

namespace MenuManagement.Model
{
    public class MenuDTO : Menu
    {
        public List<MenuDTO> ChildMenus { get; set; } = new List<MenuDTO>();
    }
}
