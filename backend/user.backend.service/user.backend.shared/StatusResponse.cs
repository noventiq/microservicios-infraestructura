namespace user.backend.shared
{
    public class StatusResponse<T> : StatusResponseSimple
    {
        public T Data { get; set; }
        public StatusResponse(bool success, string title) : base(success, title)
        {
        }
    }
}
