using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class ServiceResult<T>
    {
        public T Data { get; set; }
        public ICollection<string> Errors { get; set; }
        public bool IsValid => !Errors?.Any() == true;

        public ServiceResult()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
