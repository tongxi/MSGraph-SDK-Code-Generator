// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information.﻿

using System;
using System.Collections.Generic;
using Microsoft.Graph.ODataTemplateWriter.Extensions;
using Microsoft.Graph.ODataTemplateWriter.Settings;
using Vipr.Core.CodeModel;

namespace Microsoft.Graph.ODataTemplateWriter.CodeHelpers.Qt
{
    public static class TypeHelperQt
    {
        public static string Prefix = ConfigurationService.Settings.NamespacePrefix;

        public static ICollection<string> ReservedNames {
            get
            {
                return new HashSet<string> {
                    "description", "default"  , "self"
                };
            }
        }

        public static string GetTypeString(this OdcmType type)
        {
            if (type == null)
            {
                return "id";
            }
            switch (type.Name) {
                case "String":
                    return "QString";
                case "Int32":
                    return "int32_t";
                case "Int64":
                    return "int64_t";
                case "Int16":
                case "Byte":
                    return "int16_t";
                case "Guid":
                    return "QString";
                case "Double":
                case "Float":
                    return "double";
                case "DateTimeOffset":
                    return "QDateTime";
                case "Binary":
                    return "QString";
                case "Boolean":
                    return "bool";
                default:
                    return Prefix + type.Name.ToUpperFirstChar();
            }
        }

        public static string GetTypeString(this OdcmProperty property)
        {
            return property.Type.GetTypeString();
        }

        public static bool IsCollection(this OdcmParameter parameter)
        {
            return parameter.IsCollection;
        }

        public static bool IsComplex(this OdcmType type)
        {
            string t = GetTypeString(type);
            return !(t.StartsWith("int") || t == "bool" || t == "char" || t == "double");

        }

        public static bool IsBasicType(this OdcmType type)
        {
            if (type == null)
            {
                return false;
            }
            switch (type.Name)
            {
                case "String":
                    return true;
                case "Int32":
                    return true;
                case "Int64":
                    return true; ;
                case "Int16":
                case "Byte":
                    return true;
                case "Guid":
                    return true;
                case "Double":
                case "Float":
                    return true;
                case "DateTimeOffset":
                    return true;
                case "Binary":
                    return true;
                case "Boolean":
                    return true;
                default:
                    return false;
            }

        }

        public static bool IsComplex(this OdcmProperty property)
        {
            return property.Type.IsComplex();
        }

