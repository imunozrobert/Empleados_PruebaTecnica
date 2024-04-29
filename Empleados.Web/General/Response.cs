namespace Empleados.Web.General
{
    public class Response
    {
        public bool success { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

        public Response()
        {
            this.success = true;
        }
    }
}
