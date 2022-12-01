using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ST10116374_PROG6212_POE.Pages
{
    public class IndexModel : PageModel
    {
        public List<UserInfo> userinfo = new List<UserInfo>();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public class UserInfo
        {
            public string name;
            public string password;
        }
    }
}