        public static string ToSetterTypeString(this OdcmProperty property)
        {
            return string.Format("{0} {1}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty));
        }

        public static string ToPropertyString(this OdcmProperty property)
        {
            return string.Format("{0} {1}{2}", property.GetFullType(), (property.IsComplex() ? "*" : string.Empty), SanitizePropertyName(property));
        }

        public static string SanitizePropertyName(this OdcmProperty property)
        {
            if (ReservedNames.Contains(property.Name.ToLower()))
            {
                return property.Class.Name.ToLowerFirstChar() + property.Name.ToUpperFirstChar();
            }
            return property.Name;
        }

        public static string GetFullType(this OdcmProperty property)
        {
            if (property.IsCollection)
            {
                return "ODCollection<" + property.Type.GetTypeString() + ">";
            }
            else
            {
                return property.Type.GetTypeString();
            }
        }

        public static string GetFullType(this OdcmType type)
        {
            return GetTypeString(type);
        }

        public static bool IsSystem(this OdcmProperty property)
        {
            return property.Type.IsSystem();
        }

        public static bool IsSystem(this OdcmType type)
        {
            string t = GetTypeString(type);
            return (t.StartsWith("int") || t == "bool" || t == "char" || t == "double" || t == "QString" || t == "QDateTime");
        }

        public static bool IsDate(this OdcmProperty prop)
        {
            return prop.Type.IsDate();
        }
        public static bool IsDate(this OdcmType type)
        {
            string typeString = GetTypeString(type);
            return typeString.Equals("QDateTime");
        }

        public static bool IsString(this OdcmType type)
        {
            string t = GetTypeString(type);
            return (t == "QString");
        }

        public static bool IsString(this OdcmProperty property)
        {
            return property.Type.IsString();
        }

        public static bool IsInteger(this OdcmType type)
        {
            string t = GetTypeString(type);
            return t.StartsWith("int");
        }

        public static bool IsInteger(this OdcmProperty property)
        {
            return property.Type.IsInteger();
        }

        public static string GetToLowerFirstCharName(this OdcmProperty property)
        {
            return property.Name.ToLowerFirstChar();
        }

        public static string GetToLowerImport(this OdcmProperty property)
        {
            var index = property.Type.Name.LastIndexOf('.');
            return property.Type.Name.Substring(0, index).ToLower() + property.Type.Name.Substring(index);
        }

        public static string GetToUpperFirstCharName(this OdcmProperty property)
        {
            return property.Name.Substring(property.Name.IndexOf(".") + 1).ToUpperFirstChar();
        }

        public static bool IsEnum(this OdcmProperty property)
        {
            return property.Type is OdcmEnum;
        }
        public static string GetQTTypeToJsonConverter(this OdcmProperty property, String value)
        {
            string objectiveCType = property.GetTypeString();
            if (objectiveCType.Equals("int32_t") 
                || objectiveCType.Equals("int16_t")
                || objectiveCType.Equals("bool")
                || objectiveCType.Equals("double")
                || objectiveCType.Equals("QString")
                || objectiveCType.Equals("int64_t"))
            {
                return "QJsonValue(" + value + ")";
            }
            else if (objectiveCType.Equals("QDateTime"))
            {
                return "QJsonValue(" + value + ".toString(Qt::ISODate))";
            }
            throw new ArgumentException();
        }

        public static string GetQTPointerTypeToJsonConverter(this OdcmProperty type, String value)
        {
            string objectiveCType = type.GetTypeString();
            if (objectiveCType.Equals("int32_t")
                || objectiveCType.Equals("int16_t")
                || objectiveCType.Equals("bool")
                || objectiveCType.Equals("double")
                || objectiveCType.Equals("QString")
                || objectiveCType.Equals("int64_t"))
            {
                return "QJsonValue(*" + value + ")";
            }
            else if (objectiveCType.Equals("QDateTime"))
            {
                return "QJsonValue(" + value + "->toString(Qt::ISODate))";
            }
            throw new ArgumentException();
        }

        public static string GetQTTypeToJsonConverter(this OdcmType type, String value)
        {
            string objectiveCType = type.GetTypeString();
            if (objectiveCType.Equals("int32_t")
                || objectiveCType.Equals("int16_t")
                || objectiveCType.Equals("bool")
                || objectiveCType.Equals("double")
                || objectiveCType.Equals("QString")
                || objectiveCType.Equals("int64_t"))
            {
                return "QJsonValue(" + value + ")";
            }
            else if (objectiveCType.Equals("QDateTime"))
            {
                return "QJsonValue(" + value + ".toString(Qt::ISODate))";
            }
            throw new ArgumentException();
        }

        public static string GetQTJsonParserConverterFunction(this OdcmProperty property, String value)
        {
            string objectiveCType = property.GetTypeString();
            if (objectiveCType.Equals("int32_t") || objectiveCType.Equals("int16_t"))
            {
                return value + ".toInt()";
            }
            if (objectiveCType.Equals("int64_t"))
            {
                return value + ".toVariant().toLongLong()";
            }
            else if (objectiveCType.Equals("bool"))
            {
                return value + ".toBool()";
            }
            else if (objectiveCType.Equals("double"))
            {
                return value + ".toDouble()";
            }
            else if (objectiveCType.Equals("QString"))
            {
                return value + ".toString()";
            }
            else if (objectiveCType.Equals("QDateTime"))
            {
                return "QDateTime::fromString(" + value + ".toString(), Qt::ISODate)";
            }
            throw new ArgumentException();
        }
	}
}
