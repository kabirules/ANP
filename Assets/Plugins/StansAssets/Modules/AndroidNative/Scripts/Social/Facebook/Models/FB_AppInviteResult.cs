using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FB_AppInviteResult : FB_Result {

    private bool _cancelled = true;
    private IDictionary<string, object> _resultDictionary;

    public FB_AppInviteResult(bool cancelled, string RawData, string Error, IDictionary<string, object> dict) : base(RawData, Error)
    {
        _cancelled = cancelled;
        _resultDictionary = dict;
    }

    public bool Cancelled
    {
        get
        {
            return _cancelled;
        }
    }

    public IDictionary<string, object> ResultDictionary
    {
        get
        {
            return _resultDictionary;
        }
    }
}
