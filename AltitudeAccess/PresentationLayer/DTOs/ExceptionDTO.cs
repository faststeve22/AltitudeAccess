namespace AltitudeAccess.PresentationLayer.DTOs
{
    public class ExceptionDTO
    {
        public string Message { get; set; }
        public string Source { get; set; }
        public ExceptionDTO InnerException { get; set; }
    }
}
