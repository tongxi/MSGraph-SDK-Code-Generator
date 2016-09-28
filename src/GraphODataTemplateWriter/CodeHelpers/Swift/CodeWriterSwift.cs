// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Swift
{
    using System;
    using Vipr.Core.CodeModel;

    public class CodeWriterSwift : CodeWriterBase
    {
        public CodeWriterSwift() : base() { }

        public CodeWriterSwift(OdcmModel model) : base(model) { }

        public override string WriteClosingCommentLine()
        {
            throw new NotImplementedException();
        }

        public override string WriteInlineCommentChar()
        {
            throw new NotImplementedException();
        }

        public override string WriteOpeningCommentLine()
        {
            throw new NotImplementedException();
        }
    }

}
