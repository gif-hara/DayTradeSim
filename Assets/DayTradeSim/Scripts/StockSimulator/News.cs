namespace DayTradeSim
{
    public class News
    {
        public int CompanyId { get; private set; }
        
        public string Title { get; private set; }
        
        public string Content { get; private set; }
        
        public News(int companyId, string title, string content)
        {
            CompanyId = companyId;
            Title = title;
            Content = content;
        }
    }
}
