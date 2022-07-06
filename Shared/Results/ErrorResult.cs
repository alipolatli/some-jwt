using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class ErrorResult
    {
        public List<string> Errors { get; private set; }

        public bool IsShow { get; private set; }

        public ErrorResult()
        {
            Errors = new List<string>();
        }

        public ErrorResult(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }

        public ErrorResult(string error, bool isShow)
        {
            Errors = new List<string>();
            Errors.Add(error);
            IsShow = isShow;
        }
    }
}
