﻿using System.Collections.Generic;

namespace Ixq.Extensions.ObjectModel
{
    public class ReturnModel
    {
        public ReturnModel(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public ReturnModel(params string[] errors)
        {
            Errors = errors;
        }

        public ReturnModel(bool success)
        {
            Succeeded = success;
        }

        public ReturnModel()
        {
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; }
    }
}