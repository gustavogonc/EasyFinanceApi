﻿namespace EasyFinance.Communication.Response;
public class ResponseErrorJson
{
    public IList<string> Errors { get; set; } = [];
    public ResponseErrorJson(string error) => Errors = [error];
    public ResponseErrorJson(IList<string> errors) => Errors = errors;
    public bool TokenIsExpired = false;
}

