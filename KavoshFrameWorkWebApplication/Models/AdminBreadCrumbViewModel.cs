namespace KavoshFrameWorkWebApplication.Models
{
    public class AdminBreadCrumbViewModel
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
    }

    public class AdminBreadCrumbArgsViewModel
    {
        public string Controller { get; set; }
        public string EntityTitle { get; set; }
        public AdminPageType PageType { get; set; }
        public string ItemTitle { get; set; }
        public object Id { get; set; }
    }
}
