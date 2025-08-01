using MSFIApp.Services.Common;

namespace MSFIApp.Dtos.User.News
{
    public class Request
    {

    }

    public class Response : ApiResponse<ResponseData>
    {

    }

    public class ResponseData
    {
        public List<ArticleModel> Articles { get; set; }
        public List<ClassModel> Classes { get; set; }
    }

    public class ArticleModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrtl { get; set; }
        public string Url { get; set; }
    }

    public class ClassModel
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }
}
