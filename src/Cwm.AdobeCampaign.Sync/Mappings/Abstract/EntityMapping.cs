﻿using System;
using System.Collections.Generic;
using System.Xml;
using Cwm.AdobeCampaign.Templates;
using Cwm.AdobeCampaign.Templates.Model;
using Cwm.AdobeCampaign.WebServices.Model;
using Cwm.AdobeCampaign.WebServices.Model.Abstract;
using Cwm.AdobeCampaign.WebServices.Services;

namespace Cwm.AdobeCampaign.Sync.Mappings.Abstract
{
    /// <summary>
    /// Contains helper methods for mapping between a .NET class and information formatted for Campaign to understand.
    /// </summary>
    public abstract class EntityMapping<T> : Mapping<T>
        where T : Entity, new()
    {
        #region Fields

        private readonly string[] _queryFields = { "@name", "@label", "data" };

        #endregion

        #region Properties

        /// <summary>
        /// List of field names which should be requested when querying Campaign.
        /// </summary>
        public override IEnumerable<string> QueryFields { get { return _queryFields; } }

        /// <summary>
        /// List of the attributes on the root element which should be persisted to the local file on download.
        /// </summary>
        public virtual IEnumerable<string> AttributesToKeep { get { return new string[0]; } }

        #endregion

        #region Methods

        /// <summary>
        /// Map the information parsed from a file into a class which can be sent to Campaign to be saved.
        /// </summary>
        /// <param name="requestHandler">Request handler, which can be used if further information from Campaign is required for the mapping.</param>
        /// <param name="template">Class containing file content and metadata.</param>
        /// <returns>Class containing information which can be sent to Campaign</returns>
        public override IPersistable GetPersistableItem(IRequestHandler requestHandler, Template template)
        {
            return new T
            {
                Name = template.Metadata.Name,
                Label = template.Metadata.Label,
                RawXml = template.Code,
            };
        }

        /// <summary>
        /// Map the information sent back by Campaign into a format which can be saved as a file to disk.
        /// </summary>
        /// <param name="requestHandler">Request handler, which can be used if further information from Campaign is required for the mapping.</param>
        /// <param name="rawQueryResponse">Raw response from Campaign.</param>
        /// <returns>Class containing file content and metadata</returns>
        public override Template ParseQueryResponse(IRequestHandler requestHandler, string rawQueryResponse)
        {
            throw new NotImplementedException();
            //var doc = new XmlDocument();
            //doc.LoadXml(rawQueryResponse);

            //var metadata = new TemplateMetadata
            //{
            //    Schema = InternalName.Parse(Schema),
            //    Name = new InternalName(doc.DocumentElement.Attributes["namespace"].InnerText, doc.DocumentElement.Attributes["name"].InnerText),
            //    Label = doc.DocumentElement.Attributes["label"].InnerText,
            //};

            //doc.DocumentElement.RemoveAllAttributesExcept(AttributesToKeep);
            //doc.DocumentElement.RemoveChild("createdBy");
            //doc.DocumentElement.RemoveChild("modifiedBy");

            //var rawCode = doc.OuterXml;
            //return new Template
            //{
            //    Code = rawCode,
            //    Metadata = metadata,
            //    FileExtension = FileTypes.Xml,
            //};
        }

        #endregion
    }
}