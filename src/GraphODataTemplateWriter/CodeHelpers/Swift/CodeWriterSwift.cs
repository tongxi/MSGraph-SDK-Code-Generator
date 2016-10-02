// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Swift
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Graph.ODataTemplateWriter.Extensions;
    using Microsoft.Graph.ODataTemplateWriter.Settings;
    using Vipr.Core.CodeModel;

    public class CodeWriterSwift : CodeWriterBase
    {
        public CodeWriterSwift() : base() { }

        public CodeWriterSwift(OdcmModel model) : base(model) { }

        public override String WriteOpeningCommentLine()
        {
            return "// ------------------------------------------------------------------------------" + this.NewLineCharacter;
        }

        public override String WriteClosingCommentLine()
        {
            return "// ------------------------------------------------------------------------------";
        }

        public override string WriteInlineCommentChar()
        {
            return "//  ";
        }
        public string GetNamespacePrefix()
        {
            return ConfigurationService.Settings.NamespacePrefix;
        }
    }

}
