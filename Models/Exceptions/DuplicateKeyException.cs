namespace LodSalgsSystemFDF.Models.Exceptions
{
    public class DuplicateKeyException : Exception
    {

        public DuplicateKeyException(string errmsg) : base(errmsg)
        {
        }
    }
}
