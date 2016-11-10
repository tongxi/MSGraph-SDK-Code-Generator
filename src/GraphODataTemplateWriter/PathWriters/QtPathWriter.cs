// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.IO;
using Vipr.Core.CodeModel;
using System.Globalization;
using Microsoft.Graph.ODataTemplateWriter.PathWriters;
using Microsoft.Graph.ODataTemplateWriter.Settings;
using Microsoft.Graph.ODataTemplateWriter.TemplateProcessor;

namespace Microsoft.Graph.ODataTemplateWriter.PathWriters
{
    class QtPathWriter : PathWriterBase
    {

        public override string WritePath(ITemplateInfo template, string entityTypeName)
        {
            string prefix = ConfigurationService.Settings.NamespacePrefix;
            string coreFileName = this.TransformFileName(template, entityTypeName);

            return Path.Combine(
                template.OutputParentDirectory, 
                String.Format(CultureInfo.InvariantCulture,"{0}{1}",
                    prefix,
                    coreFileName
                )
            );
        }

    }
}
