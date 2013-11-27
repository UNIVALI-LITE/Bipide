using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

public partial class  BIPASMLexer 
{
    public delegate void HandlerMsgRequest(string _msg, int _linha);
    public event HandlerMsgRequest MessageRequest;

    public override string GetErrorMessage(RecognitionException e, string[] tokenNames)
    {
        if (this.MessageRequest!= null)
            this.MessageRequest(e.Message, e.Line);
        return base.GetErrorMessage(e, tokenNames);
    }

}
