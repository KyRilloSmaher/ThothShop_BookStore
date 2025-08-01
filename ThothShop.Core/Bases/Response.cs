﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ThothShop.Core.Bases
{
    
    public class Response<T>
    {
        #region Feilds

                public HttpStatusCode StatusCode { get; set; }
                public object Meta { get; set; }
                public bool Succeeded { get; set; }
                public string Message { get; set; }
                public List<string> Errors { get; set; }
                public Dictionary<string, List<string>> ErrorsBag { get; set; }
                public T Data { get; set; }
        #endregion

        #region Constructors
                public Response()
                {

                }
                public Response(T data, string message = null)
                {
                    Succeeded = true;
                    Message = message;
                    Data = data;
                }
                public Response(string message)
                {
                    Succeeded = false;
                    Message = message;
                }
                public Response(string message, bool succeeded)
                {
                    Succeeded = succeeded;
                    Message = message;
                }

        #endregion
    }
}
